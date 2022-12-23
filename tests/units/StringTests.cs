using System.Security.Cryptography;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests
{
    public class StringTests
    {
        [Fact]
        public void Sha1()
        {
            var fingerprint = FingerprintBuilder<BaseUser>
                .Create(SHA1.Create())
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();

            var user = new BaseUser { FirstName = "John", LastName = "Smith" };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("ac1992c8791c6ae5c8a2e8ed22feb109d86dc091", hash);
        }

        [Fact]
        public void Sha256()
        {
            var fingerprint = FingerprintBuilder<BaseUser>
                .Create(SHA256.Create())
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();

            var user = new BaseUser { FirstName = "John", LastName = "Smith" };

            var hashLower = fingerprint(user).ToLowerHexString();
            var hashUpper = fingerprint(user).ToUpperHexString();

            const string expected = "62565a67bf16004038c502eb68907411fcf7871c66ee01a1aa274cc18d9fb541";

            Assert.Equal(expected, hashLower);
            Assert.Equal(expected.ToUpperInvariant(), hashUpper);
        }


        [Fact]
        public void Sha1_Null()
        {
            var fingerprint = FingerprintBuilder<BaseUser>
                .Create(SHA1.Create())
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();

            var user = new BaseUser { FirstName = "John", LastName = null };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("2e2a2813f314668223c79f0bc819b39ce71810ca", hash);
        }

        [Fact]
        public void Sha1_Equals_AnotherBaseUser_Sha1()
        {
            var fingerprint = FingerprintBuilder<BaseUser>
                .Create(SHA1.Create())
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();

            var fingerprintAnother = FingerprintBuilder<ChangedPropUser>
                .Create(SHA1.Create())
                .For(p => p.FirstName1)
                .For(p => p.LastName1)
                .Build();

            var hash = fingerprint(new BaseUser { FirstName = "John", LastName = "Smith" }).ToLowerHexString();

            var hashRevert = fingerprintAnother(new ChangedPropUser { LastName1 = "Smith", FirstName1 = "John" }).ToLowerHexString();

            Assert.Equal(hash, hashRevert);
        }

        [Fact]
        public void Sha1_Compare_Obsolete_Create()
        {
            var fingerprint0 = FingerprintBuilder<BaseUser>
                .Create(SHA1.Create())
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();

            var fingerprint1 = FingerprintBuilder<BaseUser>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();

            var user = new BaseUser { FirstName = "John", LastName = "Smith" };

            Assert.Equal(fingerprint0(user).ToLowerHexString(), fingerprint1(user).ToLowerHexString());
        }


        private class ChangedPropUser
        {
            public string LastName1 { get; set; }

            public string FirstName1 { get; set; }
        }
    }
}
