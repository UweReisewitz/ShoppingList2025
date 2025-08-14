using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core.UI.Blazor;

public class ApplicationMessageBoxParameter(string productName) : IApplicationMessageBoxParameter
{
    public string Title { get; set; } = productName;
    public ApplicationMessageBoxButtons Buttons { get; set; }
    public ApplicationMessageBoxButtons CancelButton { get; set; }
    public ApplicationMessageBoxButtons DefaultButton { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public ApplicationMessageBoxSymbol Symbol { get; set; }
}
