using AutoMapper;
using ShoppingList2025.Database.Types;

namespace ShoppingList2025.Shared;

public class ShoppingListMapperConfiguration
{
    public static void CreateMapping(IMapperConfigurationExpression configuration)
    {
        if (configuration is not null)
        {
            configuration.CreateMap<IShoppingItem, UIShoppingItem>().ReverseMap();
            configuration.CreateMap<UIShoppingItem, ShoppingItemDetailViewModel>().ReverseMap();
        }
        else
        {
            throw new ArgumentNullException(nameof(configuration));
        }
    }

}
