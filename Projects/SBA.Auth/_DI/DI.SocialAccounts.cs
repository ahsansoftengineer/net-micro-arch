using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace SBA.Projectz.DI;

public static partial class DI_Projectz
{
  public static AuthenticationBuilder Config_Social_Auth(this AuthenticationBuilder srvc, IConfiguration config)
  {
    var accounts = new Option_SocialAccounts();
    config.GetSection(Option_SocialAccounts.SectionName).Bind(accounts);

    return srvc
      .Add_OAuthFromAccount(accounts.Google)
      .Add_OAuthFromAccount(accounts.Microsoft);
      // .Add_OAuthFromAccount(accounts.Facebook)
      // .Add_OAuthFromAccount(accounts.Github)
      // .Add_OAuthFromAccount(accounts.LinkedIn)
      // .Add_OAuthFromAccount(accounts.Twitter)
      // .Add_OAuthFromAccount(accounts.Apple);
  }
  private static AuthenticationBuilder Add_OAuthFromAccount(this AuthenticationBuilder builder, Option_SocialAccount account)
  {
    return builder.AddOAuth(account.Scheme, opt =>
    {
      opt.ClientId = account.ClientId;
      opt.ClientSecret = account.ClientSecret;
      opt.AuthorizationEndpoint = account.AuthorizationEndpoint;
      opt.TokenEndpoint = account.TokenEndpoint;
      opt.UserInformationEndpoint = account.UserInformationEndpoint;
      opt.CallbackPath = new PathString(account.CallbackPath);
      opt.SaveTokens = true;

      foreach (var scope in account.Scopes)
        opt.Scope.Add(scope);

      foreach (var claimMap in account.ClaimMap)
      {
        var claimType = claimMap.Key switch
        {
          "nameidentifier" => ClaimTypes.NameIdentifier,
          "name" => ClaimTypes.Name,
          "email" => ClaimTypes.Email,
          _ => claimMap.Key
        };
        opt.ClaimActions.MapJsonKey(claimType, claimMap.Value);
      }

      opt.Events = new OAuthEvents
      {
        OnCreatingTicket = async context =>
        {
          // Apple does not provide userinfo endpoint; handle separately if needed
          if (!string.IsNullOrEmpty(opt.UserInformationEndpoint))
          {
            var request = new HttpRequestMessage(HttpMethod.Get, opt.UserInformationEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
            var response = await context.Backchannel.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
            context.RunClaimActions(user);
          }
        }
      };
    });
  }
}