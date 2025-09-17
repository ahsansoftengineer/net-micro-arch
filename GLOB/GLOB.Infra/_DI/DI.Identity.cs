using Microsoft.AspNetCore.Identity;
using GLOB.Domain.Model.Auth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GLOB.Infra.DI;
public static partial class DI_Infra
{
  public static void Add_Infra_DB_SQL_Identity<TContext, TIUOW, TUOW>(this IServiceCollection srvc, IConfiguration config)
    where TContext : IdentityDbContext<InfraUser, InfraRole, string>
    where TIUOW : class, IUOW_Infra
    where TUOW : UOW_Infra, TIUOW
  {
    srvc.Add_Infra_DB_SQL<TContext, TIUOW, TUOW>(config);

    // Configure Identity with roles
    // srvc.AddIdentity<InfraUser, InfraRole>()
    srvc.AddIdentity<InfraUser, InfraRole>()
    .AddEntityFrameworkStores<TContext>()
    .AddDefaultTokenProviders();
  }

}