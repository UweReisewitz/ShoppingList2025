using System.Reflection;

namespace ShoppingList2025.Shared;

public class AssemblyList
{
    public static Assembly[] AdditionalAssemblies =>
    [
        typeof(MainLayout).Assembly,
    ];
}
