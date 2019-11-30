using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace FingerprintBuilder
{
    public static class FingerprintBuilderExtensions
    {
        public static IFingerprintBuilder<T> For<T, TProperty>(this IFingerprintBuilder<T> builder, Expression<Func<T, TProperty>> expression)
        {
            return builder.For(expression, _ => _);
        }

        public static IFingerprintBuilder<T> For<T>(this IFingerprintBuilder<T> builder, Expression<Func<T, string>> expression, bool toLowerCase, bool ignoreWhiteSpace)
        {
            var format = (Func<string, string>)(input =>
            {
                if (toLowerCase)
                    input = input.ToLowerInvariant();
                
                if (ignoreWhiteSpace)
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
        ///     Convert to LowerCase Hexadecimal string
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
}
