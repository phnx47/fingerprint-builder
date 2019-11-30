using System;
using System.Security.Cryptography;
using Xunit;

namespace FingerprintBuilder.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void UserInfo_Sha1()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.FirstName, x => x)
                .For(p => p.LastName, x => x)
                .Build();

            var user = new UserInfo { FirstName = "John", LastName = "Smith" };

            var hash = fingerprint(user).ToHexString();

            Assert.Equal("bfe2cb034d9448e66f642506e6370dd87bbbe0e0", hash);
        }

        [Fact]
        public void UserInfo_Sha256()
        {
        }

        class UserInfo
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }
    }
}
