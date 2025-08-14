using System.Collections.ObjectModel;
using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core.UI.Blazor;

public interface IApplicationMessageBoxViewModel : IBlazorDialogViewModelBase
{
    Task InitializeAsync(IApplicationMessageBoxParameter parameter);
    bool IsIconVisible { get; }
    string Prompt { get; }
    Task PerformMessageBoxButtonAsync(ApplicationMessageBoxButtons buttons);
    ObservableCollection<ApplicationMessageBoxButtonItem> MessageBoxButtonItems { get; }
    string Title { get; }
}
