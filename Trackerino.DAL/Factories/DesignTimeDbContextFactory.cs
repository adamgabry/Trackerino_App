using Microsoft.EntityFrameworkCore.Design;
namespace Trackerino.DAL.Factories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TrackerinoDbContext>
{
    private readonly DbContextLocalDbFactory _dbContextLocalDbFactory;
    public DesignTimeDbContextFactory()
    {
        _dbContextLocalDbFactory = new DbContextLocalDbFactory("Trackerino", seedDemoData:true);
    }

    public TrackerinoDbContext CreateDbContext(string[] args) => _dbContextLocalDbFactory.CreateDbContext();
}

