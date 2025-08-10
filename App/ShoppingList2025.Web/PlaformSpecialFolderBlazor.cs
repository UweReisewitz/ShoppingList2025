using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Blazor;

public class PlatformSpecialFolderBlazor(IMainAssembly mainAssembly) : IPlatformSpecialFolder
{
    public string ApplicationData
    {
        get
        {
            var retval = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), mainAssembly.ProductName);
            Directory.CreateDirectory(retval);
            return retval;
        }
    }
}
