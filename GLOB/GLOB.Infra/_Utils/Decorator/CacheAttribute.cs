namespace GLOB.Infra.Utils.Attributez;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class CacheAttribute : Attribute
{
  public int? DurationSeconds { get; }

  public bool IsPersistent => DurationSeconds == null;

  public CacheAttribute(int? durationSeconds)
  {
    DurationSeconds = durationSeconds;
  }
  public CacheAttribute() { }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class NoCacheAttribute : Attribute
{
}
