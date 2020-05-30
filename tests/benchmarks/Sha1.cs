using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace FingerprintBuilder.BenchmarkTests
{
    [MemoryDiagnoser]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class Sha1
    {
        private Func<UserInfo, byte[]> _fingerprint;

        [GlobalSetup]
        public void Setup()
        {
            _fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();
        }

        [Benchmark(Description = "With lock - 100*10")]
        public async Task UserInfo_ToHex()
        {
            var user = new UserInfo
            {
                FirstName = "John",
                LastName = "Smith"
            };
            
            var tasks = new List<Task>();
            for (var p = 0; p < 100; p++)
            {
                var task = Task.Run(() =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var hash = _fingerprint(user).ToLowerHexString();
                    }
                });
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
        }

        private class UserInfo
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }
    }
}
