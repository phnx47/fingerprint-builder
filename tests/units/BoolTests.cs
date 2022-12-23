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

            Assert.Equal("a03db944eab509e15d068e7a1144d8cf4b2714dd", hash);
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
