using System;

namespace ShoppingList2025.Database.Types;

public interface IStore
{
    Guid Id { get; set; }
    string Name { get; set; }
    string Address { get; set; }
}
