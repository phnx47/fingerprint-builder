using System;
using System.Linq.Expressions;

namespace FingerprintBuilder;

public interface IFingerprintBuilder<T>
{
    /// <summary>
    ///     Add Expression
    /// </summary>
    IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression);

    /// <summary>
    ///     Add Expression
    /// </summary>
    IFingerprintBuilder<T> For(Expression<Func<T, string>> expression, bool toLower, bool trim);

    /// <summary>
    ///     Add Expression
    /// </summary>
    IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression, Expression<Func<TProperty, string>> fingerprint);

    /// <summary>
    ///     Build Func
    /// </summary>
    Func<T, byte[]> Build();
}
