
using System.Text;
using Hangfire.Dashboard;

namespace SBA.Projectz.DI;

public class HangfireCustomBasicAuthenticationFilter : IDashboardAuthorizationFilter
{
    public required string User { get; set; }
    public required string Pass { get; set; }

    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();
        var authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
        httpContext.Request.Headers["Authorization"].Print("Headers");

        if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Basic "))
        {
            Challenge(httpContext);
            return false;
        }

        try
        {
            var encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
            var decodedBytes = Convert.FromBase64String(encodedUsernamePassword);
            var decodedString = Encoding.UTF8.GetString(decodedBytes);
            var parts = decodedString.Split(':', 2); // split at first colon only

            if (parts.Length != 2)
                return false;

            var username = parts[0];
            var password = parts[1];

            // For multi-user support: match current user/pass
            return username == User && password == Pass;
        }
        catch
        {
            Challenge(httpContext);
            return false;
        }
    }

    private void Challenge(HttpContext context)
    {
        context.Response.StatusCode = 401;
        context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"Hangfire Dashboard\"";
    }
}
