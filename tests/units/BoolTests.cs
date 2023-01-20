using System;
using System.Security.Cryptography;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests
{
    public class BoolTests
    {
        private readonly Func<ThisUser, byte[]> _sha1;
        private readonly ThisUser _user;

        public BoolTests()
        {
            _sha1 = FingerprintBuilder<ThisUser>
                .Create(SHA1.Create())
                .For(p => p.FirstName)
                .For(p => p.IsActive)
                .Build();

            _user = new ThisUser { FirstName = "John", IsActive = true };
        }

        [Fact]
        public void Sha1()
        {
            var hash = _sha1(_user).ToLowerHexString();

            Assert.Equal("fe563bb6a90707c3e2f9c2960a4c96de7d894762", hash);
        }

        [Fact]
        public void UserInfo_Sha1_UpdateBool_ChangeHash()
        {
            var hash0 = _sha1(_user).ToLowerHexString();
            _user.IsActive = false;
            var hash1 = _sha1(_user).ToLowerHexString();

            Assert.NotEqual(hash0, hash1);
        }

        private class ThisUser : BaseUser
        {
            public bool IsActive { get; set; }
        }
    }
}
