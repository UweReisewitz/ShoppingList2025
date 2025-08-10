using Prism.Common;
using ShoppingList2025.Core.UI.Types;

namespace ShoppingList2025.Core.UI.Blazor;

public interface IBlazorNavigationService
{
    Stack<string> NavigationStack { get; }
    IParameters? NavigationParameters { get; }
    NavigationDirection Direction { get; }
    // Indikator für Navigation, die von Browser-Funktionalitäten (z. B. Vor-/Zurück-Button) initiiert wird und nicht von eigenem Code
    //bool IsProgrammedNavigation { get; set; }
    // Indikator für Navigation, die von eigenem Code initiiert wird und nicht von Browser-Funktionalitäten (z. B. Vor-/Zurück-Button)
    bool IsProgrammedNavigation { get; set; }
    void ReloadApplication();
    void NavigateBack(IParameters? parameters = null, bool skipQueryParameters = false);
    void NavigateTo(string url);
    void NavigateTo(string url, bool openInNewTab);
    void NavigateTo<IViewModel>(IParameters? parameters = null) where IViewModel : IBlazorViewModelBase;
    void Refresh(bool forceReload = false);
    void RegisterForNavigation<IViewModel, TView>() where IViewModel : IBlazorViewModelBase;
    string Uri { get; }
}