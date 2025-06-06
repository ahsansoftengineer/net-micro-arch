namespace GLOB.API.Config.Attributez;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class CacheAttribute : Attribute
{
  public int? DurationSeconds { get; }

  public bool IsPersistent => DurationSeconds == null;

  public CacheAttribute(int? durationSeconds = 60)
  {
    DurationSeconds = durationSeconds;
  }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class NoCacheAttribute : Attribute
{
}
