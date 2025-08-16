using PropertyChanged;
using ShoppingList2025.Database.Types;

namespace ShoppingList2025.Shared;

[AddINotifyPropertyChangedInterface]
public class UIStore(IStore store, bool isNewStore)
{
    public UIStore(IStore store)
        : this(store, false)
    {
    }

    public bool IsNewStore { get; } = isNewStore;

    public IStore DbStore { get; } = store;
    public Guid Id
    {
        get => this.DbStore.Id;
        set => this.DbStore.Id = value;
    }
    public string Name
    {
        get => this.DbStore.Name;
        set => this.DbStore.Name = value;
    }

    public string Address
    {
        get => this.DbStore.Address;
        set => this.DbStore.Address = value;
    }
}
