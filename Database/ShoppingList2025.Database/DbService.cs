using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingList2025.Core.Types;
using ShoppingList2025.Database.Model;
using ShoppingList2025.Database.Types;

namespace ShoppingList2025.Database
{
    public class DbService(IPlatformSpecialFolder platformSpecialFolder)
        : IDbService, IDisposable
    {
        private readonly LocalDbContext localContext = new(platformSpecialFolder);

        public async Task CreateOrMigrateDatabaseAsync()
        {
            using (var dbContext = new LocalDbContext(platformSpecialFolder))
            {
                await dbContext.CreateOrMigrateDatabaseAsync();
            }
        }

        public void SaveChanges() => this.localContext.SaveChanges();

        public Task SaveChangesAsync() => this.localContext.SaveChangesAsync();

        public Task<List<IShoppingItem>> GetShoppingListItemsAsync()
        {
            return this.localContext.ShoppingItem
                .Where(si => si.State != ShoppingItemState.ShoppingComplete)
                .Cast<IShoppingItem>()
                .ToListAsync();
        }


        private bool isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    this.localContext?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.isDisposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public IShoppingItem CreateShoppingItem() => (IShoppingItem)new ShoppingItem();

        public async Task AddShoppingItemAsync(IShoppingItem shoppingItem)
        {
            await this.localContext.ShoppingItem.AddAsync((ShoppingItem)shoppingItem);
        }

        public Task<List<string>> GetSuggestedNamesAsync(string name)
        {
            return this.localContext.ShoppingItem
                .Where(si => si.Name.StartsWith(name))
                .Select(si => si.Name)
                .ToListAsync();
        }

        public IShoppingItem? FindShoppingItem(string name)
        {
            return this.localContext.ShoppingItem
                .Where(si => si.Name == name)
                .FirstOrDefault();
        }

        public Task<IShoppingItem?> FindShoppingItemAsync(string name)
        {
            return this.localContext.ShoppingItem
                .Where(si => si.Name == name)
                .Cast<IShoppingItem>()
                .FirstOrDefaultAsync();
        }

        public void RemoveShoppingItem(IShoppingItem item)
        {
            this.localContext.Remove(item);
        }

        public async Task EndShoppingAsync()
        {
            var boughtItems = await this.localContext.ShoppingItem
                .Where(si => si.State == ShoppingItemState.Bought)
                .ToListAsync();

            foreach (var boughtItem in boughtItems)
            {
                boughtItem.State = ShoppingItemState.ShoppingComplete;
            }
            await this.localContext.SaveChangesAsync();
        }
    }
}
