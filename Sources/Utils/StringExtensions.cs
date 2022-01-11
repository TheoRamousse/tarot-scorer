using System;
namespace Utils
{
    public static class StringExtensions
    {
        public static bool ContainsIgnoreCase(this string original, string substring, StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            return original.IndexOf(substring, stringComparison) >= 0; 
        }

        public static bool ContainsIgnoreCase(this string original, string substring)
        {
            return original.ToUpper().Contains(substring.ToUpper()); 
        }
    }
}
