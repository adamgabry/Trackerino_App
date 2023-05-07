using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABI.Windows.Devices.Printers;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Trackerino.BL.Mappers;
using Trackerino.DAL;
using Trackerino.DAL.Factories;

namespace Trackerino.App;

public static class DALInstaller
{
    public static IServiceCollection AddDALServices(this IServiceCollection services)
    {
        services.AddSingleton<IDbContextFactory<TrackerinoDbContext>>(provider => new DbContextLocalDbFactory("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog = Trackerino; MultipleActiveResultSets = True; Encrypt = False; TrustServerCertificate = True;", true)); // TODO correct call?
        services.AddSingleton<IDbMigrator, NoneDbMigrator>();

        services.AddSingleton<ActivityModelMapper>();
        services.AddSingleton<ProjectModelMapper>();
        services.AddSingleton<ProjectUserModelMapper>();
        services.AddSingleton<UserModelMapper>();
        services.AddSingleton<UserProjectActivityModelMapper>();
        services.AddSingleton<UserProjectModelMapper>();

        return services;
    }
}