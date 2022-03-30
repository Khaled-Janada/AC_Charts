namespace Infinity.Extensions; 

public static class StringExt {

    public static string SeparateCamelCase(this string original) {
        return System.Text.RegularExpressions.Regex.Replace(original, "([a-z])([A-Z])", "$1 $2");
    }

}
