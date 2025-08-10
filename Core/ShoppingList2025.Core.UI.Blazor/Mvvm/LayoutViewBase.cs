using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core.UI.Blazor;

/// <summary>
/// Base view for the layout components.
/// </summary>
/// <typeparam name="T">Type of the view model.</typeparam>
public class LayoutViewBase<T> : LayoutComponentBase, IDisposable
    where T : BlazorViewModelBase
{
    private ILogger<LayoutViewBase<T>>? logger;

    protected LayoutViewBase()
    {
    }

    protected LayoutViewBase(IIocContainerProvider containerProvider)
    {
        this.ContainerProvider = containerProvider;
    }

    /// <summary>
    /// Binding data context.
    /// </summary>
    protected T? ViewModel { get; set; }

    /// <summary>
    /// Injectable service provider.
    /// </summary>
    [Inject]
    public IIocContainerProvider? ContainerProvider { get; set; }

    #region Internal methods

    private void ConfigureBindingContext()
    {
        this.ViewModel = this.ContainerProvider!.GetService<T>();
        this.ViewModel.PropertyChanged += this.ViewModel_PropertyChanged;
        this.logger = this.ContainerProvider.GetService<ILogger<LayoutViewBase<T>>>();
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        this.logger?.LogTrace("Raised property changed event {PropertyName}", e.PropertyName);
        this.StateHasChanged();
    }

    #endregion

    #region Lifecycle methods

    protected override void OnInitialized()
    {
        base.OnInitialized();
        this.ConfigureBindingContext();
        this.ViewModel?.OnInitialized();
    }

    protected override Task OnInitializedAsync()
    {
        return this.ViewModel?.OnInitializedAsync() ?? Task.CompletedTask;
    }

    protected override Task OnParametersSetAsync()
    {
        return this.ViewModel?.OnParametersSetAsync() ?? Task.CompletedTask;
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        return this.ViewModel?.OnAfterRenderAsync(firstRender) ?? Task.CompletedTask;
    }

    protected override bool ShouldRender()
    {
        return this.ViewModel?.ShouldRender() ?? true;
    }

    #endregion

    #region Implementation of IDisposable

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        if (this.ViewModel != null)
        {
            this.ViewModel.PropertyChanged -= this.ViewModel_PropertyChanged;
        }

        this.logger?.LogTrace("Disposed view of the {ClassName}", typeof(T).Name);
    }

    ~LayoutViewBase()
    {
        this.Dispose(false);
    }

    #endregion
}