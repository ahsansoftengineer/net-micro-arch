namespace GLOB.API.Config.Optionz;

public class Option_Logging
{
    public static string SectionName = "Logging";
    public Option_LogLevel? LogLevel { get; set; }
}

public class Option_LogLevel
{
    public static string SectionName = "LogLevel";
    public string? Default { get; set; }
    public string? MicrosoftAspNetCore { get; set; }
}

public class Option_ConnectionStrings
{
    public static string SectionName = "ConnectionStrings";
    public string? SqlConnection { get; set; }
    public string? SqlConnectionHangfire { get; set; }
    public string? Redis { get; set; }
    public string? SQLite { get; set; }
}