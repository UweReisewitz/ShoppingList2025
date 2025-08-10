using Microsoft.Extensions.DependencyInjection;
using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core;
public class IocContainerProvider(IServiceProvider scopedServiceProvider) 
    : IIocContainerProvider
{
    public T GetService<T>()
    {
        var service = scopedServiceProvider.GetService<T>();
        return service == null ? throw new NullReferenceException() : service;
    }

    public T GetService<T>(Type typeToResolve) => scopedServiceProvider.GetServices<T>().First(o => o != null && o.GetType() == typeToResolve);

    public IEnumerable<T> GetServices<T>() => scopedServiceProvider.GetServices<T>();
}
