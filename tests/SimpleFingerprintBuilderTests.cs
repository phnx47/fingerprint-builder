using System;
using System.Security.Cryptography;
using Xunit;
using Xunit.Abstractions;

namespace FingerprintBuilder.Tests
{
    public class SimpleFingerprintBuilderTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public SimpleFingerprintBuilderTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void UserInfo_Sha1()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();

            var user = new UserInfo
            {
                FirstName = "John",
                LastName = "Smith"
            };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("bfe2cb034d9448e66f642506e6370dd87bbbe0e0", hash);
        }

        [Fact]
        public void UserInfo_ToLowerCase_Sha1()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.FirstName, true, true)
                .For(p => p.LastName, true, true)
                .Build();

            var user = new UserInfo
            {
                FirstName = "John",
                LastName = "Smith"
            };

            const string expectedHash = "df7fd58e2378573dd2e6e7340a9b2390d2bda770";

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal(expectedHash, hash);

            user.FirstName = user.FirstName.ToLowerInvariant();
            user.LastName = user.LastName.ToLowerInvariant();

            var hash1 = fingerprint(user).ToLowerHexString();
            Assert.Equal(expectedHash, hash1);
        }

        [Fact]
        public void UserInfo_Sha256()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA256.Create().ComputeHash)
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();

            var user = new UserInfo
            {
                FirstName = "John",
                LastName = "Smith"
            };

            var hashLower = fingerprint(user).ToLowerHexString();
            var hashUpper = fingerprint(user).ToUpperHexString();

            Assert.Equal("9996c4bbc1da4938144886b27b7c680e75932b5a56d911754d75ae4e0a9b4f1a", hashLower);
            Assert.Equal("9996c4bbc1da4938144886b27b7c680e75932b5a56d911754d75ae4e0a9b4f1a".ToUpperInvariant(), hashUpper);
        }

        [Fact]
        public void UserInfo_ToLowerCase_Sha256()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA256.Create().ComputeHash)
                .For(p => p.FirstName, true, true)
                .For(p => p.LastName, true, true)
                .Build();

            var user = new UserInfo
            {
                FirstName = "John",
                LastName = "Smith"
            };

            const string expectedHash = "6012fe3d8bd3038b701c9ddec210b591baecc3aa2ec1f727a7d1f3c9f2032cb3";

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal(expectedHash, hash);

            user.FirstName = user.FirstName.ToLowerInvariant();
            user.LastName = user.LastName.ToLowerInvariant();

            var hash1 = fingerprint(user).ToLowerHexString();
            Assert.Equal(expectedHash, hash1);
        }

        [Fact]
        public void UserInfo_Sha1_Null()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();

            var user = new UserInfo
            {
                FirstName = "John",
                LastName = null
            };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("5ab5aeba11346413348fb7c9361058e016ecf3ca", hash);
        }

        private class UserInfo
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }
    }
}
