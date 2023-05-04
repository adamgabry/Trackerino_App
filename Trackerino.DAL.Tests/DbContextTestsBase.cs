using Microsoft.EntityFrameworkCore;
using Trackerino.DAL.Factories;

namespace Trackerino.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{   
    //private readonly TrackerinoDbContext _dbContextSut;
    protected DbContextTestsBase()
    {

        TrackerinoDbContextFactory = new DbContextLocalDbFactory(GetType().FullName!, seedDemoData: false);
        TrackerinoDbContextSut = TrackerinoDbContextFactory.CreateDbContext();

    }
    protected TrackerinoDbContext TrackerinoDbContextSut { get; }
    protected IDbContextFactory<TrackerinoDbContext> TrackerinoDbContextFactory { get; }

    public async Task InitializeAsync()
    {
        await TrackerinoDbContextSut.Database.EnsureDeletedAsync();
        await TrackerinoDbContextSut.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await TrackerinoDbContextSut.Database.EnsureDeletedAsync();
        await TrackerinoDbContextSut.DisposeAsync();
    }
}