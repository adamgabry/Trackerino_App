using Microsoft.EntityFrameworkCore;
using Trackerino.DAL;
using Trackerino.DAL.Factories;

namespace Trackerino.Common.Tests;

public class DbContextLocalDbTestingFactory : IDbContextFactory<TrackerinoDbContext>
{
    private readonly bool _seedTestingData;
    private readonly DbContextOptionsBuilder<TrackerinoDbContext> _dbContextOptionsBuilder = new();

    public DbContextLocalDbTestingFactory(string databaseName, bool seedTestingData = true)
    {
        _seedTestingData = seedTestingData;
        _dbContextOptionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;" +
                                              $" Initial Catalog = {databaseName};" +
                                              " MultipleActiveResultSets = True;" +
                                              " Encrypt = False;" +
                                              " TrustServerCertificate = True;");
    }
    public TrackerinoDbContext CreateDbContext() => new(_dbContextOptionsBuilder.Options, _seedTestingData);
}

