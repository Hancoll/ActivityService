using ActivityService.Application.Common.Interfaces;
using ActivityService.Domain.Common;

namespace ActivityService.Infrastructure.Persistence;

internal class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly List<TEntity> _entitites = new List<TEntity>();

    public TEntity? GetEntity(Guid id)
    {
        return _entitites.Find(x => x.Id == id);
    }

    public TEntity? GetEntity(Specification<TEntity> filter)
    {
        return _entitites
            .Where(filter.IsSatisfiedBy)
            .FirstOrDefault();
    }

    public IEnumerable<TEntity> GetEntities(Specification<TEntity>? filter = null, int? from = null, int? size = null)
    {
        var result = _entitites
                .Where(x => filter is null ? true : filter.IsSatisfiedBy(x))
                .Skip(from is null ? 0 : (int)from);

        if (size is not null)
            return result.Take((int)size);

        return result;
    }

    public void Insert(TEntity entity)
    {
        _entitites.Add(entity);
    }

    public void Insert(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            Insert(entity);
        }
    }

    public void Remove(Guid id)
    {
        var entity = GetEntity(id);
        _entitites.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        Remove(entity.Id);
        Insert(entity);
    }

    public bool IsExists(Guid id)
    {
        return _entitites.Where(x => x.Id == id).Any();
    }
}
