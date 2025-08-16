using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShoppingList2025.Database.Types;

namespace ShoppingList2025.Database.Model;

public class Store : IStore
{
    public static Store Empty => new() { IsEmpty = true };

    public Store()
    {
        this.Id = Guid.NewGuid();
    }

    [NotMapped]
    public bool IsEmpty { get; private set; }

    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
