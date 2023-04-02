using System;
using System.Security.Cryptography;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests;

public class StringTests
{
    private readonly Func<User, byte[]> _sha1;
    private readonly User _user;

    public StringTests()
    {
        _sha1 = FingerprintBuilder<User>
            .Create(SHA1.Create())
            .For(p => p.FirstName)
            .For(p => p.LastName)
            .Build();
        _user = new User { FirstName = "John", LastName = "Smith" };
    }

    [Fact]
    public void Sha1()
    {
        var hash = _sha1(_user).ToLowerHexString();

        Assert.Equal("ac1992c8791c6ae5c8a2e8ed22feb109d86dc091", hash);
    }

    [Fact]
    public void Sha256()
    {
        var sha256 = FingerprintBuilder<User>
            .Create(SHA256.Create())
            .For(p => p.FirstName)
            .For(p => p.LastName)
            .Build();

        var hashLower = sha256(_user).ToLowerHexString();
        var hashUpper = sha256(_user).ToUpperHexString();

        const string expected = "62565a67bf16004038c502eb68907411fcf7871c66ee01a1aa274cc18d9fb541";

        Assert.Equal(expected, hashLower);
        Assert.Equal(expected.ToUpperInvariant(), hashUpper);
    }


    [Fact]
    public void Sha1_Null()
    {
        _user.LastName = null;

        var hash = _sha1(_user).ToLowerHexString();

        Assert.Equal("2e2a2813f314668223c79f0bc819b39ce71810ca", hash);
    }

    [Fact]
    public void Sha1_Equals_AnotherBaseUser_Sha1()
    {
        var sha1ChangedProp = FingerprintBuilder<ChangedPropUser>
            .Create(SHA1.Create())
            .For(p => p.FirstName1)
            .For(p => p.LastName1)
            .Build();

        var hash = _sha1(_user).ToLowerHexString();

        var hashChangedProp = sha1ChangedProp(new ChangedPropUser { LastName1 = "Smith", FirstName1 = "John" }).ToLowerHexString();

        Assert.Equal(hash, hashChangedProp);
    }

    [Fact]
    public void Sha1_Compare_Create()
    {
        var sha1A = FingerprintBuilder<User>
            .Create(SHA1.Create())
            .For(p => p.FirstName)
            .For(p => p.LastName)
            .Build();

        var sha1B = FingerprintBuilder<User>
            .Create(SHA1.Create().ComputeHash)
            .For(p => p.FirstName)
            .For(p => p.LastName)
            .Build();

        Assert.Equal(sha1A(_user).ToLowerHexString(), sha1B(_user).ToLowerHexString());
    }

    private class ChangedPropUser
    {
        public string LastName1 { get; set; }

        public string FirstName1 { get; set; }
    }
}
