using System;
using System.Globalization;
using System.Linq;

namespace FingerprintBuilder
{
    public static class FingerprintBuilderExtensions
    {
        public static string ToHexString(this byte[] source)
        {
            if (source == null) 
                throw new ArgumentNullException(nameof(source));
            
            return string.Join("", source.Select(ch => ch.ToString("x2", CultureInfo.InvariantCulture)));
        }
    }
}
