using System;

namespace ShoppingList2025.Database.Types
{
    public interface IShoppingItem
    {
        Guid Id { get; set; }
        string Name { get; set; }
        ShoppingItemState State { get; set; }
        DateTime LastBought { get; set; }
        byte[]? ImageData { get; set; }
    }
}
