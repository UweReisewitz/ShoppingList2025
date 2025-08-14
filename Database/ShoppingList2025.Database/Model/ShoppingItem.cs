using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShoppingList2025.Database.Types;

namespace ShoppingList2025.Database.Model;

public class ShoppingItem : IShoppingItem
{
    public static ShoppingItem Empty => new() { IsEmpty = true };

    public ShoppingItem()
    {
        this.Id = Guid.NewGuid();
    }

    [NotMapped]
    public bool IsEmpty { get; private set; }

    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "int")]
    public ShoppingItemState State { get; set; }

    public DateTime LastBought { get; set; }

    [Column(TypeName = "BLOB")]
    public byte[]? ImageData { get; set; }
}
