using Microsoft.EntityFrameworkCore;
using Trackerino.Common.Tests.Seeds;
using Trackerino.DAL;

namespace Trackerino.Common.Tests;

public class TrackerinoTestingDbContext : TrackerinoDbContext
{
    private readonly bool _seedTestingData;

    public TrackerinoTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = true)
        : base(contextOptions, seedDemoData: true)
    {
        _seedTestingData = seedTestingData;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (_seedTestingData)
        {
            ProjectSeeds.Seed(modelBuilder);
            UserSeeds.Seed(modelBuilder);
            ActivitySeeds.Seed(modelBuilder);
            UserProjectSeeds.Seed(modelBuilder);
        }
    }
}
