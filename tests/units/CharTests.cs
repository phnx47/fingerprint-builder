using System;
using System.Security.Cryptography;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests;

public class CharTests
{
    private readonly Func<ThisUser, byte[]> _sha1;
    private readonly ThisUser _user;

    public CharTests()
    {
        _sha1 = FingerprintBuilder<ThisUser>
            .Create(SHA1.Create())
            .For(p => p.FirstName)
            .For(p => p.Char)
            .For(p => p.ArrChars)
            .Build();

        _user = new ThisUser
        {
            FirstName = "John",
            Char = 'a',
            ArrChars = new[] { 'a', 'b' }
        };
    }

    [Fact]
    public void Sha1()
    {
        var hash = _sha1(_user).ToLowerHexString();
        Assert.Equal("c2969eeda3f16f68058e987e0f0822708837b7a4", hash);
    }

    [Fact]
    public void UserInfo_Sha1_UpdateChar_ChangeHash()
    {
        var hash0 = _sha1(_user).ToLowerHexString();
        _user.Char = 'c';
        var hash1 = _sha1(_user).ToLowerHexString();

        Assert.NotEqual(hash0, hash1);
    }


    [Fact]
    public void UserInfo_Sha1_UpdateCharArray_ChangeHash()
    {
        var hash0 = _sha1(_user).ToLowerHexString();
        _user.ArrChars[1] = 'c';
        var hash1 = _sha1(_user).ToLowerHexString();

        Assert.NotEqual(hash0, hash1);
    }

    private class ThisUser : User
    {
        public char Char { get; set; }

        public char[] ArrChars { get; set; }
    }
}
