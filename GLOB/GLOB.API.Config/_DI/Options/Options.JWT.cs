using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GLOB.API.Config.Optionz;
public class Option_JwtSettings
{

  public static string? SectionName = "JwtSettings";
  public string? SecretKey { get; set; } //"YourSuperStrongSecretKey_ReplaceThis"
  public string? Issuer { get; set; } // "https://localhost:1104/"
  public string? Audience { get; set; } // "https://localhost:1104/"
  public int AccessTokenExpiryMinutes { get; set; } //60 -> 15
  public int? AccessTokenExpiryHour { get; set; } //1
  public int RefreshTokenExpiryDays { get; set; } //7
  public bool ValidateIssuer { get; set; }// = true;
  public bool ValidateAudience { get; set; }// = true;
  public bool ValidateLifetime { get; set; }// = true;
  public bool ValidateIssuerSigningKey { get; set; }// = true;

  // Cookie
  public static string? Scheme = "AuthorizationCookieScheme";
  public string? LoginPath { get; set; }
  public string? CookieName { get; set; }
  public string? AccessDeniedPath { get; set; }
  public bool? SlidingExpiration { get; set; }// = true;
  public SymmetricSecurityKey GetSymmetricSecurityKey()
  {
    return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
  }
}