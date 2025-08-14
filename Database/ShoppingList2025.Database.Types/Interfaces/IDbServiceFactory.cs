namespace ShoppingList2025.Database.Types
{
    public interface IDbServiceFactory
    {
        IDbService CreateNew();
    }
}
