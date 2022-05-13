using System.Text.RegularExpressions;

namespace Packt.Shared;

public static class StringExtensions
{
    public static bool isValidEmail(this string input)
    {
        // use simpleregular expression to check string is valid email
        return Regex.IsMatch(input, @"[a-zA-Z0-9\.-_]+@[a-zA-Z0-9\.-_]+");
    }
}
