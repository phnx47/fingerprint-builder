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
            .For(p => p.C)
            .For(p => p.CA)
            .Build();

        _user = new ThisUser
        {
            FirstName = "John",
            C = 'a',
            CA = new[] { 'a', 'b' }
        };
    }

    [Fact]
    public void Sha1()
    {
        var hash = _sha1(_user).ToLowerHexString();
        Assert.Equal("a35e3c62ef3fc755ddadf9df86c3008e8874cac1", hash);
    }

    [Fact]
    public void UserInfo_Sha1_UpdateChar_ChangeHash()
    {
        var hash0 = _sha1(_user).ToLowerHexString();
        _user.C = 'c';
        var hash1 = _sha1(_user).ToLowerHexString();

        Assert.NotEqual(hash0, hash1);
    }


    [Fact]
    public void UserInfo_Sha1_UpdateCharArray_ChangeHash()
    {
        var hash0 = _sha1(_user).ToLowerHexString();
        _user.CA[1] = 'c';
        var hash1 = _sha1(_user).ToLowerHexString();

        Assert.NotEqual(hash0, hash1);
    }

    private class ThisUser : BaseUser
    {
        public char C { get; set; }

        public char[] CA { get; set; }
    }
}
