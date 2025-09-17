using GLOB.Domain.Model.Auth;
using GLOB.Infra.Data.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SBA.Auth.Services;

namespace SBA.Auth.Controllers;
public abstract class AccountBaseController<T> : API_1_InjectorController<T>
{
  // private IRepoGenericz<AccountId> _repo = null;
  protected readonly Option_JwtSettings _jwt;
  protected readonly UserManager<InfraUser> _userManager;
  protected readonly SignInManager<InfraUser> _signInManager;
  protected readonly TokenService _tokenService;
  protected readonly SmtpEmailSender _emailSender;
  protected readonly DBCtxInfraIdentity _ctx;

  protected IUOW_Projectz _uowProjectz { get; }
  public AccountBaseController(
    IServiceProvider srvcProvider
  ) : base(srvcProvider)
  {
    _ctx = _sp.GetSrvc<DBCtxInfraIdentity>();
    _uowProjectz = _sp.GetSrvc<IUOW_Projectz>();

    _userManager = _sp.GetSrvc<UserManager<InfraUser>>();
    _signInManager = _sp.GetSrvc<SignInManager<InfraUser>>();;
    _tokenService = _sp.GetSrvc<TokenService>();
    _emailSender = _sp.GetSrvc<SmtpEmailSender>();
    
    _jwt = _sp.GetSrvc<IOptions<Option_JwtSettings>>().Value;
  }
}