using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ShoppingList2025.Core.UI.Blazor;

/// <summary>
/// Base class for razor components.
/// </summary>
/// <typeparam name="T">Type of the view model.</typeparam>
public abstract class BlazorDialogViewBase<T> : BlazorViewBase<T>, IDisposable
    where T : IBlazorDialogViewModelBase
{
    [CascadingParameter]
    public required IMudDialogInstance DialogInstance { get; set; }

    protected virtual Task OnSubmitAsync()
    {
        this.DialogInstance.Close();
        return Task.CompletedTask;
    }

    protected virtual Task OnCancelAsync()
    {
        this.DialogInstance.Close(DialogResult.Cancel());
        return Task.CompletedTask;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }

    #region Lifecycle methods

    protected override void OnInitialized()
    {
        base.OnInitialized();
        this.SetBindingContext();
        this.ViewModel.DialogInstance = this.DialogInstance;
        this.ViewModel.OnInitialized();
    }

    #endregion
}