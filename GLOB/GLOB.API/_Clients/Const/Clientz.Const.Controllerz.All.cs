namespace GLOB.API.Clientz;

public class ControllerzX
{
  public readonly string ProjectzLookupBase = "_projectz-lookup-base";
  public readonly string ProjectzLookup = "_projectz-lookup";
}
public class ControllerzAuth : ControllerzX
{
  public readonly string userz = "userz";
  public readonly string rolez = "rolez";
  public readonly string permission = "permission";
  
}
public class ControllerzHierarchy : ControllerzX
{
  public readonly string GlobalLookupBase = "_global-lookup-base";
  public readonly string GlobalLookup = "_global-lookup";

  public readonly string country = "country";
  public readonly string state = "state";
  public readonly string city = "city";
  
}