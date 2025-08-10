using System.Collections.ObjectModel;
using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using Microsoft.Extensions.Logging;
using Prism.Common;
using PropertyChanged;
using ShoppingList2025.Core.Types;
using ShoppingList2025.Core.UI.Blazor;
using ShoppingList2025.Core.UI.Types;
using ShoppingList2025.Database.Types;

namespace ShoppingList2025.Shared;

[AddINotifyPropertyChangedInterface]
public class HomePageViewModel : BlazorViewModelBase, IHomePageViewModel
{
    private readonly IDbService dbService;
    private readonly IBlazorNavigationService navigationService;
    private readonly ILogger logger;
    private readonly IApplicationMessageBoxFrontend messageBoxFrontend;
    private bool isDatabaseMigrated;

    public HomePageViewModel(IBlazorNavigationService navigationService,
                             IDbService dbService,
                             ILogger logger,
                             IApplicationMessageBoxFrontend messageBoxFrontend)
        : base()
    {
        this.dbService = dbService;

        this.Items = [];
        this.LoadItemsCommand = new AsyncCommand(this.ExecuteLoadItemsCommandAsync);

        this.SetItemBought = new AsyncCommand<UIShoppingItem>(this.SetItemBoughtAsync);
        this.ItemTapped = new AsyncCommand<UIShoppingItem>(this.OnItemSelectedAsync);

        this.AddItemCommand = new AsyncCommand(this.OnAddItemAsync);
        this.ShoppingDoneCommand = new AsyncCommand(this.ShoppingDoneCommandAsync);
        this.navigationService = navigationService;
        this.logger = logger;
        this.messageBoxFrontend = messageBoxFrontend;
    }

    public AsyncCommand ShoppingDoneCommand { get; }
    private async Task ShoppingDoneCommandAsync()
    {
        await this.dbService.EndShoppingAsync();
        await this.ExecuteLoadItemsCommandAsync();
    }


    public AsyncCommand<UIShoppingItem> SetItemBought { get; }
    private async Task SetItemBoughtAsync(UIShoppingItem? item)
    {
        if (item != null && item.State == ShoppingItemState.Open)
        {
            item.State = ShoppingItemState.Bought;
            item.LastBought = DateTime.Now;
            await this.dbService.SaveChangesAsync();
        }
    }


    public ObservableCollection<UIShoppingItem> Items { get; private set; }
    public AsyncCommand LoadItemsCommand { get; }
    public AsyncCommand AddItemCommand { get; }
    public AsyncCommand<UIShoppingItem> ItemTapped { get; }

    private async Task ExecuteLoadItemsCommandAsync()
    {
        this.IsBusy = true;

        try
        {
            this.Items.Clear();
            var items = await this.dbService.GetShoppingListItemsAsync();

            foreach (var item in items.OrderBy(i => i.State).ThenBy(i => i.Name))
            {
                this.Items.Add(new UIShoppingItem(item));
            }
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "ExecuteLoadItemsCommandAsync");
            await this.messageBoxFrontend.ShowErrorMessageAsync("ExecuteLoadItemsCommandAsync", $"Exception aufgetreten:\n{ex.GetType()}\n{ex.Message}");
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
        set => this.OnItemSelectedAsync(value).SafeFireAndForget();
    }

    private async Task OnAddItemAsync()
    {
        var dbShoppingItem = this.dbService.CreateShoppingItem();
        await this.dbService.AddShoppingItemAsync(dbShoppingItem);
        var uiShoppingItem = new UIShoppingItem(dbShoppingItem, true);

        await this.NavigateToDetailPageAsync(uiShoppingItem);
    }

    private async Task OnItemSelectedAsync(UIShoppingItem? item)
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
        this.navigationService.NavigateTo<IShoppingItemDetailViewModel>(parameters);
        return Task.CompletedTask;
    }

    public override async Task OnNavigatedToAsync(IParameters? parameters = null)
    {
        await this.ExecuteLoadItemsCommandAsync();
    }

    public override async Task OnInitializedAsync()
    {
        this.SelectedItem = null;

        if (!this.isDatabaseMigrated)
        {
            await this.dbService.CreateOrMigrateDatabaseAsync();
            this.isDatabaseMigrated = true;
        }
    }
}