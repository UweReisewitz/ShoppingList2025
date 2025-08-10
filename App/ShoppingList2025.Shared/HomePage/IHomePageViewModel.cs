using System.Collections.ObjectModel;
using AsyncAwaitBestPractices.MVVM;
using ShoppingList2025.Core.UI.Blazor;

namespace ShoppingList2025.Shared;

public interface IHomePageViewModel : IBlazorViewModelBase
{
    AsyncCommand AddItemCommand { get; }
    ObservableCollection<UIShoppingItem> Items { get; }
    AsyncCommand<UIShoppingItem> ItemTapped { get; }
    AsyncCommand LoadItemsCommand { get; }
    UIShoppingItem? SelectedItem { get; set; }
    AsyncCommand<UIShoppingItem> SetItemBought { get; }
    AsyncCommand ShoppingDoneCommand { get; }
}