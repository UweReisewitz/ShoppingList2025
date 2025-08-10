using ShoppingList2025.Core.Types;
using ShoppingList2025.Database.Types;

namespace ShoppingList2025.Database
{
    public class DbServiceFactory(IPlatformSpecialFolder platformSpecialFolder) : IDbServiceFactory
    {
        private readonly IPlatformSpecialFolder platformSpecialFolder = platformSpecialFolder;

        public IDbService CreateNew()
        {
            return new DbService(this.platformSpecialFolder);
        }
    }
}
