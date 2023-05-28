using Microsoft.EntityFrameworkCore;
using Trackerino.Common.Tests.Seeds;
using Trackerino.DAL;

namespace Trackerino.Common.Tests;

public class TrackerinoTestingDbContext : TrackerinoDbContext
{
    private readonly bool _seedTestingData;

    public TrackerinoTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = true)
        : base(contextOptions, seedDemoData: false, seedTestingData)
    {
        _seedTestingData = seedTestingData;
    }
}
