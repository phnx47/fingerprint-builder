using System;
using System.Security.Cryptography;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests
{
    public class ByteTests
    {
        private readonly Func<ThisUser, byte[]> _fingerprint;
        private readonly ThisUser _user;

        public ByteTests()
        {
            _fingerprint = FingerprintBuilder<ThisUser>
                .Create(SHA1.Create())
                .For(p => p.FirstName)
                .For(p => p.B)
                .For(p => p.SB)
                .Build();

            _user = new ThisUser { FirstName = "John", B = 2, SB = 1 };
        }

        [Fact]
        public void Sha1()
        {
            var hash = _fingerprint(_user).ToLowerHexString();
            Assert.Equal("a776d5f93c2913146b400978961e2d2658b27932", hash);
        }

        [Fact]
        public void UserInfo_Sha1_UpdateByte_ChangeHash()
        {
            var hash0 = _fingerprint(_user).ToLowerHexString();
            _user.B = 3;
            var hash1 = _fingerprint(_user).ToLowerHexString();

            Assert.NotEqual(hash0, hash1);
        }

        [Fact]
        public void UserInfo_Sha1_UpdateSByte_ChangeHash()
        {
            var hash0 = _fingerprint(_user).ToLowerHexString();
            _user.SB = -1;
            var hash1 = _fingerprint(_user).ToLowerHexString();

            Assert.NotEqual(hash0, hash1);
        }

        private class ThisUser : BaseUser
        {
            public byte B { get; set; }
            public sbyte SB { get; set; }
        }
    }
}
