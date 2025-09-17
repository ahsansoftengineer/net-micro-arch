// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using GLOB.Domain.Model.Auth;
// using GLOB.Infra.Data.Auth;
// using Microsoft.IdentityModel.Tokens;

// namespace GLOB.Infra.Extz;
// public static partial class ExtsInfra
// {
//   public static string GenerateJwtToken(InfraUser user, Option_JwtSettings opt)
//   {
//     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(opt.SecretKey));
//     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//     var claims = new[]
//     {
//       new Claim(JwtRegisteredClaimNames.Sub, user?.Email),
//       new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//       new Claim("UserId", user.Id)
//     };

//     var token = new JwtSecurityToken(
//       issuer: opt.Issuer,
//       audience: opt.Audience,
//       claims: claims,
//       expires: DateTime.UtcNow.AddHours(1),
//       signingCredentials: creds);

//     return new JwtSecurityTokenHandler().WriteToken(token);
//   }
// }