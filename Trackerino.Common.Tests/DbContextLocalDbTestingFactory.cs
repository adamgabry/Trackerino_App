using Microsoft.EntityFrameworkCore;
using Trackerino.DAL;
using Trackerino.DAL.Factories;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Trackerino.DAL.Migrations;

namespace Trackerino.Common.Tests;

public class DbContextLocalDbTestingFactory : IDbContextFactory<TrackerinoDbContext>
{
    private readonly bool _seedTestingData;
    private readonly DbContextOptionsBuilder<TrackerinoDbContext> _dbContextOptionsBuilder = new();
    private readonly string _databaseName;

    public DbContextLocalDbTestingFactory(string databaseName, bool seedTestingData = true)
    {
        _seedTestingData = seedTestingData;
        _databaseName = databaseName;
        _dbContextOptionsBuilder.UseSqlServer(
            $"Data Source=(LocalDB)\\MSSQLLocalDB; Initial Catalog = ${_databaseName}; MultipleActiveResultSets = True; Integrated Security = True; Encrypt=False; TrustServerCertificate = True;");
    }
    public TrackerinoDbContext CreateDbContext() => new(_dbContextOptionsBuilder.Options, _seedTestingData);
}

