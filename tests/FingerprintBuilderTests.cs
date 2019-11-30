using System.Security.Cryptography;
using Xunit;

namespace FingerprintBuilder.Tests
{
    public class FingerprintBuilderTests
    {
        [Fact]
        public void UserInfo_Sha1()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();

            var user = new UserInfo { FirstName = "John", LastName = "Smith" };

            var hash = fingerprint(user).ToHexString();

            Assert.Equal("bfe2cb034d9448e66f642506e6370dd87bbbe0e0", hash);
        }

        [Fact]
        public void UserInfo_IgnoreCase_Sha1()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.FirstName, true, true)
                .For(p => p.LastName, true, true)
                .Build();

            var user = new UserInfo { FirstName = "John", LastName = "Smith" };

            var hash = fingerprint(user).ToHexString();

            Assert.Equal("f747d848c4e4c6fab369e6a4d72e42d764036e98", hash);

            user.FirstName = user.FirstName.ToLowerInvariant();
            user.LastName = user.LastName.ToLowerInvariant();

            var hash1 = fingerprint(user).ToHexString();
            Assert.Equal("f747d848c4e4c6fab369e6a4d72e42d764036e98", hash1);
        }

        [Fact]
        public void UserInfo_Sha256()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA256.Create().ComputeHash)
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();

            var user = new UserInfo { FirstName = "John", LastName = "Smith" };

            var hash = fingerprint(user).ToHexString();

            Assert.Equal("9996c4bbc1da4938144886b27b7c680e75932b5a56d911754d75ae4e0a9b4f1a", hash);
        }

        [Fact]
        public void UserInfo_IgnoreCase_Sha256()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA256.Create().ComputeHash)
                .For(p => p.FirstName, true, true)
                .For(p => p.LastName, true, true)
                .Build();

            var user = new UserInfo { FirstName = "John", LastName = "Smith" };

            var hash = fingerprint(user).ToHexString();

            Assert.Equal("74f20d0059ce864f9e8a4ad337a94dbfed3b00be9b04bbc01d12feec63eb645f", hash);

            user.FirstName = user.FirstName.ToLowerInvariant();
            user.LastName = user.LastName.ToLowerInvariant();

            var hash1 = fingerprint(user).ToHexString();
            Assert.Equal("74f20d0059ce864f9e8a4ad337a94dbfed3b00be9b04bbc01d12feec63eb645f", hash1);
        }

        private class UserInfo
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }
    }
}
