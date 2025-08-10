using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShoppingList2025.Core.Types;

namespace ShoppingList2025;

public partial class Main
{
    public Main()
    {
            
    }
    [Inject]
    public required IJSRuntime JsRuntime { get; set; }

    [Inject]
    public required IFinalExceptionHandling FinalExceptionHandling { get; set; }

    private void ProcessException(Exception ex) => this.FinalExceptionHandling.HandleException("Blazor UI", ex);

}

