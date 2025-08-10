using Prism.Common;

namespace ShoppingList2025.Core.UI.Blazor;
public interface IBlazorNavigationAware
{
    Task OnNavigatedToAsync(IParameters? parameters = null);
    Task OnNavigatedFromAsync(IParameters? parameters = null);
}
