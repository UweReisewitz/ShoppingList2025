using PropertyChanged;
using ShoppingList2025.Core.Types;
using ShoppingList2025.Database.Types;

namespace ShoppingList2025.Shared;

[AddINotifyPropertyChangedInterface]
public class UIShoppingItem(IShoppingItem shoppingItem, bool isNewShoppingItem)
{
    public UIShoppingItem(IShoppingItem shoppingItem)
        : this(shoppingItem, false)
    {
    }

    public bool IsNewShoppingItem { get; } = isNewShoppingItem;

    public IShoppingItem DbShoppingItem { get; } = shoppingItem;
    public Guid Id
    {
        get => this.DbShoppingItem.Id;
        set => this.DbShoppingItem.Id = value;
    }
    public string Name
    {
        get => this.DbShoppingItem.Name;
        set => this.DbShoppingItem.Name = value;
    }
    public ShoppingItemState State
    {
        get => this.DbShoppingItem.State;
        set => this.DbShoppingItem.State = value;
    }
    public DateTime LastBought
    {
        get => this.DbShoppingItem.LastBought;
        set => this.DbShoppingItem.LastBought = value;
    }

    private string imagesrc => this.ImageData.Length > 0
        ? Convert.ToBase64String(this.ImageData)
        : string.Empty;

    public byte[] ImageData
    {
        get => this.DbShoppingItem.ImageData ?? [];
        set => this.DbShoppingItem.ImageData = value;
    }

    [DependsOn(nameof(State))]
    public string StateText => this.State.GetDescription();

    [DependsOn(nameof(ImageData))]
    public string ImageDataUrl => this.ImageData.Length > 0
        ? $"data:image/bmp;base64,{this.imagesrc}"
        : string.Empty;

    [DependsOn(nameof(State))]
    public bool IsOpen => this.State == ShoppingItemState.Open;

    [DependsOn(nameof(State))]
    public bool IsBought => this.State == ShoppingItemState.Bought;
}
