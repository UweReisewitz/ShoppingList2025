using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingList2025.Database.Types
{
    public interface IDbService
    {
        Task CreateOrMigrateDatabaseAsync();

        void SaveChanges();
        Task SaveChangesAsync();

        Task<List<IShoppingItem>> GetShoppingListItemsAsync();
        Task<List<IStore>> GetStoresAsync();

        Task AddStoreAsync(IStore store);
        IStore CreateStore();
        void RemoveStore(IStore store);
        
        Task AddShoppingItemAsync(IShoppingItem item);
        IShoppingItem CreateShoppingItem();
        void RemoveShoppingItem(IShoppingItem item);
        Task EndShoppingAsync();

        IStore? FindStore(string name);
        Task<IStore?>FindStoreAsync(string name);

        IShoppingItem? FindShoppingItem(string name);
        Task<IShoppingItem?> FindShoppingItemAsync(string name);

        Task<List<string>> GetSuggestedNamesAsync(string name);
    }
}
