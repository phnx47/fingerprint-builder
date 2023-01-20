﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace FingerprintBuilder
{
    public class FingerprintBuilder<T> : IFingerprintBuilder<T>
    {
        private readonly Func<byte[], byte[]> _computeHash;
        private readonly IDictionary<string, Func<T, object>> _fingerprints = new SortedDictionary<string, Func<T, object>>(StringComparer.OrdinalIgnoreCase);

        private readonly Type[] _supportedTypes = { typeof(bool), typeof(byte), typeof(string), typeof(double) };

        private FingerprintBuilder(Func<byte[], byte[]> computeHash)
        {
            _computeHash = computeHash ?? throw new ArgumentNullException(nameof(computeHash));
        }

        public static IFingerprintBuilder<T> Create(HashAlgorithm hashAlgorithm) => Create(hashAlgorithm.ComputeHash);

        public static IFingerprintBuilder<T> Create(Func<byte[], byte[]> computeHash) => new FingerprintBuilder<T>(computeHash);

        public IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression) => For<TProperty>(expression, _ => _);

        public IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression, Expression<Func<TProperty, string>> fingerprint) =>
            For<TProperty, string>(expression, fingerprint);

        public IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression, Expression<Func<TProperty, TProperty>> fingerprint)
        {
            return For<TProperty, TProperty>(expression, fingerprint);
        }

        private IFingerprintBuilder<T> For<TProperty, TReturnType>(Expression<Func<T, TProperty>> expression, Expression<Func<TProperty, TReturnType>> fingerprint)
        {
            if (!(expression.Body is MemberExpression memberExpression))
                throw new ArgumentException("Expression must be a member expression");

            if (_fingerprints.ContainsKey(memberExpression.Member.Name))
                throw new ArgumentException($"Member {memberExpression.Member.Name} has already been added");

            var returnType = typeof(TReturnType);
            if (!_supportedTypes.Contains(typeof(TReturnType)))
                throw new ArgumentException($"Unsupported Return Type: {returnType.Name}", memberExpression.Member.Name);

            var getValue = expression.Compile();
            var getFingerprint = fingerprint.Compile();

            _fingerprints[memberExpression.Member.Name] = obj =>
            {
                var value = getValue(obj);
                return value == null ? default : getFingerprint(value);
            };

            return this;
        }

        public Func<T, byte[]> Build()
        {
            return obj =>
            {
                using (var memory = new MemoryStream())
                {
                    using (var binaryWriter = new BinaryWriter(memory))
                    {
                        foreach (var item in _fingerprints)
                        {
                            var graph = item.Value(obj);
                            switch (graph)
                            {
                                case null:
                                    continue;
                                case bool b:
                                    binaryWriter.Write(b);
                                    break;
                                case byte b:
                                    binaryWriter.Write(b);
                                    break;
                                case string s:
                                    binaryWriter.Write(s);
                                    break;
                                case double d:
                                    binaryWriter.Write(d);
                                    break;


                                default:
                                    throw new ArgumentException("Unsupported Return Type", item.Key);
                            }
                        }

                        var arr = memory.ToArray();
                        lock (_computeHash)
                            return _computeHash(arr);
                    }
                }
            };
        }
    }
}
