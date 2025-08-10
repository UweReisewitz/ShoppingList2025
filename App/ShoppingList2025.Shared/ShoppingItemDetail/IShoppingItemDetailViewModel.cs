using AsyncAwaitBestPractices.MVVM;
using ShoppingList2025.Core.Types;
using ShoppingList2025.Core.UI.Blazor;
using ShoppingList2025.Database.Types;

namespace ShoppingList2025.Shared
{
    public interface IShoppingItemDetailViewModel : IBlazorViewModelBase
    {
        Guid Id { get; set; }
        byte[] ImageData { get; set; }
        bool IsImageVisible { get; }
        string ImageSource { get; }
        EnumValueDescription ItemState { get; set; }
        IList<EnumValueDescription> ItemStateList { get; }
        bool IsLastBoughtVisible { get; }
        DateTime LastBought { get; set; }
        string Name { get; set; }
        AsyncCommand PickPhotoCommand { get; }
        ShoppingItemState State { get; set; }
        List<string> SuggestedNames { get; }
        AsyncCommand GotoHomePageCommand { get; }
        AsyncCommand TakePhotoCommand { get; }
        AsyncCommand<string> UpdateSuggestedNames { get; }
    }
}