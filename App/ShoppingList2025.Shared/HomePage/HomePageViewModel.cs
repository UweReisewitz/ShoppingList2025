using System.Collections.ObjectModel;
using AsyncAwaitBestPractices;
using Microsoft.Extensions.Logging;
using Prism.Common;
using PropertyChanged;
using ShoppingList2025.Core.Types;
using ShoppingList2025.Core.UI.Blazor;
using ShoppingList2025.Core.UI.Types;
using ShoppingList2025.Database.Types;

namespace ShoppingList2025.Shared;

[AddINotifyPropertyChangedInterface]
public class HomePageViewModel(IBlazorNavigationService navigationService,
                               IDbService dbService,
                               ILogger logger,
                               IApplicationMessageBoxFrontend messageBoxFrontend)
    : BlazorViewModelBase(), IHomePageViewModel
{
    private bool isDatabaseMigrated;

    public async Task ShoppingDoneAsync()
    {
        await dbService.EndShoppingAsync();
        await this.LoadItemsAsync();
    }

    public async Task SetItemBoughtAsync(UIShoppingItem? item)
    {
        if (item != null && item.State == ShoppingItemState.Open)
        {
            item.State = ShoppingItemState.Bought;
            item.LastBought = DateTime.Now;
            await dbService.SaveChangesAsync();
        }
    }


    public ObservableCollection<UIShoppingItem> Items { get; private set; } = [];
    public async Task LoadItemsAsync()
    {
        this.IsBusy = true;

        try
        {
            this.Items.Clear();
            var items = await dbService.GetShoppingListItemsAsync();

            foreach (var item in items.OrderBy(i => i.State).ThenBy(i => i.Name))
            {
                this.Items.Add(new UIShoppingItem(item));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(LoadItemsAsync));
            await messageBoxFrontend.ShowErrorMessageAsync(nameof(LoadItemsAsync), $"Exception aufgetreten:\n{ex.GetType()}\n{ex.Message}");
        }
        finally
        {
            this.IsBusy = false;
        }
    }

    private UIShoppingItem? selectedItem;
    public UIShoppingItem? SelectedItem
    {
        get => this.selectedItem;
        set => this.SelectItemAsync(value).SafeFireAndForget();
    }

    public async Task AddItemAsync()
    {
        var dbShoppingItem = dbService.CreateShoppingItem();
        await dbService.AddShoppingItemAsync(dbShoppingItem);
        var uiShoppingItem = new UIShoppingItem(dbShoppingItem, true);

        await this.NavigateToDetailPageAsync(uiShoppingItem);
    }

    public async Task SelectItemAsync(UIShoppingItem? item)
    {
        this.selectedItem = item;
        if (item != null)
        {
            await this.NavigateToDetailPageAsync(item);
        }
    }

    private Task NavigateToDetailPageAsync(UIShoppingItem uiShoppingItem)
    {
        var parameters = new NavigationParameters
            {
                { "Item", uiShoppingItem }
            };

        // This will push the ShoppingItemDetailPage onto the navigation stack
        navigationService.NavigateTo<IShoppingItemDetailViewModel>(parameters);
        return Task.CompletedTask;
    }

    public override async Task OnNavigatedToAsync(IParameters? parameters = null)
    {
        await this.LoadItemsAsync();
    }

    public override async Task OnInitializedAsync()
    {
        this.SelectedItem = null;

        if (!this.isDatabaseMigrated)
        {
            await dbService.CreateOrMigrateDatabaseAsync();
            this.isDatabaseMigrated = true;
        }
    }

    public Task GotoStoreListAsync()
    {
        navigationService.NavigateTo<IStoreListPageViewModel>();
        return Task.CompletedTask;
    }
}