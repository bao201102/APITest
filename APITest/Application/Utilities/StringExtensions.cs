using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace APITest.Application.Utilities
{
    public static class StringExtensions
    {
        //public static string ToSnakeCase(this string input)
        //{
        //    if (string.IsNullOrEmpty(input))
        //    {
        //        return input;
        //    }

        //    return Regex.Match(input, "^_+")?.ToString() + Regex.Replace(input, "([a-z0-9])([A-Z])", "$1_$2").ToLower();
        //}

        //public static JObject ToSnakeCase(this JObject original)
        //{
        //    JObject jObject = new JObject();
        //    foreach (JProperty item in original.Properties())
        //    {
        //        string propertyName = item.Name.ToSnakeCase();
        //        jObject[propertyName] = item.Value;
        //    }

        //    return jObject;
        //}
    }
}
