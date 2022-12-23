using System.Security.Cryptography;
using Xunit;

namespace FingerprintBuilder.Tests
{
    public class BoolFingerprintBuilderTests
    {
        [Fact]
        public void UserInfo_Sha1()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Name)
                .For(p => p.IsActive)
                .Build();

            var user = new UserInfo { Name = "John", IsActive = true };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("a03db944eab509e15d068e7a1144d8cf4b2714dd", hash);
        }

        [Fact]
        public void UserInfo_Sha1_UpdateBool_ChangeHash()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Name)
                .For(p => p.IsActive)
                .Build();

            var user = new UserInfo { Name = "John", IsActive = true };

            var hash0 = fingerprint(user).ToLowerHexString();
            user.IsActive = false;
            var hash1 = fingerprint(user).ToLowerHexString();

            Assert.NotEqual(hash0, hash1);
        }

        private class UserInfo
        {
            public string Name { get; set; }

            public bool IsActive { get; set; }
        }
    }
}
