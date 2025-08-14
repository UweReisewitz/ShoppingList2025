namespace ShoppingList2025.Core.Types;

public interface IFinalExceptionHandling
{
    void InitializeSafeFireAndForget();
    void TaskSchedulerUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e);
    void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e);

    void HandleException(string logExceptionName, Exception ex);
}
