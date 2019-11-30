using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace FingerprintBuilder
{
    public static class FingerprintBuilderExtensions
    {
        public static IFingerprintBuilder<T> For<T, TProperty>(this IFingerprintBuilder<T> builder, Expression<Func<T, TProperty>> expression)
        {
            return builder.For(expression, _ => _);
        }

        public static IFingerprintBuilder<T> For<T>(this IFingerprintBuilder<T> builder, Expression<Func<T, string>> expression, bool ignoreCase, bool ignoreWhiteSpace)
        {
            var format = (Func<string, string>)(input =>
            {
                if (ignoreCase)
                {
                    input = input.ToUpperInvariant();
                }
                if (ignoreWhiteSpace)
                {
                    input = input.Trim();
                }
                return input;
            });

            return builder.For(expression, input => format(input));
        }
        
        public static string ToHexString(this byte[] source)
        {
            if (source == null) 
                throw new ArgumentNullException(nameof(source));
            
            return string.Join("", source.Select(ch => ch.ToString("x2", CultureInfo.InvariantCulture)));
        }
    }
}
