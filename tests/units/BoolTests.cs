using System.Security.Cryptography;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests
{
    public class BoolTests
    {
        [Fact]
        public void Sha1()
        {
            var fingerprint = FingerprintBuilder<ThisUser>
                .Create(SHA1.Create())
                .For(p => p.FirstName)
                .For(p => p.IsActive)
                .Build();

            var user = new ThisUser { FirstName = "John", IsActive = true };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("fe563bb6a90707c3e2f9c2960a4c96de7d894762", hash);
        }

        [Fact]
        public void UserInfo_Sha1_UpdateBool_ChangeHash()
        {
            var fingerprint = FingerprintBuilder<ThisUser>
                .Create(SHA1.Create())
                .For(p => p.FirstName)
                .For(p => p.IsActive)
                .Build();

            var user = new ThisUser { FirstName = "John", IsActive = true };

            var hash0 = fingerprint(user).ToLowerHexString();
            user.IsActive = false;
            var hash1 = fingerprint(user).ToLowerHexString();

            Assert.NotEqual(hash0, hash1);
        }

        private class ThisUser : BaseUser
        {
            public bool IsActive { get; set; }
        }
    }
}
