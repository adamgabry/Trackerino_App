using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit.Abstractions;
using Trackerino.DAL.Factories;
namespace Trackerino.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{   
    //private readonly TrackerinoDbContext _dbContextSut;
    protected DbContextTestsBase()
    {
        TrackerinoDbContextSUT = new Trackerino.DAL.Factories.DefaultFactory().CreateDbContext(new[] { "" });
    }
    protected TrackerinoDbContext TrackerinoDbContextSUT { get; }

    public async Task InitializeAsync()
    {
        await TrackerinoDbContextSUT.Database.EnsureDeletedAsync();
        await TrackerinoDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await TrackerinoDbContextSUT.Database.EnsureDeletedAsync();
        await TrackerinoDbContextSUT.DisposeAsync();
    }
}