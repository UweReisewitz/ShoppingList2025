namespace ShoppingList2025.Core.Types;

public interface IApplicationMessageBoxParameter
{
    string Title { get; set; }
    ApplicationMessageBoxButtons Buttons { get; set; }
    ApplicationMessageBoxButtons CancelButton { get; set; }
    ApplicationMessageBoxButtons DefaultButton { get; set; }
    string Prompt { get; set; }
    ApplicationMessageBoxSymbol Symbol { get; set; }
}
