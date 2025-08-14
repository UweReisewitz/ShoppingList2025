using Microsoft.AspNetCore.Components;
using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core.UI.Blazor;

public partial class FinalExceptionPage
{
    [Inject]
    public required IBlazorNavigationService BlazorNavigationService { get; set; }

    private readonly string buttonText = SharedSettings.ApplicationPlattformType == ApplicationPlattformType.BlazorServer
        ? "Applikation Neu Laden"
        : "Applikation beenden";

    private void CloseApplication()
    {
        if (SharedSettings.ApplicationPlattformType == ApplicationPlattformType.BlazorServer)
        {
            this.BlazorNavigationService.ReloadApplication();
        }
        else
        {
            Environment.Exit(0);
        }
    }
}

