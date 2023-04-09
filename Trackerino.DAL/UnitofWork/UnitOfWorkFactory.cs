using Microsoft.EntityFrameworkCore;

namespace Trackerino.DAL.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<TrackerinoDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<TrackerinoDbContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
}
