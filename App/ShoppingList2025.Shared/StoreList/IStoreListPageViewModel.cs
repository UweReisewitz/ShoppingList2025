using System.Collections.ObjectModel;
using ShoppingList2025.Core.UI.Blazor;

namespace ShoppingList2025.Shared
{
    public interface IStoreListPageViewModel : IBlazorViewModelBase
    {
        Task GotoHomePageAsync();
        Task AddItemAsync();
        ObservableCollection<UIStore> Items { get; }
        Task SelectItemAsync(UIStore store);
        Task LoadItemsAsync();
        UIStore? SelectedItem { get; set; }

    }
}