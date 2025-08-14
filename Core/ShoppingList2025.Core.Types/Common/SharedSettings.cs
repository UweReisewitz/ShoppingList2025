namespace ShoppingList2025.Core.Types;

public enum ApplicationPlattformType
{
    None = 0,
    BlazorServer = 1,
    BlazorMaui = 2
}

public static class SharedSettings
{
    public static ApplicationPlattformType ApplicationPlattformType { get; set; } = ApplicationPlattformType.None;
}
