using System;
using Microsoft.Extensions.DependencyInjection;
using ShoppingList2025.Database.Types;

namespace ShoppingList2025.Database;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
    {
        services.AddSingleton<IDbServiceFactory, DbServiceFactory>();
        services.AddScoped<IDbService>(provider =>
        {
            var dbServiceFactory = provider.GetService<IDbServiceFactory>();
            return dbServiceFactory?.CreateNew() ?? throw new NullReferenceException("Unable to resolve DbServiceFactory");
        });

        return services;
    }
}
