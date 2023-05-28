using Microsoft.EntityFrameworkCore;

namespace Trackerino.DAL.Factories;

    public class DbContextLocalDbFactory : IDbContextFactory<TrackerinoDbContext>
    {
        public readonly bool _seedTestingData;
        private readonly DbContextOptionsBuilder<TrackerinoDbContext> _dbContextOptionsBuilder = new();

        public DbContextLocalDbFactory(string databaseName, bool seedTestingData = true)
    {
        _seedTestingData = seedTestingData;
        _dbContextOptionsBuilder.UseSqlServer($"Data Source=(LocalDB)\\MSSQLLocalDB; Initial Catalog = {databaseName}; MultipleActiveResultSets = True; Integrated Security = True; Encrypt=False; TrustServerCertificate = True;");
        }
        public TrackerinoDbContext CreateDbContext() => new (_dbContextOptionsBuilder.Options, false, _seedTestingData);
    }