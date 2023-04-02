using System;
using System.Security.Cryptography;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests.TypeTests;

public class IntTests
{
    private readonly Func<ThisUser, byte[]> _sha1;
    private readonly ThisUser _user;

    public IntTests()
    {
        _sha1 = FingerprintBuilder<ThisUser>
            .Create(SHA1.Create())
            .For(p => p.FirstName)
            .For(p => p.IntNumber)
            .For(p => p.UIntNumber)
            .Build();

        _user = new ThisUser
        {
            FirstName = "John",
            IntNumber = -2,
            UIntNumber = 2,
        };
    }

    [Fact]
    public void Sha1()
    {
        var hash = _sha1(_user).ToLowerHexString();
        Assert.Equal("8670e727f763004645ffb82b49e405b486732b7b", hash);
    }

    [Fact]
    public void UserInfo_Sha1_UpdateShort_ChangeHash()
    {
        var hash0 = _sha1(_user).ToLowerHexString();
        _user.IntNumber = 2;
        var hash1 = _sha1(_user).ToLowerHexString();

        Assert.NotEqual(hash0, hash1);
    }

    [Fact]
    public void UserInfo_Sha1_UpdateUShort_ChangeHash()
    {
        var hash0 = _sha1(_user).ToLowerHexString();
        _user.UIntNumber = 1;
        var hash1 = _sha1(_user).ToLowerHexString();

        Assert.NotEqual(hash0, hash1);
    }

    private class ThisUser : User
    {
        public int IntNumber { get; set; }
        public uint UIntNumber { get; set; }
    }
}
