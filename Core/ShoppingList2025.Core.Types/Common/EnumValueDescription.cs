using System.Globalization;

namespace ShoppingList2025.Core.Types;

public class EnumValueDescription(Enum enumvalue, string description)
{
    public Enum EnumValue { get; set; } = enumvalue;
    public string Description { get; set; } = description;

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        var val1 = Convert.ToInt32(this.EnumValue, CultureInfo.InvariantCulture);
        var val2 = Convert.ToInt32(((EnumValueDescription)obj).EnumValue, CultureInfo.InvariantCulture);
        var retval = (val1 == val2);
        return retval;
    }

    public override int GetHashCode()
    {
        return this.EnumValue.GetHashCode();
    }

    public static bool operator ==(EnumValueDescription obj1, EnumValueDescription obj2)
    {
        return ReferenceEquals(obj1, obj2) || obj1 is not null && obj1.Equals(obj2);
    }

    public static bool operator !=(EnumValueDescription obj1, EnumValueDescription obj2)
    {
        return !(obj1 == obj2);
    }
}
