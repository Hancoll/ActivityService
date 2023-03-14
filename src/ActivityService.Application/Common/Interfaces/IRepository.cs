using ActivityService.Domain.Common;
using ActivityService.Domain.Common.Interfaces;

namespace ActivityService.Application.Common.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
{
    void Insert(TEntity entity);

    void Insert(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void Delete(TEntity entity);

    void Update(TEntity entity);

    TEntity? GetEntity(Specification<TEntity> filter);

    IEnumerable<TEntity> GetEntities(Specification<TEntity> filter);
}

