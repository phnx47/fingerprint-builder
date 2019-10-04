using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;

namespace FingerprintBuilder
{
    public class FingerprintBuilder<T>
    {
        private readonly Func<byte[], byte[]> _computeHash;

        private readonly SortedDictionary<string, Func<T, object>> _fingerprints;

        public FingerprintBuilder(Func<byte[], byte[]> computeHash)
        {
            _computeHash = computeHash ?? throw new ArgumentNullException(nameof(computeHash));
            _fingerprints = new SortedDictionary<string, Func<T, object>>(StringComparer.OrdinalIgnoreCase);    
        }

        public static FingerprintBuilder<T> Create(Func<byte[], byte[]> computeHash)
        {
            return new FingerprintBuilder<T>(computeHash);
        }

        public FingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression, Expression<Func<TProperty, string>> fingerprint)
        {
            if (!(expression.Body is MemberExpression memberExpression ))
                throw new ArgumentException("Expression must be a member expression");
            
            if (_fingerprints.ContainsKey(memberExpression.Member.Name))
                throw new ArgumentException($"Member {memberExpression.Member.Name} has already been added.");
            
            var getValue = expression.Compile();
            var getFingerprint = fingerprint.Compile();

            _fingerprints[memberExpression.Member.Name] = obj =>
            {
                var value = getValue(obj);
                return value == null ? "" : getFingerprint(value);
            };

            return this;
        }

        public Func<T, byte[]> Build()
        {
            var binaryFormatter = new BinaryFormatter();

            return obj =>
            {
                using (var memory = new MemoryStream())
                {
                    foreach (var item in _fingerprints)
                        binaryFormatter.Serialize(memory, item.Value(obj));
                    
                    return _computeHash(memory.ToArray());
                }
            };
        }
    }
}
