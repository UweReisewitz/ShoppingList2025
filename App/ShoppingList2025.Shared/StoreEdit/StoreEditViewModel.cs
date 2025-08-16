using AutoMapper;
using Prism.Common;
using PropertyChanged;
using ShoppingList2025.Core.UI.Blazor;
using ShoppingList2025.Database.Types;

namespace ShoppingList2025.Shared;

[AddINotifyPropertyChangedInterface]
public class StoreEditViewModel(IBlazorNavigationService navigationService,
                                IMapper mapper,
                                IDbService dbService) 
    : BlazorViewModelBase(), IStoreEditViewModel
{
    private UIStore? uiStore;

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public async Task GotoListPageAsync()
    {
        await this.SaveItemDataAsync();
        navigationService.NavigateTo<IStoreListPageViewModel>();
    }

    public override Task OnNavigatedToAsync(IParameters? parameters = null)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        this.uiStore = (UIStore)parameters["Item"];
        mapper.Map(this.uiStore, this);

        return base.OnNavigatedToAsync(parameters);
    }

    private async Task SaveItemDataAsync()
    {
        var currentStore = this.GetStoreOrThrow();
        if (string.IsNullOrWhiteSpace(this.Name) && currentStore.IsNewStore)
        {
            dbService.RemoveStore(currentStore.DbStore);
        }
        else
        {
            var oldItem = dbService.FindStore(this.Name);
            if (oldItem != null && oldItem.Id != this.Id)
            {
                if (currentStore.IsNewStore)
                {
                    dbService.RemoveStore(currentStore.DbStore);
                }
                this.uiStore = new UIStore(oldItem);
                this.Id = this.uiStore.Id;
            }
            mapper.Map(this, this.uiStore);
            await dbService.SaveChangesAsync();
        }
    }

    private UIStore GetStoreOrThrow() => this.uiStore ?? throw new NullReferenceException(nameof(this.uiStore));
}
