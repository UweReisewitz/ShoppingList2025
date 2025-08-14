using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShoppingList2025.Core.Types;
using ShoppingList2025.Core.UI.Types;

namespace ShoppingList2025.Core.UI.Blazor;

/// <summary>
/// Base class for razor components.
/// </summary>
/// <typeparam name="T">Type of the view model.</typeparam>
public abstract class BlazorViewBase<T> : ComponentBase, IBlazorViewInvokeAsync, IDisposable
    where T : IBlazorViewModelBase
{
    [Inject]
    public required IIocContainerProvider ContainerProvider { get; set; }
    [Inject]
    public required IBlazorNavigationService NavigationService { get; set; }

    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    /// <summary>
    /// Binding data context.
    /// </summary>
    /// We initialize it to default(T)! ONLY because it will be set during OnInitialized().
    /// After that it has a value or OnInitialized()=>SetBindingContext() has failed because
    /// of a missing Service injection
    /// 
    /// This spares us tons of ViewModel!-Statements
    protected T ViewModel { get; set; } = default(T)!;

    protected string GetImagePath(string imageResourceName) => $"_content/{Path.GetFileNameWithoutExtension(this.GetType().Assembly.Location)}/Images/{imageResourceName}";


    #region Internal methods

    protected void SetBindingContext()
    {
        if (this.ViewModel == null)
        {
            this.ViewModel = this.ContainerProvider.GetService<T>();
            this.ViewModel.PropertyChanged += this.ViewModel_PropertyChanged;
            this.ViewModel.BlazorComponentInvokeAsync = this;
        }
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        this.InvokeAsync(this.StateHasChanged);
    }

    Task IBlazorViewInvokeAsync.InvokeAsync(Action workItem) => this.InvokeAsync(workItem);

    Task IBlazorViewInvokeAsync.InvokeAsync(Func<Task> workItem) => this.InvokeAsync(workItem);


    #endregion

    #region Lifecycle methods

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        await this.ViewModel.OnParametersSetAsync();
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        await this.ViewModel.SetParametersAsync(parameters);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        this.SetBindingContext();
        this.ViewModel?.OnInitialized();
    }

    protected override async Task OnInitializedAsync()
    {
        await this.ViewModel.OnInitializedAsync();
        if (this.NavigationService!.Direction == NavigationDirection.NavigateTo)
        {
            await this.ViewModel.OnNavigatedToAsync(this.NavigationService.NavigationParameters);
        }
        else
        {
            await this.ViewModel.OnNavigatedFromAsync(this.NavigationService.NavigationParameters);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            var attribute = this.GetType().GetCustomAttribute<InitialFocusAttribute>();
            if (attribute != null)
            {
                await this.JSRuntime!.InvokeVoidAsync("focusComponentByName", attribute.FocusableObjectName);
            }
        }
        await this.ViewModel.OnAfterRenderAsync(firstRender);
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
            this.ViewModel.BlazorComponentInvokeAsync = null;
        }
    }

    ~BlazorViewBase()
    {
        this.Dispose(false);
    }

    #endregion
}