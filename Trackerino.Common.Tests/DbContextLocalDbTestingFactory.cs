using Microsoft.EntityFrameworkCore;
using Trackerino.DAL;
using Trackerino.DAL.Factories;

namespace Trackerino.Common.Tests;

public class DbContextLocalDbTestingFactory : IDbContextFactory<TrackerinoDbContext>
{
    private readonly bool _seedTestingData;
    private readonly string _databaseName;

    public DbContextLocalDbTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }


    public TrackerinoDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<TrackerinoDbContext> builder = new();
        builder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;" +
                             $" Initial Catalog = {_databaseName};" +
                             " MultipleActiveResultSets = True;" +
                             " Encrypt = False;" +
                             " TrustServerCertificate = True;");
        builder.EnableSensitiveDataLogging();
        return new TrackerinoTestingDbContext(builder.Options, _seedTestingData);
    }
}

