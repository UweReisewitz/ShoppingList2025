using System.Collections.ObjectModel;
using ShoppingList2025.Core.UI.Blazor;

namespace ShoppingList2025.Shared;

public interface IHomePageViewModel : IBlazorViewModelBase
{
    Task AddItemAsync();
    ObservableCollection<UIShoppingItem> Items { get; }
    Task SelectItemAsync(UIShoppingItem shoppingItem);
    Task LoadItemsAsync();
    UIShoppingItem? SelectedItem { get; set; }
    Task SetItemBoughtAsync(UIShoppingItem shoppingItem);
    Task ShoppingDoneAsync();
}