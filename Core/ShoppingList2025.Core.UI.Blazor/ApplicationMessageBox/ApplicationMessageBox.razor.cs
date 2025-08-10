using Microsoft.AspNetCore.Components;
using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core.UI.Blazor;

public partial class ApplicationMessageBox
{
    [Parameter]
    public required IApplicationMessageBoxParameter parameter { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await this.ViewModel.InitializeAsync(this.parameter);
    }


    private string IconColorStyle()
    {
        return this.parameter.Symbol switch
        {
            ApplicationMessageBoxSymbol.Exclamation => "color: gold;",
            ApplicationMessageBoxSymbol.Stop => "color: red;",
            ApplicationMessageBoxSymbol.Question => "color: black;",
            ApplicationMessageBoxSymbol.Information => "color: blue;",
            _ => "color: black;",
        };
    }
}
