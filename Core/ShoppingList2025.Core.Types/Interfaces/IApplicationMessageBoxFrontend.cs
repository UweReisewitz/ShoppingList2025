namespace ShoppingList2025.Core.Types;

public interface IApplicationMessageBoxFrontend
{
    IApplicationMessageBoxParameter CreateMessageBoxParameter();
    Task ShowErrorMessageAsync(string errorMessage);
    Task ShowErrorMessageAsync(string title, string errorMessage);
    Task ShowMessageAsync(string message, ApplicationMessageBoxMessageMode messageMode);
    Task ShowMessageAsync(string title, string message, ApplicationMessageBoxMessageMode messageMode);
}
