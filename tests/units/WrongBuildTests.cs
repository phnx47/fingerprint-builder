using System;
using System.Security.Cryptography;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests;

public class WrongBuildTests
{
    [Fact]
    public void ComputeHash_Null_Throw_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => FingerprintBuilder<User>.Create(computeHash: null));
    }

    [Fact]
    public void HashAlgorithm_Null_Throw_ArgumentException()
    {
        Assert.Throws<ArgumentException>(() => FingerprintBuilder<User>.Create(hashAlgorithm: null));
    }

    [Fact]
    public void For_Prop_Duplicate_Throw_ArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(() => FingerprintBuilder<User>
            .Create(SHA1.Create())
            .For(p => p.FirstName)
            .For(p => p.FirstName));

        Assert.Equal(nameof(User.FirstName), exception.ParamName);
    }

    [Fact]
    public void For_Prop_IsNot_MemberExpression_Throw_ArgumentException()
    {
        Assert.Throws<ArgumentException>(() => FingerprintBuilder<User>
            .Create(SHA1.Create())
            .For(p => ""));
    }
}
