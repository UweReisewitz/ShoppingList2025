using MudBlazor;

namespace ShoppingList2025.Core.UI.Blazor;
public interface IBlazorDialogViewModelBase : IBlazorViewModelBase
{
    IMudDialogInstance? DialogInstance { get; set; }
}
