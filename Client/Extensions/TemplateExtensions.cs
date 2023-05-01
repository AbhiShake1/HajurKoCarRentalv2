using HajurKoCarRental.Client.Utils;

namespace HajurKoCarRental.Client.Extensions;

public static class TemplateExtensions
{
    public static string ReplaceInUrl(this string uri, object value)
    {
        return TemplateUtils.ReplaceInUrl(uri, value);
    }
}