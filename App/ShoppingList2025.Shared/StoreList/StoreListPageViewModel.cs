using System.Collections.ObjectModel;
using AsyncAwaitBestPractices;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Prism.Common;
using PropertyChanged;
using ShoppingList2025.Core.Types;
using ShoppingList2025.Core.UI.Blazor;
using ShoppingList2025.Core.UI.Types;
using ShoppingList2025.Database.Types;

namespace ShoppingList2025.Shared;

[AddINotifyPropertyChangedInterface]
public class StoreListPageViewModel(IBlazorNavigationService navigationService,
                                ILogger logger,
                                IApplicationMessageBoxFrontend messageBoxFrontend,
                                IDbService dbService)
    : BlazorViewModelBase(), IStoreListPageViewModel
{
    public ObservableCollection<UIStore> Items { get; private set; } = [];

    private UIStore? selectedItem;
    public UIStore? SelectedItem
    {
        get => this.selectedItem;
        set => this.SelectItemAsync(value).SafeFireAndForget();
    }

    public override async Task OnNavigatedToAsync(IParameters? parameters = null)
    {
        await this.LoadItemsAsync();
        await base.OnNavigatedToAsync(parameters);
    }

    public async Task AddItemAsync()
    {
        var dbStore = dbService.CreateStore();
        await dbService.AddStoreAsync(dbStore);
        var uiStore = new UIStore(dbStore, true);

        await this.NavigateToStoreEditPageAsync(uiStore);
    }

    public async Task SelectItemAsync(UIStore? item)
    {
        this.selectedItem = item;
        if (item != null)
        {
            await this.NavigateToStoreEditPageAsync(item);
        }
    }

    private Task NavigateToStoreEditPageAsync(UIStore store)
    {
        var parameters = new NavigationParameters
            {
                { "Item", store }
            };

        // This will push the ShoppingItemDetailPage onto the navigation stack
        navigationService.NavigateTo<IStoreEditViewModel>(parameters);
        return Task.CompletedTask;
    }


    public async Task LoadItemsAsync()
    {
        this.IsBusy = true;

        try
        {
            this.Items.Clear();
            var items = await dbService.GetStoresAsync();

            foreach (var item in items.OrderBy(i => i.Name))
            {
                this.Items.Add(new UIStore(item));
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

    public Task GotoHomePageAsync()
    {
        navigationService.NavigateTo<IHomePageViewModel>();
        return Task.CompletedTask;
    }
}
