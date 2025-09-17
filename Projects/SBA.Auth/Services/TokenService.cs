using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using GLOB.Domain.Model.Auth;
using GLOB.Infra.Data.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
namespace SBA.Auth.Services;

public class TokenService
{
  private readonly UserManager<InfraUser> _userManager;
  protected readonly Option_JwtSettings _jwt;

  private readonly DBCtxInfraIdentity _context;

  public TokenService(
    IOptions<Option_JwtSettings> Option_JwtSettings,
    UserManager<InfraUser> userManager,
    DBCtxInfraIdentity context)
  {
    _jwt = Option_JwtSettings.Value;
    _userManager = userManager;
    _context = context;
  }
  public async Task<object> GetUserClaimSignInCookie(InfraUser user, HttpContext httpContext)
  {
    string JTI = Guid.NewGuid().ToString();
    var authClaims = new List<Claim>
      {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, JTI),
        new Claim(JwtRegisteredClaimNames.Sub, user.Id)
      };

    var roles = await _userManager.GetRolesAsync(user);

    foreach (var role in roles)
    {
      authClaims.Add(new Claim(ClaimTypes.Role, role));
    }
    // Add identity and principal
    var identity = new ClaimsIdentity(authClaims, Option_JwtSettings.Scheme);
    var principal = new ClaimsPrincipal(identity);

    // Sign in using cookie
    await httpContext.SignInAsync(Option_JwtSettings.Scheme, principal);
    return new
    {

      user.Id,
      user.UserName,
      user?.Email,
      user?.EmailConfirmed,
      user.PhoneNumber,
      user.PhoneNumberConfirmed,
      user.TwoFactorEnabled,
      jti = JTI,
      roles
    };
  }
  public async Task<object> GetTokensAndUserClaims(InfraUser user, HttpContext httpContext)
  {
    var roles = await _userManager.GetRolesAsync(user);

    string jti;
    var accessToken = GetAccessToken(user, roles, out jti);
    var refreshToken = GetRefreshToken();
    var accessTokenExpiry = DateTime.UtcNow.AddMinutes(_jwt.AccessTokenExpiryMinutes);
    // var accessTokenExpiry = DateTime.UtcNow.AddHours(_jwt.AccessTokenExpiryHour);
    var refreshTokenExpiry = DateTime.UtcNow.AddDays(_jwt.RefreshTokenExpiryDays);

    string ip = httpContext.Connection.RemoteIpAddress?.ToString();
    await SaveRefreshTokenAsync(user.Id, refreshToken, ip, jti);

    return new
    {
      accessToken,
      refreshToken,
      // expiresIn = _jwt.AccessTokenExpiryHour,
      expiresIn = _jwt.AccessTokenExpiryMinutes,
      accessTokenExpiry = accessTokenExpiry.ToString("o"), // ISO 8601
      refreshTokenExpiry = refreshTokenExpiry.ToString("o"),
      tokenType = "Bearer",
      user = new
      {
        user.Id,
        user.UserName,
        user?.Email,
        user?.EmailConfirmed,
        user.PhoneNumber,
        user.PhoneNumberConfirmed,
        user.TwoFactorEnabled,
        jti,
        roles
      }
    };
  }
  public string GetAccessToken(InfraUser user, IList<string> roles, out string JTI)
  {
    JTI = Guid.NewGuid().ToString();
    var authClaims = new List<Claim>
    {
      new Claim(ClaimTypes.NameIdentifier, user.Id),
      new Claim(ClaimTypes.Name, user.UserName),
      new Claim(JwtRegisteredClaimNames.Jti, JTI),
      new Claim(JwtRegisteredClaimNames.Sub, user.Id)
    };

    foreach (var role in roles)
    {
      authClaims.Add(new Claim(ClaimTypes.Role, role));
    }

    var token = new JwtSecurityToken(
      issuer: _jwt.Issuer,
      audience: _jwt.Audience,
      // expires: DateTime.UtcNow.AddMinutes(_jwt.AccessTokenExpiryHour),
      expires: DateTime.UtcNow.AddMinutes(_jwt.AccessTokenExpiryMinutes),
      claims: authClaims,
      signingCredentials: new SigningCredentials(_jwt.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  public string GetRefreshToken()
  {
    var randomBytes = new byte[64];
    using var rng = RandomNumberGenerator.Create();
    rng.GetBytes(randomBytes);
    return Convert.ToBase64String(randomBytes);
  }

  public async Task SaveRefreshTokenAsync(string userId, string refreshToken, string IP, string JTI)
  {
    var token = new RefreshToken
    {
      InfraUserId = userId,
      Token = refreshToken,
      IsUsed = false,
      IsRevoked = false,
      JwtId = JTI,
      CreatedByIp = IP,
      ExpiresAt = DateTime.UtcNow.AddDays(7),
      CreatedAt = DateTimeOffset.UtcNow,
    };

    await _context.RefreshTokens.AddAsync(token);
    await _context.SaveChangesAsync();
  }
  public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
  {
    var tokenValidationParameters = new TokenValidationParameters
    {
      ValidIssuer = _jwt.Issuer,
      ValidAudience = _jwt.Audience,
      ValidateIssuer = _jwt.ValidateIssuer,
      ValidateAudience = _jwt.ValidateAudience,
      ValidateLifetime = _jwt.ValidateLifetime,
      ValidateIssuerSigningKey = _jwt.ValidateIssuerSigningKey,
      IssuerSigningKey = _jwt.GetSymmetricSecurityKey()
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    try
    {
      var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
      if (securityToken is not JwtSecurityToken jwtSecurityToken ||
          !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
      {
        return null;
      }

      return principal;
    }
    catch
    {
      return null;
    }
  }
}
