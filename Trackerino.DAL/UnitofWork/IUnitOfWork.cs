using System;
using System.Threading.Tasks;
using Trackerino.DAL.Entities;
using Trackerino.DAL.Mappers;
using Trackerino.DAL.Repositories;

namespace Trackerino.DAL.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<TEntity> GetRepository<TEntity, TEntityMapper>()
        where TEntity : class, IEntity
        where TEntityMapper : IEntityMapper<TEntity>, new();

    Task CommitAsync();
}
