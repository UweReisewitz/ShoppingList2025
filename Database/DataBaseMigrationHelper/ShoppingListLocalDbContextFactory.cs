using Microsoft.EntityFrameworkCore.Design;
using ShoppingList2025.Database;
using ShoppingListMaui.Core;

namespace DatabaseMigrationHelper
{
    internal class ShoppingListLocalDbContextFactory : IDesignTimeDbContextFactory<LocalDbContext>
    {
        public LocalDbContext CreateDbContext(string[] args)
        {
            return new LocalDbContext(new PlatformSpecialFolder());
        }
    }
}
