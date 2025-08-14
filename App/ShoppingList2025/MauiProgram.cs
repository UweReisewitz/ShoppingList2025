using System.Reflection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using ShoppingList2025.Core;
using ShoppingList2025.Core.Types;
using ShoppingList2025.Core.UI.Blazor;
using ShoppingList2025.Core.UI.Maui;
using ShoppingList2025.Database;
using ShoppingList2025.Shared;

namespace ShoppingList2025;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        AppContext.SetSwitch("System.Reflection.NullabilityInfoContext.IsSupported", true);

        var builder = MauiApp.CreateBuilder();

        var logger = CoreBaseLibraryRegistrar.RegisterMauiServices(builder.Services, Assembly.GetExecutingAssembly(), ApplicationPlattformType.BlazorMaui);

        logger.LogInformation("Startup MAUI-App");

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"));

        builder.Services.AddSingleton<IPlatformSpecialFolder, PlatformSpecialFolder>();
        builder.Services.AddScoped<IApplicationMediaPicker, ApplicationMediaPickerMaui>();

        builder.Services.AddUIBlazor();
        builder.Services.AddFrontend();
        builder.Services.AddUIMessageBox();
        builder.Services.AddDatabaseServices();

        builder.Services.AddMudServices();

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();

        builder.Services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true,
            ValidateOnBuild = true
        });
#endif

        return builder.Build();
    }
}

