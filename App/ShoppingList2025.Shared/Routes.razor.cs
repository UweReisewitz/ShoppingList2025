using Microsoft.AspNetCore.Components;
using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Shared;

public partial class Routes
{
    [Inject]
    public required IFinalExceptionHandling FinalExceptionHandling { get; set; }

    private void ProcessException(Exception ex) => this.FinalExceptionHandling.HandleException("Blazor UI", ex);

}
