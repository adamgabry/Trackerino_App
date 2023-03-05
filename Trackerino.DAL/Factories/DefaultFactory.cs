using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Trackerino.DAL.Factories
{
    public class DefaultFactory : IDesignTimeDbContextFactory<TrackerinoDbContext>
    {
        public TrackerinoDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<TrackerinoDbContext> builder = new();
            builder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;" +
                                 " Initial Catalog = Trackerino;" +
                                 " MultipleActiveResultSets = True;" +
                                 " Encrypt = False;" +
                                 " TrustServerCertificate = True;");
            return new TrackerinoDbContext(builder.Options);
        }
    }
    public class SqLiteFactory : IDbContextFactory<TrackerinoDbContext>
    {
        public TrackerinoDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<TrackerinoDbContext> builder = new();
            builder.UseSqlite("Data Source=Trackerino.db");
            return new TrackerinoDbContext(builder.Options);
        }
    }
}
