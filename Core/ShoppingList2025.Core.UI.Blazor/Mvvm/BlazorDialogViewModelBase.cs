using System.ComponentModel;
using MudBlazor;

namespace ShoppingList2025.Core.UI.Blazor;

public abstract class BlazorDialogViewModelBase : BlazorViewModelBase, IBlazorDialogViewModelBase, INotifyPropertyChanged
{
    public IMudDialogInstance? DialogInstance { get; set; }
}