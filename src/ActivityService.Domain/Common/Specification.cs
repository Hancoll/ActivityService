using ActivityService.Domain.Common.Interfaces;
using System.Linq.Expressions;

namespace ActivityService.Domain.Common;

public abstract class Specification<TEntity> where TEntity : Entity
{
    public abstract Expression<Func<TEntity, bool>> ToExpression();

    public bool IsSatisfiedBy(TEntity entity)
    {
        var predicate = ToExpression().Compile();

        return predicate(entity);
    }
}
