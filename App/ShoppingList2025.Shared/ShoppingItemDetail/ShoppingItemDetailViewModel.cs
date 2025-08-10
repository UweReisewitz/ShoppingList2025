using AutoMapper;
using Prism.Common;
using PropertyChanged;
using ShoppingList2025.Core.Types;
using ShoppingList2025.Core.UI.Blazor;
using ShoppingList2025.Database.Types;

namespace ShoppingList2025.Shared;

[AddINotifyPropertyChangedInterface]
public class ShoppingItemDetailViewModel(IBlazorNavigationService navigationService,
                                         IMainAssembly mainAssembly,
                                         IMapper mapper,
                                         IApplicationMediaPicker applicationMediaPicker,
                                         IDbService dbService) 
    : BlazorViewModelBase(), IShoppingItemDetailViewModel
{
    private UIShoppingItem? uiShoppingItem;

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    private ShoppingItemState itemState;
    public ShoppingItemState State
    {
        get => this.itemState;
        set
        {
            if (value != this.itemState)
            {
                if (this.itemState == ShoppingItemState.Open)
                {
                    this.LastBought = DateTime.Now;
                }
                this.itemState = value;
            }
        }
    }

    public IList<EnumValueDescription> ItemStateList => typeof(ShoppingItemState).ToList();

    [DependsOn(nameof(State))]
    public EnumValueDescription ItemState
    {
        get => new(this.State, this.State.GetDescription());
        set => this.State = (ShoppingItemState)value.EnumValue;
    }

    public bool IsLastBoughtVisible => this.LastBought != DateTime.MinValue;
    public DateTime LastBought { get; set; }


    public async Task UpdateSuggestedNamesAsync(string? value)
    {
        this.SuggestedNames = value != null
            ? await dbService.GetSuggestedNamesAsync(value)
            : [];
    }

    public List<string> SuggestedNames { get; private set; } = [];

    public async Task GotoHomePageAsync()
    {
        await this.SaveItemDataAsync();
        navigationService.NavigateTo<IHomePageViewModel>();
    }

    public async Task TakePhotoAsync()
    {
        if (!this.IsBusy)
        {
            this.IsBusy = true;

            var applicationMediaPickerOptions = new ApplicationMediaPickerOptions
            {
                Title = mainAssembly.ProductName,                
            };
            var fileName = await applicationMediaPicker.CapturePhotoAsync(applicationMediaPickerOptions);

            if (string.IsNullOrEmpty(fileName))
            {
                this.IsBusy = false;
                return;
            }

            this.ImageData = ConvertFileToByteArray(fileName);
            this.IsBusy = false;
        }
    }

    public async Task PickPhotoAsync()
    {
        if (!this.IsBusy)
        {
            this.IsBusy = true;
            var applicationMediaPickerOptions = new ApplicationMediaPickerOptions
            {
                Title = mainAssembly.ProductName
            };
            var fileName = await applicationMediaPicker.PickPhotoAsync(applicationMediaPickerOptions);

            if (fileName == null)
            {
                this.IsBusy = false;
                return;
            }

            this.ImageData = ConvertFileToByteArray(fileName);
            this.IsBusy = false;
        }
    }

    private static byte[] ConvertFileToByteArray(string fileName)
    {
        return !File.Exists(fileName)
            ? throw new FileNotFoundException($"File '{fileName}' not found for conversion to byte array")
            : File.ReadAllBytes(fileName);
    }

    public byte[] ImageData { get; set; } = [];
    public bool IsImageVisible => this.ImageData.Length > 0;
    public string ImageSource
    {
        get
        {
            var imageString = Convert.ToBase64String(this.ImageData);
            return $"data:image/bmp;base64, {imageString}";
        }
    }

    public override Task OnNavigatedToAsync(IParameters? parameters = null)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        this.uiShoppingItem = (UIShoppingItem)parameters["Item"];
        mapper.Map(this.uiShoppingItem, this);

        return base.OnNavigatedToAsync(parameters);
    }

    private async Task SaveItemDataAsync()
    {
        var currentShoppingItem = this.GetShoppingItemOrThrow();
        if (string.IsNullOrWhiteSpace(this.Name) && currentShoppingItem.IsNewShoppingItem)
        {
            dbService.RemoveShoppingItem(currentShoppingItem.DbShoppingItem);
        }
        else
        {
            var oldItem = dbService.FindShoppingItem(this.Name);
            if (oldItem != null && oldItem.Id != this.Id)
            {
                if (currentShoppingItem.IsNewShoppingItem)
                {
                    dbService.RemoveShoppingItem(currentShoppingItem.DbShoppingItem);
                    this.LastBought = oldItem.LastBought;
                    this.ImageData = oldItem.ImageData ?? [];
                }
                this.uiShoppingItem = new UIShoppingItem(oldItem);
                this.Id = this.uiShoppingItem.Id;
            }
            mapper.Map(this, this.uiShoppingItem);
            await dbService.SaveChangesAsync();
        }
    }

    private UIShoppingItem GetShoppingItemOrThrow() => this.uiShoppingItem ?? throw new NullReferenceException(nameof(this.uiShoppingItem));
}
