using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core;

public static class ApplicationLoggingRegistrar
{
    public static ILogger RegisterServices(IServiceCollection services, IMainAssembly mainAssembly)
    {
        var applicationLogging = new ApplicationLogging(mainAssembly);
        applicationLogging.Init();
        var logger = applicationLogging.LoggerFactory.CreateLogger(mainAssembly.ProductName);
        services?.AddSingleton<ILogger>(logger);
        return logger;
    }
}
