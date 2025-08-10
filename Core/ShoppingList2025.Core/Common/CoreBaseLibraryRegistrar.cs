using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core;
public static class CoreBaseLibraryRegistrar
{

    public static ILogger RegisterMauiServices(IServiceCollection services, Assembly assembly, ApplicationPlattformType plattformType)
    {
        SharedSettings.ApplicationPlattformType = plattformType;
        var mainAssembly = RegisterServices(services, assembly);

        return ApplicationLoggingRegistrar.RegisterServices(services, mainAssembly);
    }

    public static ILogger RegisterBlazorServerServices(IServiceCollection services, Assembly assembly)
    {
        SharedSettings.ApplicationPlattformType = ApplicationPlattformType.BlazorServer;
        var mainAssembly = RegisterServices(services, assembly);

        return ApplicationLoggingRegistrar.RegisterServices(services, mainAssembly);
    }

    private static MainAssembly RegisterServices(IServiceCollection services, Assembly assembly)
    {
        var mainAssembly = new MainAssembly
        {
            EntryAssembly = assembly
        };
        services.AddSingleton<IMainAssembly>(mainAssembly);
        services.AddScoped<IIocContainerProvider, IocContainerProvider>();

        return mainAssembly;
    }

}
