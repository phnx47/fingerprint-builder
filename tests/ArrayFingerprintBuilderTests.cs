using System.Security.Cryptography;
using Xunit;

namespace FingerprintBuilder.Tests
{
    public partial class ArrayFingerprintBuilderTests
    {
        [Fact]
        public void UserInfo_Sha1()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Name)
                .For(p => p.Emails)
                .Build();

            var user = new UserInfo { Name = "John", Emails = new[] { "test@test.com", "test1@test.com" } };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("365993bbd89e2e25039848e51904679cc9e13d17", hash);
        }

        [Fact]
        public void UserInfo_EmailsString_Sha1()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Name)
                .For(p => p.Emails, emails=> string.Join('|', emails))
                .Build();

            var user = new UserInfo { Name = "John", Emails = new[] { "test@test.com", "test1@test.com" } };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("9f825a64a3eb7a7f0b4887ce09ad2a76d085a8b0", hash);
        }
        
        private class UserInfo
        {
            public string Name { get; set; }
        
            public string[] Emails { get; set; }
        }
    }
}
