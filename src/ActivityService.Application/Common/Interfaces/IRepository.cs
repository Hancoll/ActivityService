﻿using ActivityService.Domain.Common;

namespace ActivityService.Application.Common.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    void Insert(TEntity entity);

    void Insert(IEnumerable<TEntity> entities);

    void Remove(Guid id);

    void Update(TEntity entity);

    bool IsExists(Guid id);

    TEntity? GetEntity(Specification<TEntity>? filter = null);

    IEnumerable<TEntity> GetEntities(Specification<TEntity>? filter = null, int? from = null, int? size = null);
}

