using Microsoft.Extensions.DependencyInjection;
using Trackerino.BL.Mappers;
using Trackerino.DAL.UnitOfWork;
using Trackerino.BL.Facades;

namespace Trackerino.BL;

public static class BLInstaller
{
    public static IServiceCollection AddBLServices(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();

        services.Scan(selector => selector
            .FromAssemblyOf<BusinessLogic>()
            .AddClasses(filter => filter.AssignableTo(typeof(IFacade<,,>)))
            .AsMatchingInterface()
            .WithSingletonLifetime());

        services.Scan(selector => selector
            .FromAssemblyOf<BusinessLogic>()
            .AddClasses(filter => filter.AssignableTo(typeof(ModelMapperBase<,,>)))
            .AsMatchingInterface()
            .WithSingletonLifetime());

        return services;
    }
}