using System.Collections.ObjectModel;
using AsyncAwaitBestPractices.MVVM;
using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core.UI.Blazor;

public interface IApplicationMessageBoxViewModel : IBlazorDialogViewModelBase
{
    Task InitializeAsync(IApplicationMessageBoxParameter parameter);
    bool IsIconVisible { get; }
    string Prompt { get; }
    IAsyncCommand<ApplicationMessageBoxButtons> MessageBoxButtonCommand { get; }
    ObservableCollection<ApplicationMessageBoxButtonItem> MessageBoxButtonItems { get; }
    string Title { get; }
}
