using System.Text.RegularExpressions;

namespace HajurKoCarRental.Client.Utils;

internal static class TemplateUtils
{
    public static string ReplaceInUrl(string uri, object value)
    {
        var regex = new Regex("{.*?}");
        var match = regex.Match(uri);
        if (!match.Success) return uri;
        var idPlaceholder = match.Value;
        var newUrl = uri.Replace(idPlaceholder, value.ToString());
        return newUrl;
    }
}