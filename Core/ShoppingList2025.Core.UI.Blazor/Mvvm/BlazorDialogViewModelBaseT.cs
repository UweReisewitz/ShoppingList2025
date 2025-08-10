using System.ComponentModel;

namespace ShoppingList2025.Core.UI.Blazor;

public abstract class BlazorDialogViewModelBase<U> : BlazorDialogViewModelBase, IBlazorDialogViewModelBase<U>, INotifyPropertyChanged
{
    public required U Parameter { get; set; }
}