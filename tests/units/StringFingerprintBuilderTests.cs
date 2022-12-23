using System.Security.Cryptography;
using Xunit;

namespace FingerprintBuilder.Tests
{
    public class StringFingerprintBuilderTests
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

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("ac1992c8791c6ae5c8a2e8ed22feb109d86dc091", hash);
        }

        [Fact]
        public void UserInfo_ToLowerCase_Sha1()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.FirstName, true, true)
                .For(p => p.LastName, true, true)
                .Build();

            var user = new UserInfo { FirstName = "John", LastName = "Smith" };

            const string expectedHash = "302ad30676be9e618daed716b3710ab70c1323db";

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

            var user = new UserInfo { FirstName = "John", LastName = "Smith" };

            var hashLower = fingerprint(user).ToLowerHexString();
            var hashUpper = fingerprint(user).ToUpperHexString();

            Assert.Equal("62565a67bf16004038c502eb68907411fcf7871c66ee01a1aa274cc18d9fb541", hashLower);
            Assert.Equal("62565a67bf16004038c502eb68907411fcf7871c66ee01a1aa274cc18d9fb541".ToUpperInvariant(), hashUpper);
        }

        [Fact]
        public void UserInfo_ToLowerCase_Sha256()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA256.Create().ComputeHash)
                .For(p => p.FirstName, true, true)
                .For(p => p.LastName, true, true)
                .Build();

            var user = new UserInfo { FirstName = "John", LastName = "Smith" };

            const string expectedHash = "fdd11c24f2c3f4cd9e57fbbdf77aa4c3332959fda1f6097a92d6e212aa2a533f";

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

            var user = new UserInfo { FirstName = "John", LastName = null };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("2e2a2813f314668223c79f0bc819b39ce71810ca", hash);
        }

        [Fact]
        public void UserInfo_Sha1_Equals_AnotherUserInfo_Sha1()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();

            var fingerprintAnother = FingerprintBuilder<AnotherUserInfo>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.FirstName1)
                .For(p => p.LastName1)
                .Build();

            var hash = fingerprint(new UserInfo { FirstName = "John", LastName = "Smith" }).ToLowerHexString();

            var hashRevert = fingerprintAnother(new AnotherUserInfo() {  LastName1 = "Smith", FirstName1 = "John" }).ToLowerHexString();

            Assert.Equal(hash, hashRevert);
        }

        private class UserInfo
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        private class AnotherUserInfo
        {
            public string LastName1 { get; set; }

            public string FirstName1 { get; set; }
        }
    }
}
