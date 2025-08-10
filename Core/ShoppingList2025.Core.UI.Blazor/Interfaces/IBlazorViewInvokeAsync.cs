namespace ShoppingList2025.Core.UI.Blazor;
public interface IBlazorViewInvokeAsync
{
    Task InvokeAsync(Action workItem);
    Task InvokeAsync(Func<Task> workItem);
}
