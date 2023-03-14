using System.Linq.Expressions;

namespace ActivityService.Domain.Common.Specifications;

public class EntityByIdSpecification<TEntity> : Specification<TEntity> where TEntity : Entity
{
    private readonly Guid _id;

    public EntityByIdSpecification(Guid id)
    {
        _id = id;
    }

    public override Expression<Func<TEntity, bool>> ToExpression()
    {
        return entity => entity.Id == _id;
    }
}
