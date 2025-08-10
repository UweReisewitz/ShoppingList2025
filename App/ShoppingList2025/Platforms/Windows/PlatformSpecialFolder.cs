using ShoppingList2025.Core.Types;

namespace ShoppingList2025;
public class PlatformSpecialFolder : IPlatformSpecialFolder
{
    public string ApplicationData
    {
        get
        {
            var retval = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ShoppingList2025");
            Directory.CreateDirectory(retval);
            return retval;
        }
    }
}