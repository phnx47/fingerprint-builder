using System;
using System.Security.Cryptography;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests.TypedTests;

public class ByteTests
{
    private readonly Func<ThisUser, byte[]> _sha1;
    private readonly ThisUser _user;

    public ByteTests()
    {
        _sha1 = FingerprintBuilder<ThisUser>
            .Create(SHA1.Create())
            .For(p => p.FirstName)
            .For(p => p.ByteNumber)
            .For(p => p.SByteNumber)
            .For(p => p.ByteArrNumbers)
            .Build();

        _user = new ThisUser
        {
            FirstName = "John",
            ByteNumber = 2,
            SByteNumber = 1,
            ByteArrNumbers = new byte[] { 1, 2 }
        };
    }

    [Fact]
    public void Sha1()
    {
        var hash = _sha1(_user).ToLowerHexString();
        Assert.Equal("9bba39dc41a4d896f1a54e0c37fed94a8722189f", hash);
    }

    [Fact]
    public void UserInfo_Sha1_UpdateByte_ChangeHash()
    {
        var hash0 = _sha1(_user).ToLowerHexString();
        _user.ByteNumber = 3;
        var hash1 = _sha1(_user).ToLowerHexString();

        Assert.NotEqual(hash0, hash1);
    }

    [Fact]
    public void UserInfo_Sha1_UpdateSByte_ChangeHash()
    {
        var hash0 = _sha1(_user).ToLowerHexString();
        _user.SByteNumber = -1;
        var hash1 = _sha1(_user).ToLowerHexString();

        Assert.NotEqual(hash0, hash1);
    }

    [Fact]
    public void UserInfo_Sha1_UpdateSByteArray_ChangeHash()
    {
        var hash0 = _sha1(_user).ToLowerHexString();
        _user.ByteArrNumbers[1] = 5;
        var hash1 = _sha1(_user).ToLowerHexString();

        Assert.NotEqual(hash0, hash1);
    }

    private class ThisUser : User
    {
        public byte ByteNumber { get; set; }
        public sbyte SByteNumber { get; set; }
        public byte[] ByteArrNumbers { get; set; }
    }
}
