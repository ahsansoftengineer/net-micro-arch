namespace GLOB.API.Config.Optionz;

public class Option_SocialAccounts
{
  public static string SectionName = "SocialAccounts";
  public Option_SocialAccount Google { get; set; }
  public Option_SocialAccount Microsoft { get; set; }
  public Option_SocialAccount Facebook { get; set; }
  public Option_SocialAccount Github { get; set; }
  public Option_SocialAccount LinkedIn { get; set; }
  public Option_SocialAccount Twitter { get; set; }
  public Option_SocialAccount Apple { get; set; }

}
public class Option_SocialAccount
{
  public string Scheme { get; set; } // Google, Microsoft, Facebook, Apple
  public string ClientId { get; set; }
  public string ClientSecret { get; set; }
  public string AuthorizationEndpoint { get; set; }
  public string TokenEndpoint { get; set; }
  public string UserInformationEndpoint { get; set; }
  public string CallbackPath { get; set; }
  public List<string> Scopes { get; set; } = new();
  public Dictionary<string, string> ClaimMap { get; set; } = new();
}