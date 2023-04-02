using Microsoft.EntityFrameworkCore;

namespace Trackerino.DAL.Factories
{
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