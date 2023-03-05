using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Trackerino.DAL.Factories
{
    public class DefaultFactory : IDesignTimeDbContextFactory<TrackerinoDbContext>
    {
        public TrackerinoDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<TrackerinoDbContext> builder = new();
            
            builder.UseSqlServer(
                );
                return new TrackerinoDbContext(builder.Options);
        }
    }
    public class SQLiteFactory : IDbContextFactory<TrackerinoDbContext>
    {
        public TrackerinoDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<TrackerinoDbContext> builder = new();
            builder.UseSqlite()
            return new TrackerinoDbContext(builder.Options);
        }
    }
}
