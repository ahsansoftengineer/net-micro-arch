
namespace GLOB.Infra.Model.Base;

public abstract class EntityAlpha : EntityAlpha<int>, IEntityAlpha
{
}

public abstract class EntityBeta : EntityBeta<int>, IEntityBeta, IEntityAlpha
{
}

public abstract class EntityBase : EntityBase<int>, IEntityBase, IEntityAlpha
{
}