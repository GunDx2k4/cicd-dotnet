using System.Text.RegularExpressions;

namespace CiCd.Common.Extensions;

public static class StringExtensions
{
    public static string ToSlug(this string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return string.Empty;

        var str = text.ToLowerInvariant().Trim();
        str = Regex.Replace(str, @"\s+", "-");
        str = Regex.Replace(str, @"[^a-z0-9\-]", "");
        return str;
    }

    public static string MaskEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            return email;

        var parts = email.Split('@');
        var name = parts[0];
        if (name.Length <= 2) return $"{name[0]}*@{parts[1]}";

        return $"{name[0]}{new string('*', name.Length - 2)}{name[^1]}@{parts[1]}";
    }
}
