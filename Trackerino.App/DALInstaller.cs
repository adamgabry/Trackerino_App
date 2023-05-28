using Microsoft.EntityFrameworkCore;
using Trackerino.BL.Mappers;
using Trackerino.DAL;
using Trackerino.DAL.Factories;
using Trackerino.DAL.Mappers;


namespace Trackerino.App;

public static class DALInstaller
{
    public static IServiceCollection AddDALServices(this IServiceCollection services)
    {
        services.AddSingleton<IDbContextFactory<TrackerinoDbContext>>(provider => new DbContextSqLiteFactory("Trackerino",true));
        services.AddSingleton<IDbMigrator, SqLiteDbMigrator>();

        services.AddSingleton<ActivityEntityMapper>();
        services.AddSingleton<ProjectEntityMapper>();
        services.AddSingleton<UserEntityMapper>();
        services.AddSingleton<UserProjectEntityMapper>();

        return services;
    }
}