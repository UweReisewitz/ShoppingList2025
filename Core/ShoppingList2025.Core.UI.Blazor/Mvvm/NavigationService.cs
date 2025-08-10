using AsyncAwaitBestPractices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using Prism.Common;
using ShoppingList2025.Core.UI.Types;

namespace ShoppingList2025.Core.UI.Blazor;

public class NavigationService : IBlazorNavigationService, IDisposable
{
    private readonly NavigationManager aspNavigationManager;
    private readonly IJSRuntime jsRuntime;
    //private readonly Stack<string> NavigationStack = new();
    private readonly Dictionary<Type, string> navigatableObjects = [];

    public Stack<string> NavigationStack { get; private set; } = new();

    public IParameters? NavigationParameters { get; private set; }

    public NavigationDirection Direction { get; private set; }

    public bool IsProgrammedNavigation { get; set; }

    public string Uri => this.aspNavigationManager.Uri;

    public NavigationService(NavigationManager aspNavigationManager,
                             IJSRuntime jsRuntime)
    {
        this.aspNavigationManager = aspNavigationManager;
        this.jsRuntime = jsRuntime;
        // Reset von IsProgrammedNavigation auf false, wenn programmierte Navigation abgeschlossen
        this.aspNavigationManager.LocationChanged += this.LocationChanged;
    }

    public void RegisterForNavigation<IViewModel, TView>() where IViewModel : IBlazorViewModelBase
    {
        var viewName = typeof(TView)
            .GetCustomAttributes(typeof(RouteAttribute), false)
            .Cast<RouteAttribute>()
            .Select(e => e.Template)
            .FirstOrDefault(e => e.Trim('/').Split('/').Length == 1)
            ?? throw new ArgumentException($"RouteAttribute not set on Type '{typeof(TView)}'");

        this.navigatableObjects.Add(typeof(IViewModel), viewName.Trim('/'));
    }

    public void NavigateTo(string url) => this.NavigateTo(url, false);

    public void NavigateTo(string url, bool openInNewTab)
    {
        if (openInNewTab)
        {
            this.OpenUrlAsync(url).SafeFireAndForget();
        }
        else
        {
            this.IsProgrammedNavigation = true;
            this.NavigationStack.Push(this.aspNavigationManager.Uri.Replace(this.aspNavigationManager.BaseUri, string.Empty));
            this.aspNavigationManager.NavigateTo(url);
        }
    }

    private async ValueTask OpenUrlAsync(string url)
    {
        await this.jsRuntime.InvokeVoidAsync("open", url, "_blank");
    }

    public void NavigateTo<IViewModel>(IParameters? parameters = null) where IViewModel : IBlazorViewModelBase
    {
        this.IsProgrammedNavigation = true;
        if (!this.navigatableObjects.TryGetValue(typeof(IViewModel), out var page))
        {
            throw new PageNotRegisteredForNavigationException(typeof(IViewModel).ToString());
        }
        parameters ??= new NavigationParameters();
        this.Direction = NavigationDirection.NavigateTo;
        this.NavigationParameters = parameters;

        var uriParameters = parameters.TryGetValue("route", out string route) ? UriParameters(route) : string.Empty;

        this.NavigationStack.Push(this.aspNavigationManager.Uri.Replace(this.aspNavigationManager.BaseUri, string.Empty));
        this.aspNavigationManager.NavigateTo(page + uriParameters);
    }

    public void ReloadApplication()
    {
        this.aspNavigationManager.NavigateTo("/", forceLoad:true, replace:true);
    }

    public void NavigateBack(IParameters? parameters = null, bool skipQueryParameters = false)
    {
        this.IsProgrammedNavigation = true;
        if (this.NavigationStack.Count != 0)
        {
            parameters ??= new NavigationParameters();
            this.Direction = NavigationDirection.NavigateBack;
            this.NavigationParameters = parameters;

            if (skipQueryParameters)
            {
                this.aspNavigationManager.NavigateTo(this.NavigationStack.Pop().Split('?').First());
            }
            else
            {
                this.aspNavigationManager.NavigateTo(this.NavigationStack.Pop());
            }
        }
    }

    public void Refresh(bool forceReload = false)
    {
        this.aspNavigationManager.Refresh(forceReload);
    }

    private static string UriParameters(object route)
    {
        return route is string routeName
            ? routeName
            : string.Empty;
    }

    private void LocationChanged(object? sender, LocationChangedEventArgs e)
    {
        // Reset IsProgrammedNavigation to false
        this.IsProgrammedNavigation = false;
    }

    private bool isDisposed;
    protected virtual void Dispose(bool disposing)
    {
        if (!this.isDisposed)
        {
            if (disposing)
            {
                this.aspNavigationManager.LocationChanged -= this.LocationChanged;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            this.isDisposed = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
