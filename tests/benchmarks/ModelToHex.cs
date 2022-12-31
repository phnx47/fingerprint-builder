using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace FingerprintBuilder.BenchmarkTests
{
    [MeanColumn, MinColumn, MaxColumn, MedianColumn]
    public class ModelToHex
    {
        private Func<UserInfo, byte[]> _md5;
        private Func<UserInfo, byte[]> _sha1;
        private Func<UserInfo, byte[]> _sha256;
        private Func<UserInfo, byte[]> _sha512;

        private UserInfo _user;

        [GlobalSetup]
        public void Setup()
        {
            _user = new UserInfo
            {
                FirstName = "John",
                LastName = "Smith"
            };

            _md5 = FingerprintBuilder<UserInfo>
                .Create(MD5.Create())
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();

            _sha1 = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create())
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();

            _sha256 = FingerprintBuilder<UserInfo>
                .Create(SHA256.Create())
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();

            _sha512 = FingerprintBuilder<UserInfo>
                .Create(SHA512.Create())
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();
        }

        [Benchmark]
        public string MD5_Model_To_Hex()
        {
            return _md5(_user).ToLowerHexString();
        }

        [Benchmark]
        public string SHA1_Model_To_Hex()
        {
            return _sha1(_user).ToLowerHexString();
        }

        [Benchmark]
        public string SHA256_Model_To_Hex()
        {
            return _sha256(_user).ToLowerHexString();
        }

        [Benchmark]
        public string SHA512_Model_To_Hex()
        {
            return _sha512(_user).ToLowerHexString();
        }

        private class UserInfo
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }
    }
}
