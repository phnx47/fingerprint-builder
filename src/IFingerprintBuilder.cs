using System;
using System.Linq.Expressions;

namespace FingerprintBuilder;

public interface IFingerprintBuilder<T>
{
    IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression);

    IFingerprintBuilder<T> For(Expression<Func<T, string>> expression, bool toLower, bool trim);

    IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression, Expression<Func<TProperty, string>> fingerprint);

    Func<T, byte[]> Build();
}
