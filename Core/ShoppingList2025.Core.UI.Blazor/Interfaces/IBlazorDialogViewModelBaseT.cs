namespace ShoppingList2025.Core.UI.Blazor;
public interface IBlazorDialogViewModelBase<U> : IBlazorDialogViewModelBase
{
    U Parameter { get; set; }
}
