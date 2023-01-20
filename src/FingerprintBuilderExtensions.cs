using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace FingerprintBuilder;

public static class FingerprintBuilderExtensions
{
    public static IFingerprintBuilder<T> For<T>(this IFingerprintBuilder<T> builder, Expression<Func<T, string>> expression, bool toLower, bool trim)
    {
        var format = (Func<string, string>)(input =>
        {
            if (toLower)
                input = input.ToLowerInvariant();

            if (trim)
                input = input.Trim();

            return input;
        });

        return builder.For(expression, input => format(input));
    }

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

    /// <summary>
    ///     Convert to string
    /// </summary>
    private static string ToString(this byte[] source, string format)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return string.Join("", source.Select(ch => ch.ToString(format, CultureInfo.InvariantCulture)));
    }
}
