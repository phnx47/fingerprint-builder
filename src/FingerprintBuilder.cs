using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace FingerprintBuilder;

public class FingerprintBuilder<T> : IFingerprintBuilder<T>
{
    private readonly Func<byte[], byte[]> _computeHash;
    private readonly IDictionary<string, Func<T, object>> _fingerprints = new SortedDictionary<string, Func<T, object>>(StringComparer.OrdinalIgnoreCase);

    private readonly Type[] _supportedTypes =
    {
        typeof(bool),
        typeof(byte),
        typeof(sbyte),
        typeof(byte[]),
        typeof(char),
        typeof(char[]),
        typeof(string),
        typeof(double)
    };

    private FingerprintBuilder(Func<byte[], byte[]> computeHash)
    {
        _computeHash = computeHash ?? throw new ArgumentNullException(nameof(computeHash));
    }

    public static IFingerprintBuilder<T> Create(HashAlgorithm hashAlgorithm) => Create(hashAlgorithm.ComputeHash);

    public static IFingerprintBuilder<T> Create(Func<byte[], byte[]> computeHash) => new FingerprintBuilder<T>(computeHash);

    public IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression) => For(expression, f => f);

    public IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression, Expression<Func<TProperty, string>> fingerprint) =>
        For<TProperty, string>(expression, fingerprint);

    private IFingerprintBuilder<T> For<TProperty, TReturnType>(Expression<Func<T, TProperty>> expression, Expression<Func<TProperty, TReturnType>> fingerprint)
    {
        if (expression.Body is not MemberExpression memberExpression)
            throw new ArgumentException("Expression must be a member expression");

        if (_fingerprints.ContainsKey(memberExpression.Member.Name))
            throw new ArgumentException($"Member {memberExpression.Member.Name} has already been added");

        var returnType = typeof(TReturnType);
        if (!_supportedTypes.Contains(typeof(TReturnType)))
            throw new ArgumentException($"Unsupported Return Type: {returnType.Name}", memberExpression.Member.Name);

        var getValue = expression.Compile();
        var getFingerprint = fingerprint.Compile();

        _fingerprints[memberExpression.Member.Name] = entity =>
        {
            var value = getValue(entity);
            return value == null ? default : getFingerprint(value);
        };

        return this;
    }

    public Func<T, byte[]> Build()
    {
        return entity =>
        {
            using var memory = new MemoryStream();
            using var binaryWriter = new BinaryWriter(memory);
            foreach (var item in _fingerprints)
            {
                var value = item.Value(entity);
                switch (value)
                {
                    case null:
                        break;
                    case bool typedValue:
                        binaryWriter.Write(typedValue);
                        break;
                    case byte typedValue:
                        binaryWriter.Write(typedValue);
                        break;
                    case sbyte typedValue:
                        binaryWriter.Write(typedValue);
                        break;
                    case byte[] typedValue:
                        binaryWriter.Write(typedValue);
                        break;
                    case char typedValue:
                        binaryWriter.Write(typedValue);
                        break;
                    case char[] typedValue:
                        binaryWriter.Write(typedValue);
                        break;
                    case string typedValue:
                        binaryWriter.Write(typedValue);
                        break;
                    case double typedValue:
                        binaryWriter.Write(typedValue);
                        break;
                    default:
                        throw new ArgumentException("Unsupported Return Type", item.Key);
                }
            }

            var arr = memory.ToArray();
            lock (_computeHash)
                return _computeHash(arr);
        };
    }
}
