using System;
using System.Globalization;
using System.Linq;

namespace FingerprintBuilder;

public static class FingerprintBuilderExtensions
{
    /// <summary>
    ///     Convert to LowerCase Hexadecimal string
    /// </summary>
    public static string ToLowerHexString(this byte[] source)
    {
        return source.ToString("x2");
    }

    /// <summary>
    ///     Convert to UpperCase Hexadecimal string
    /// </summary>
    public static string ToUpperHexString(this byte[] source)
    {
        return source.ToString("X2");
    }

    private static string ToString(this byte[] source, string format)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return string.Join("", source.Select(ch => ch.ToString(format, CultureInfo.InvariantCulture)));
    }
}
