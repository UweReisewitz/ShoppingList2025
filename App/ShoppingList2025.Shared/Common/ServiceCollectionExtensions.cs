using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using ShoppingList2025.Core.UI.Blazor;

namespace ShoppingList2025.Shared;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFrontend(this IServiceCollection services)
    {
        services.AddScoped<IHomePageViewModel, HomePageViewModel>();
        services.AddScoped<IShoppingItemDetailViewModel, ShoppingItemDetailViewModel>();
        services.AddScoped<IStoreListPageViewModel, StoreListPageViewModel>();
        services.AddScoped<IStoreEditViewModel, StoreEditViewModel>();

        services.AddScoped<IMapper>(provider => new MapperConfiguration(cfg => ShoppingListMapperConfiguration.CreateMapping(cfg)).CreateMapper());

        services.AddScoped<IBlazorNavigationService, NavigationService>(serviceProvider =>
        {
            var aspNavigationManager = serviceProvider.GetRequiredService<NavigationManager>();
            var jsRuntime = serviceProvider.GetRequiredService<IJSRuntime>();
            var navigationService = new NavigationService(aspNavigationManager, jsRuntime);
            navigationService.RegisterForNavigation<IHomePageViewModel, HomePage>();
            navigationService.RegisterForNavigation<IShoppingItemDetailViewModel, ShoppingItemDetailPage>();
            navigationService.RegisterForNavigation<IStoreListPageViewModel, StoreListPage>();
            navigationService.RegisterForNavigation<IStoreEditViewModel, StoreEditPage>();
            return navigationService;
        });
        return services;
    }
}
