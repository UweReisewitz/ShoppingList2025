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
        switch (parameter.Symbol)
        {
            case ApplicationMessageBoxSymbol.Exclamation:
                return "color: gold;";
            case ApplicationMessageBoxSymbol.Stop:
                return "color: red;";
            case ApplicationMessageBoxSymbol.Question:
                return "color: black;";
            case ApplicationMessageBoxSymbol.Information:
                return "color: blue;";
            default:
                return "color: black;";
        }
    }
}
