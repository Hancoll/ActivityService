using ActivityService.Application.Common.Interfaces;
using ActivityService.Domain.Common;
using ActivityService.Domain.Common.Interfaces;
using System.Data.SqlTypes;

namespace ActivityService.Infrastructure.Persistence;

internal class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
{
    private readonly List<TEntity> _entitites = new List<TEntity>();

    public void Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TEntity> GetEntities(Specification<TEntity> filter)
    {
        throw new NotImplementedException();
    }

    public TEntity? GetEntity(Specification<TEntity> filter)
    {
        return _entitites.Where(x => filter.IsSatisfiedBy(x)).FirstOrDefault();
    }

    public void Insert(TEntity entity)
    {
        _entitites.Add(entity);
    }

    public void Insert(IEnumerable<TEntity> entities)
    {
        foreach(var entity in entities)
        {
            Insert(entity);
        }
    }

    public void Remove(TEntity entity)
    {
        _entitites.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        throw new NotImplementedException();
    }
}
