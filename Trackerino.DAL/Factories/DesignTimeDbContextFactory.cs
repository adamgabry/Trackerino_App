using Microsoft.EntityFrameworkCore.Design;
namespace Trackerino.DAL.Factories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TrackerinoDbContext>
{
    private readonly DbContextSqLiteFactory _dbContextSqLiteFactory;
    public DesignTimeDbContextFactory()
    {
        _dbContextSqLiteFactory = new DbContextSqLiteFactory("Trackerino",true);
    }

    public TrackerinoDbContext CreateDbContext(string[] args) => _dbContextSqLiteFactory.CreateDbContext();
}

