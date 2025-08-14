using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingList2025.Core.Types;
using ShoppingList2025.Database.Model;
using ShoppingList2025.Database.Types;

[assembly: InternalsVisibleToAttribute("DataBaseMigrationHelper")]

namespace ShoppingList2025.Database
{
    internal class LocalDbContext(IPlatformSpecialFolder iSpecialFolder) : DbContext
    {
        public DbSet<ShoppingItem> ShoppingItem { get; set; }

        public async Task CreateOrMigrateDatabaseAsync()
        {
            await this.Database.MigrateAsync();

            if (this.dbIsNew)
            {
                var item1 = new ShoppingItem()
                {
                    Name = "Äpfel",
                    State = ShoppingItemState.Open
                };
                var item2 = new ShoppingItem()
                {
                    Name = "Birnen",
                    State = ShoppingItemState.Open
                };
                var item3 = new ShoppingItem()
                {
                    Name = "Bananen",
                    State = ShoppingItemState.Bought
                };
                var item4 = new ShoppingItem()
                {
                    Name = "Müsli",
                    State = ShoppingItemState.ShoppingComplete,
                    LastBought = DateTime.Now
                };
                this.ShoppingItem.Add(item1);
                this.ShoppingItem.Add(item2);
                this.ShoppingItem.Add(item3);
                this.ShoppingItem.Add(item4);
                await this.SaveChangesAsync();
            }
        }

        private bool dbIsNew;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(iSpecialFolder.ApplicationData, "ShoppingList.db3");

            this.dbIsNew = !File.Exists(dbPath);

            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}
