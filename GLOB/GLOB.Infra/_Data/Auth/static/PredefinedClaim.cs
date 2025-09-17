namespace GLOB.Infra.Data.Auth;
public class ACTION
{
    public static readonly string ALL =  "ALL";
    public static readonly string CREATE =  "CREATE";
    public static readonly string READ =  "READ";
    public static readonly string UPDATE =  "UPDATE";
    public static readonly string DELETE =  "DELETE";
    public static readonly string STATUS =  "STATUS";
    public static readonly string REPORT =  "REPORT";
    public static readonly string EXPORT =  "EXPORT";
     
}

public class ROLE
{
    public static readonly string ADMIN =  "ADMIN";
    public static readonly string USER =  "USER";
    public static readonly string MANAGER =  "MANAGER";
    public static readonly string PERMISSION =  "PERMISSION";
    public static readonly string USER_CREATOR =  "USER_CREATOR";
    public static readonly string USER_STANDARD =  "USER_STANDARD";
    public static readonly string USER_BUSINESS =  "USER_BUSINESS";
    public static readonly string USER_AGENT =  "USER_AGENT";
    public static readonly string USER_MARKETING =  "USER_MARKETING";
    // ROLES TABLE
    public static List<string> ROLES = [
        ADMIN, 
        USER, 
        MANAGER, 
        USER_CREATOR,
        USER_STANDARD,
        USER_BUSINESS,
        USER_AGENT,
        USER_MARKETING
    ];
}
