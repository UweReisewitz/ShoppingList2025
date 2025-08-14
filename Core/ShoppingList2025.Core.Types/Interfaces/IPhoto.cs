namespace ShoppingList2025.Core.Types;

public interface IPhoto
{
    Task<byte[]> TakePhoto();
    Task<byte[]> PickPhoto();
}
