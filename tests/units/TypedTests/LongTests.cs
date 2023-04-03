using System;
using System.Security.Cryptography;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests.TypedTests;

public class LongTests
{
    private readonly Func<ThisUser, byte[]> _sha1;
    private readonly ThisUser _user;

    public LongTests()
    {
        _sha1 = FingerprintBuilder<ThisUser>
            .Create(SHA1.Create())
            .For(p => p.FirstName)
            .For(p => p.LongNumber)
            .For(p => p.ULongNumber)
            .Build();

        _user = new ThisUser
        {
            FirstName = "John",
            LongNumber = -2,
            ULongNumber = 2,
        };
    }

    [Fact]
    public void Sha1()
    {
        var hash = _sha1(_user).ToLowerHexString();
        Assert.Equal("f6b0bac504d0d966b2b1ea2e0504f2ee661f0d40", hash);
    }

    [Fact]
    public void UserInfo_Sha1_UpdateLong_ChangeHash()
    {
        var hash0 = _sha1(_user).ToLowerHexString();
        _user.LongNumber = 2;
        var hash1 = _sha1(_user).ToLowerHexString();

        Assert.NotEqual(hash0, hash1);
    }

    [Fact]
    public void UserInfo_Sha1_UpdateULong_ChangeHash()
    {
        var hash0 = _sha1(_user).ToLowerHexString();
        _user.ULongNumber = 1;
        var hash1 = _sha1(_user).ToLowerHexString();

        Assert.NotEqual(hash0, hash1);
    }

    private class ThisUser : User
    {
        public long LongNumber { get; set; }
        public ulong ULongNumber { get; set; }
    }
}
