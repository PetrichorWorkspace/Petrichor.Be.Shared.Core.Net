using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Core.Domain.Entities;

namespace Shared.Core.Driven.Persistence.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(string id, CancellationToken ct = default);

    Task<List<TEntity>> GetAllAsync(CancellationToken ct = default);

    ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken ct = default);
}