using Microsoft.EntityFrameworkCore;
using Trackerino.DAL.Factories;
using Trackerino.Common.Tests;

namespace Trackerino.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected DbContextTestsBase()
    {
        //TrackerinoContextTestingFactory = new DbContextLocalDbTestingFactory(GetType().FullName!, seedTestingData: false);
        TrackerinoDbContextFactory = new DbContextLocalDbTestingFactory(GetType().FullName!, seedTestingData: true);
        TrackerinoDbContextSut = TrackerinoDbContextFactory.CreateDbContext();

    }
    //protected IDbContextFactory<TrackerinoDbContext> TrackerinoContextTestingFactory { get; }
    protected TrackerinoDbContext TrackerinoDbContextSut { get; }
    protected IDbContextFactory<TrackerinoDbContext> TrackerinoDbContextFactory { get; }

    public async Task InitializeAsync()
    {
        await TrackerinoDbContextSut.Database.EnsureDeletedAsync();
        await TrackerinoDbContextSut.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        //await TrackerinoDbContextSut.Database.EnsureDeletedAsync();
        //await TrackerinoDbContextSut.DisposeAsync();
    }
}