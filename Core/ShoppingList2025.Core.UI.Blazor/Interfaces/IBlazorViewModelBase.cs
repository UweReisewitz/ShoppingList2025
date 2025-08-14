using System.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace ShoppingList2025.Core.UI.Blazor;
public interface IBlazorViewModelBase : IBlazorNavigationAware
{
    bool IsBusy { get; set; }
    IBlazorViewInvokeAsync? BlazorComponentInvokeAsync { get; set; }
    event PropertyChangedEventHandler? PropertyChanged;
    Task SetParametersAsync(ParameterView parameters);
    void OnInitialized();
    Task OnInitializedAsync();

    Task OnParametersSetAsync();

    Task OnAfterRenderAsync(bool firstRender);
}
