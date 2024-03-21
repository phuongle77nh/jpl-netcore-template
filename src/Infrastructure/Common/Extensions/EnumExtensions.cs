using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Jpl.MicroService.Infrastructure.Common.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum enumValue)
    {
        object[] attr = enumValue.GetType().GetField(enumValue.ToString())!
            .GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (attr.Length > 0)
            return ((DescriptionAttribute)attr[0]).Description;
        string result = enumValue.ToString();
        result = Regex.Replace(result, "([a-z])([A-Z])", "$1 $2");
        result = Regex.Replace(result, "([A-Za-z])([0-9])", "$1 $2");
        result = Regex.Replace(result, "([0-9])([A-Za-z])", "$1 $2");
        result = Regex.Replace(result, "(?<!^)(?<! )([A-Z][a-z])", " $1");
        return result;
    }

    public static List<string> GetDescriptionList(this Enum enumValue)
    {
        string result = enumValue.GetDescription();
        return result.Split(',').ToList();
    }


    public static string ConvertToString(this Enum eff)
    {
        return Enum.GetName(eff.GetType(), eff);
    }

    public static IEnumerable<T> GetValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    private static string GetEnumDescription(this Enum value)
    {
        // Get the Description attribute value for the enum value
        FieldInfo fi = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes.Length > 0)
            return attributes[0].Description;
        else
            return value.ToString();
    }
}