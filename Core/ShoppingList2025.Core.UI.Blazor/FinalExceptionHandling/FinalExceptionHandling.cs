using AsyncAwaitBestPractices;
using Microsoft.Extensions.Logging;
using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core.UI.Blazor;

public class FinalExceptionHandling(ILogger logger)
    : IFinalExceptionHandling
{
    public void InitializeSafeFireAndForget()
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

    private void LogSafeFireAndForgetException(Exception ex)
    {
        this.HandleException("SafeFireAndForgetException", ex);
    }

    public void TaskSchedulerUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        this.HandleException("UnobservedTaskException", e.Exception);
    }

    public void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        this.HandleException("UnhandledException", (Exception)e.ExceptionObject);
    }

    public /*async*/ void HandleException(string exceptionContextDescription, Exception ex)
    {
        logger.LogError(ex, "{context}", exceptionContextDescription);

        System.Diagnostics.Debugger.Break();
    }
}
