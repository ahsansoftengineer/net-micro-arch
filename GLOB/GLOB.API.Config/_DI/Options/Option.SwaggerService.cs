namespace GLOB.API.Config.Optionz;
public class Option_SwaggerService
{
    public string Name { get; set; }
    public string Url { get; set; }
}
public class Option_SwaggerServices
{
  public static string SectionName = "SwaggerServices";
  public List<Option_SwaggerService> Services { get; set; }
}
