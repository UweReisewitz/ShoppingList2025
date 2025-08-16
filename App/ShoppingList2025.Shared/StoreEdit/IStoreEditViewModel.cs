using ShoppingList2025.Core.UI.Blazor;

namespace ShoppingList2025.Shared
{
    public interface IStoreEditViewModel : IBlazorViewModelBase
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Address { get; set; }
        Task GotoListPageAsync();
    }
}