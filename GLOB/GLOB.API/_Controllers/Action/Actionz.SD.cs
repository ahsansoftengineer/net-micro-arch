using GLOB.Infra.Repo;
namespace GLOB.API.Staticz;

public static partial class _Actionz
{
  public static async Task<IActionResult> ToActionDelete<T>(this IRepoGenericz<T> repo, IUOW_Infra uow, int Id)
    where T : class, IEntityAlpha
  {
      if (Id < 1) return _Res.NotFoundId(Id);
      var item = await repo.Get(Id);

      if (item == null) return _Res.NotFoundId(Id);
      await repo.Delete(Id);
      await uow.Save();
      return "Record Deleted Successfull".Ok();
  }

  public static async Task<IActionResult> ToActionStatus<T>(this IRepoGenericz<T> repo, IUOW_Infra uow, int Id, Status status)
    where T : class, IEntityAlpha
  {

    try
    {
      if (Id < 0) return _Res.NotFoundId(Id);
      if(!Enum.IsDefined(status)) return _Res.InvalidEnums(status.ToString());
      
      var item = await repo.Get(Id);

      if (item == null) return _Res.NotFoundId(Id);

      repo.UpdateStatus(item, status);
      await uow.Save();
      return item.ToExtVMSingle().Ok();
    }
    catch (Exception ex)
    {
      return _Res.CatchException(ex, nameof(ToActionStatus));
    }
  }

}