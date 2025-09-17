using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace GLOB.Infra.Data.Auth;
public class AuthOptionSetup : IConfigureOptions<IdentityOptions>
{
    private readonly Option_Identity _settings;

    public AuthOptionSetup(IOptions<Option_Identity> identitySettings)
    {
        _settings = identitySettings.Value;
    }

    public void Configure(IdentityOptions opt)
    {
        // User settings
        opt.User.RequireUniqueEmail = _settings.RequireUniqueEmail;

        // Password settings
        opt.Password.RequireDigit = _settings.Password.RequireDigit;
        opt.Password.RequiredLength = _settings.Password.RequiredLength;
        opt.Password.RequireNonAlphanumeric = _settings.Password.RequireNonAlphanumeric;
        opt.Password.RequireUppercase = _settings.Password.RequireUppercase;
        opt.Password.RequireLowercase = _settings.Password.RequireLowercase;

        // Lockout settings
        opt.Lockout.MaxFailedAccessAttempts = _settings.Lockout.MaxFailAttempts;
        opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(_settings.Lockout.LockoutMinutes);
    }
}

//   "Identity": {
//     "RequireUniqueEmail": true,
//     "Password": {
//       "RequireDigit": false,
//       "RequiredLength": 5,
//       "RequireNonAlphanumeric": false,
//       "RequireUppercase": false,
//       "RequireLowercase": false
//     },
//     "Lockout": {
//       "MaxFailAttempts": 5,
//       "LockoutMinutes": 10
//     },
//     "User": {
//       "AllowedUserNameCharacters": "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"
//     }
//   },