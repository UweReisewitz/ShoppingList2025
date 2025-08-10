namespace ShoppingList2025.Shared;

public partial class HomePage
{
    private static string GetBackgroundStyle(UIShoppingItem item) => item.State == Database.Types.ShoppingItemState.Open
            ? "background:yellow"
            : "background:lightgreen";
}
