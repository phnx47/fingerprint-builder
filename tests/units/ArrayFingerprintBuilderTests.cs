using System;
using System.Security.Cryptography;
using Xunit;

namespace FingerprintBuilder.Tests
{
    public class ArrayFingerprintBuilderTests
    {
        [Fact]
        public void UserInfo_EmailsAsArray_ThrowArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => FingerprintBuilder<UserInfo>
                .Create(SHA1.Create())
                .For(p => p.Name)
                .For(p => p.Emails)
                .Build());
            Assert.Equal(nameof(UserInfo.Emails), exception.ParamName);
        }

        [Fact]
        public void UserInfo_EmailsToString_Sha1()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create())
                .For(p => p.Name)
                .For(p => p.Emails, emails => string.Join('|', emails))
                .Build();

            var user = new UserInfo
            {
                Name = "John",
                Emails = new[] { "test@test.com" }
            };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("a9840b53e6aadb3a3a5a48d3ffc9df56441304b6", hash);
        }

        [Fact]
        public void UserInfo_EmailsToString_Sha1_UpdateArray_ChangeHash()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create())
                .For(p => p.Name)
                .For(p => p.Emails, emails => string.Join('|', emails))
                .Build();

            var user = new UserInfo
            {
                Name = "John",
                Emails = new[] { "test@test.com" }
            };

            var hash0 = fingerprint(user).ToLowerHexString();
            user.Emails[0] += "1";
            var hash1 = fingerprint(user).ToLowerHexString();

            Assert.NotEqual(hash0, hash1);
        }

        private class UserInfo
        {
            public string Name { get; set; }

            public string[] Emails { get; set; }
        }
    }
}
