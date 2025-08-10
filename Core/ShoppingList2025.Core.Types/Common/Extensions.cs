using System.ComponentModel;

namespace ShoppingList2025.Core.Types;

public static class Extensions
{
    /// <summary>
    /// Gets the <see cref="DescriptionAttribute" /> of an <see cref="Enum" /> 
    /// type value.
    /// </summary>
    /// <param name="value">The <see cref="Enum" /> type value.</param>
    /// <returns>A string containing the text of the
    /// <see cref="DescriptionAttribute"/>.</returns>
    public static string GetDescription(this Enum value)
    {
        var description = value.ToString();
        var fieldInfo = value.GetType().GetField(description);
        if (fieldInfo != null)
        {
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
        }
        return description;
    }

    /// <summary>
    /// Converts the <see cref="Enum" /> type to an <see cref="IList" /> 
    /// compatible object.
    /// </summary>
    /// <param name="type">The <see cref="Enum"/> type.</param>
    /// <returns>An <see cref="IList"/> containing the enumerated
    /// type value and description.</returns>
    public static List<EnumValueDescription> ToList(this Type type)
    {
        var list = new List<EnumValueDescription>();
        var enumValues = Enum.GetValues(type);

        foreach (Enum value in enumValues)
        {
            list.Add(new EnumValueDescription(value, GetDescription(value)));
        }

        return list;
    }
}
