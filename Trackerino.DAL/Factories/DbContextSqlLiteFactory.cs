using Microsoft.EntityFrameworkCore;

namespace Trackerino.DAL.Factories
{
    public class DbContextSqlLiteFactory : IDbContextFactory<TrackerinoDbContext>
    {
        public readonly bool _seedDemoData;
        private readonly DbContextOptionsBuilder<TrackerinoDbContext> _dbContextOptionsBuilder = new();

        public DbContextSqlLiteFactory(string databaseName = "Trackerino", bool seedDemoData= true)
        {
            _seedDemoData = seedDemoData;
            _dbContextOptionsBuilder.UseSqlite($"Data Source={databaseName};Cache=Shared");
        }
        public TrackerinoDbContext CreateDbContext() => new (_dbContextOptionsBuilder.Options, _seedDemoData);
    }
}