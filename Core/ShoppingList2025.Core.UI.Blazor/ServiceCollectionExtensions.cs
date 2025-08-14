using Microsoft.Extensions.DependencyInjection;
using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core.UI.Blazor;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUIBlazor(this IServiceCollection services)
    {
        if (SharedSettings.ApplicationPlattformType == ApplicationPlattformType.BlazorServer)
        {
            services.AddScoped<IFinalExceptionHandling, FinalExceptionHandling>();
        }
        else
        {
            services.AddSingleton<IFinalExceptionHandling, FinalExceptionHandling>();
        }
        return services;
    }

    public static IServiceCollection AddUIMessageBox(this IServiceCollection services)
    {
        services.AddScoped<IApplicationMessageBoxFrontend, ApplicationMessageBoxFrontend>();
        services.AddScoped<IApplicationMessageBoxViewModel, ApplicationMessageBoxViewModel>();
        return services;
    }
}
