namespace ShoppingList2025.Core.UI.Types;

public class PageNotRegisteredForNavigationException : Exception
{
    public PageNotRegisteredForNavigationException()
    {
    }

    public PageNotRegisteredForNavigationException(string message) : base(message)
    {
    }

    public PageNotRegisteredForNavigationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
