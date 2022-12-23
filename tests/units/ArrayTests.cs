using System;
using System.Security.Cryptography;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests
{
    public class ArrayTests
    {
        [Fact]
        public void EmailsAsArray_ThrowArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => FingerprintBuilder<ThisUser>
                .Create(SHA1.Create())
                .For(p => p.FirstName)
                .For(p => p.Emails)
                .Build());
            Assert.Equal(nameof(ThisUser.Emails), exception.ParamName);
        }

        [Fact]
        public void Sha1_EmailsToString()
        {
            var fingerprint = FingerprintBuilder<ThisUser>
                .Create(SHA1.Create())
                .For(p => p.FirstName)
                .For(p => p.Emails, emails => string.Join('|', emails))
                .Build();

            var user = new ThisUser { FirstName = "John", Emails = new[] { "test@test.com" } };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("a9840b53e6aadb3a3a5a48d3ffc9df56441304b6", hash);
        }

        [Fact]
        public void Sha1_EmailsToString_UpdateArray_ChangeHash()
        {
            var fingerprint = FingerprintBuilder<ThisUser>
                .Create(SHA1.Create())
                .For(p => p.FirstName)
                .For(p => p.Emails, emails => string.Join('|', emails))
                .Build();

            var user = new ThisUser { FirstName = "John", Emails = new[] { "test@test.com" } };

            var hash0 = fingerprint(user).ToLowerHexString();
            user.Emails[0] += "1";
            var hash1 = fingerprint(user).ToLowerHexString();

            Assert.NotEqual(hash0, hash1);
        }

        private class ThisUser : BaseUser
        {
            public string[] Emails { get; set; }
        }
    }
}
