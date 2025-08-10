using AsyncAwaitBestPractices;
using Microsoft.AspNetCore.Components;

namespace ShoppingList2025.Core.UI.Blazor;
public partial class BlazorFinalExceptionHandling
    : ComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        AppDomain.CurrentDomain.UnhandledException += this.CurrentDomainUnhandledException;
        TaskScheduler.UnobservedTaskException += this.TaskSchedulerUnobservedTaskException;

        this.InitializeSafeFireAndForget();
    }

    private void InitializeSafeFireAndForget()
    {
        // Initialize SafeFireAndForget
        // Only use `shouldAlwaysRethrowException: true` when you want `.SafeFireAndForget()` to
        // always rethrow every exception. 
        // This is not recommended, because there is no way to catch an Exception rethrown by
        // `SafeFireAndForget()`; `shouldAlwaysRethrowException: true` should **not** be used
        // in Production/Release builds.
        SafeFireAndForgetExtensions.Initialize(shouldAlwaysRethrowException: false);

        SafeFireAndForgetExtensions.SetDefaultExceptionHandling(this.LogSafeFireAndForgetException);
    }

    private void LogSafeFireAndForgetException(Exception ex) => this.DispatchCaughtExceptionAsync(ex);

    private void TaskSchedulerUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e) => this.DispatchCaughtExceptionAsync(e.Exception);

    private void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e) => this.DispatchCaughtExceptionAsync((Exception)e.ExceptionObject);

    private async void DispatchCaughtExceptionAsync(Exception ex) => await this.InvokeAsync(() => this.DispatchExceptionAsync(ex));

}
