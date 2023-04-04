using System;
using System.Security.Cryptography;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests.TypedTests;

public class HalfTests
{
    [Fact]
    public void Half_ThrowArgumentException() // Half is not avalaivable in 'netstandard2.*'
    {
        var exception = Assert.Throws<ArgumentException>(() => FingerprintBuilder<ThisUser>
            .Create(SHA1.Create())
            .For(p => p.FirstName)
            .For(p => p.Number)
            .Build());

        Assert.Equal(nameof(ThisUser.Number), exception.ParamName);
    }

    private class ThisUser : User
    {
        public Half Number { get; set; }
    }
}
