using GLOB.Domain.Model.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;

namespace GLOB.Infra.Data.Auth;
// No DI
public abstract partial class DBCtxIdentity : IdentityDbContext<InfraUser, InfraRole, string>
{
  protected readonly IConfiguration _config;

  public string DOTNET_ENVIRONMENT { get; }

  public DBCtxIdentity(DbContextOptions options, IServiceProvider sp) : base(options)
  {
    _config = sp.GetSrvc<IConfiguration>();
    DOTNET_ENVIRONMENT = _config.GetValueStr("DOTNET_ENVIRONMENT");
  }

  protected override void OnModelCreating(ModelBuilder mb)
  {
    DBCtx.EntityMappingConfig(mb);
    mb.SeedInfraIdentity();
    base.OnModelCreating(mb);
  }
}
