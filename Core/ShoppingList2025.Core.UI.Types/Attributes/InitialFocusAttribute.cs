namespace ShoppingList2025.Core.UI.Types;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class InitialFocusAttribute(string focusableObjectName) : Attribute
{
    public string FocusableObjectName { get; set; } = focusableObjectName;
}
