namespace ShoppingList2025.Core.Types;
public class NotInitializedException : Exception
{
    public NotInitializedException()
    {
    }

    public NotInitializedException(string? message) : base(message)
    {
    }

    public NotInitializedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
