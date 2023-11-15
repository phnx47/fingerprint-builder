#if HasNewTypes
using System;
using System.Security.Cryptography;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests.TypedTests;


public class HalfTests
{
    private readonly Func<ThisUser, byte[]> _sha1;
    private readonly ThisUser _user;

    public HalfTests()
    {
        _sha1 = FingerprintBuilder<ThisUser>
            .Create(SHA1.Create())
            .For(p => p.FirstName)
            .For(p => p.Number)
            .Build();

        _user = new ThisUser { FirstName = "John", Number = (Half)2.1 };
    }

    [Fact]
    public void Sha1()
    {
        var hash = _sha1(_user).ToLowerHexString();
        Assert.Equal("5262b46f62ebac058f25289132af4577f9f2236a", hash);
    }

    [Fact]
    public void UserInfo_Sha1_UpdateBool_ChangeHash()
    {
        var hash0 = _sha1(_user).ToLowerHexString();
        _user.Number = (Half)2.11;
        var hash1 = _sha1(_user).ToLowerHexString();

        Assert.NotEqual(hash0, hash1);
    }

    private class ThisUser : User
    {
        public Half Number { get; set; }
    }
}
#endif
