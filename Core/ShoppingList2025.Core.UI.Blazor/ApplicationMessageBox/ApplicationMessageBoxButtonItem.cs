using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core.UI.Blazor;

public class ApplicationMessageBoxButtonItem
{
    public string Text { get; set; } = string.Empty;
    public ApplicationMessageBoxButtons ButtonType { get; set; }
}
