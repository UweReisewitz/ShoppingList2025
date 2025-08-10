namespace ShoppingList2025.Core.Types;

public interface IIocContainerProvider
{
    T GetService<T>();
    T GetService<T>(Type typeToResolve);
    IEnumerable<T> GetServices<T>();
}
