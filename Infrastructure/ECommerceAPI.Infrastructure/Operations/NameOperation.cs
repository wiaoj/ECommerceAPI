using System.Text.RegularExpressions;

namespace ECommerceAPI.Infrastructure.Operations;
public static class NameOperation {
    public static String CharacterRegulatory(String name) {
        return name.Replace("\"", "")
             .Replace("!", "")
             .Replace("'", "")
             .Replace("^", "")
             .Replace("*", "")
             .Replace("+", "")
             .Replace("%", "")
             .Replace("&", "")
             .Replace("/", "")
             .Replace("(", "")
             .Replace(")", "")
             .Replace("=", "")
             .Replace("?", "")
             .Replace("_", "-")
             .Replace(" ", "-")
             .Replace("@", "")
             .Replace("€", "")
             .Replace("`", "")
             .Replace("¨", "")
             .Replace("~", "")
             .Replace(",", "")
             .Replace(":", "")
             .Replace(";", "")
             .Replace(".", "-")
             .Replace("Ö", "o")
             .Replace("ö", "o")
             .Replace("Ü", "u")
             .Replace("ü", "u")
             .Replace("ı", "i")
             .Replace("İ", "i")
             .Replace("Ğ", "g")
             .Replace("ğ", "g")
             .Replace("Ş", "s")
             .Replace("ş", "s")
             .Replace("Ç", "c")
             .Replace("ç", "c")
             .Replace("ß", "")
             .Replace("â", "a")
             .Replace("î", "i")
             .Replace("<", "")
             .Replace(">", "")
             .Replace("|", "");
    }

    private static String CharacterNameFormatterRegex(String name) {
        Regex regexEmpty = new("[½{}():;!*'\",&#$^@€|/<>~]");
        Regex regexLine = new("[-_.+]");
        name = name.Trim()
            .ToLower()
            .Replace(" ", "-")
            .Replace("[", "").Replace("]", "");
        String newFileName = regexEmpty.Replace(name, String.Empty);
        newFileName = regexLine.Replace(newFileName, "-");
        return newFileName;
    }
}