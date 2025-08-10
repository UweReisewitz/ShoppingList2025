using System;
using System.IO;
using ShoppingList2025.Core.Types;

namespace ShoppingListMaui.Core
{
    public class PlatformSpecialFolder : IPlatformSpecialFolder
    {
        public string ApplicationData
        {
            get
            {
                var retval = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ShoppingList");
                Directory.CreateDirectory(retval);
                return retval;
            }
        }
    }
}
