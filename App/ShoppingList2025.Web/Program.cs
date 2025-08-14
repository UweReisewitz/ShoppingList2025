using System.Reflection;
using MudBlazor.Services;
using ShoppingList2025.Blazor;
using ShoppingList2025.Core;
using ShoppingList2025.Core.Types;
using ShoppingList2025.Core.UI.Blazor;
using ShoppingList2025.Database;
using ShoppingList2025.Shared;
using ShoppingList2025.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var logger = CoreBaseLibraryRegistrar.RegisterBlazorServerServices(builder.Services, Assembly.GetExecutingAssembly());

try
{
    logger.LogInformation("Init Blazor Server");

    builder.Services.AddSingleton<IPlatformSpecialFolder, PlatformSpecialFolderBlazor>();
    builder.Services.AddScoped<IApplicationMediaPicker, ApplicationMediaPickerBlazor>();

    builder.Services.AddUIBlazor();
    builder.Services.AddFrontend();
    builder.Services.AddUIMessageBox();
    builder.Services.AddDatabaseServices();

    builder.Services.AddMudServices();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseAntiforgery();

    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode()
        .AddAdditionalAssemblies(typeof(ShoppingList2025.Shared._Imports).Assembly);

    app.Run();
}
catch (Exception ex)
{
    logger.LogError(ex, "Exception during Blazor Server init");
    throw;
}
