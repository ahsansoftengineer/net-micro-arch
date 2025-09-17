namespace GLOB.API.Config.Optionz;
public class Option_Hangfire
{
    public string Title { get; set; }
    public List<Option_HangfireUser> Users { get; set; }
}

public class Option_HangfireUser
{
    public string User { get; set; }
    public string Pass { get; set; }
}