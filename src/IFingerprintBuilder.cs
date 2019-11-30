using System;
using System.Linq.Expressions;

namespace FingerprintBuilder
{
    public interface IFingerprintBuilder<T>
    {
        IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression);
    
        IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression, Expression<Func<TProperty, TProperty>> fingerprint);
        
        Func<T, byte[]> Build();
    }
}
