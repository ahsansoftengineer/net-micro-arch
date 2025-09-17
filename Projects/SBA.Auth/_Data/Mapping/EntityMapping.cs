using GLOB.Domain.Model.Auth;


public static class EntityMapping
{
  public static void MapRefreshToken(this ModelBuilder mb)
  {
     mb.Entity<RefreshToken>(entity =>
    {
        entity.HasKey(rt => rt.Id);
        entity.HasOne(rt => rt.InfraUser)
              .WithMany() // Or .WithMany(u => u.RefreshTokens) if you add navigation on user side
              .HasForeignKey(rt => rt.InfraUserId)
              .OnDelete(DeleteBehavior.Cascade);
    });
  }
}
 