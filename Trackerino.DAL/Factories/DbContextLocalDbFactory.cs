using Microsoft.EntityFrameworkCore;
namespace Trackerino.DAL.Factories;

    public class DbContextLocalDbFactory : IDbContextFactory<TrackerinoDbContext>
    {
        public readonly bool _seedDemoData;
        private readonly DbContextOptionsBuilder<TrackerinoDbContext> _dbContextOptionsBuilder = new();

        public DbContextLocalDbFactory(string databaseName, bool seedDemoData)
        {
            _seedDemoData = seedDemoData;
            _dbContextOptionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;" +
                                                  $" Initial Catalog = {databaseName};" +
                                                  " MultipleActiveResultSets = True;" +
                                                  " Encrypt = False;" +
                                                  "Integrated Security = True;" +
                                                  " TrustServerCertificate = True;");
        }
        public TrackerinoDbContext CreateDbContext() => new (_dbContextOptionsBuilder.Options, _seedDemoData);
    }