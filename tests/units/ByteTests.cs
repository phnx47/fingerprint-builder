using System;
using System.Security.Cryptography;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests;

public class ByteTests
{
    private readonly Func<ThisUser, byte[]> _sha1;
    private readonly ThisUser _user;

    public ByteTests()
    {
        _sha1 = FingerprintBuilder<ThisUser>
            .Create(SHA1.Create())
            .For(p => p.FirstName)
            .For(p => p.B)
            .For(p => p.SB)
            .For(p => p.BA)
            .Build();

        _user = new ThisUser
        {
            FirstName = "John",
            B = 2,
            SB = 1,
            BA = new byte[] { 1, 2 }
        };
    }

    [Fact]
    public void Sha1()
    {
        var hash = _sha1(_user).ToLowerHexString();
        Assert.Equal("3493e4203a9700ae766dfb807e9febd47afcdd88", hash);
    }

    [Fact]
    public void UserInfo_Sha1_UpdateByte_ChangeHash()
    {
        var hash0 = _sha1(_user).ToLowerHexString();
        _user.B = 3;
        var hash1 = _sha1(_user).ToLowerHexString();

        Assert.NotEqual(hash0, hash1);
    }

    [Fact]
    public void UserInfo_Sha1_UpdateSByte_ChangeHash()
    {
        var hash0 = _sha1(_user).ToLowerHexString();
        _user.SB = -1;
        var hash1 = _sha1(_user).ToLowerHexString();

        Assert.NotEqual(hash0, hash1);
    }

    [Fact]
    public void UserInfo_Sha1_UpdateSByteArray_ChangeHash()
    {
        var hash0 = _sha1(_user).ToLowerHexString();
        _user.BA[1] = 5;
        var hash1 = _sha1(_user).ToLowerHexString();

        Assert.NotEqual(hash0, hash1);
    }

    private class ThisUser : BaseUser
    {
        public byte B { get; set; }
        public sbyte SB { get; set; }
        public byte[] BA { get; set; }
    }
}
