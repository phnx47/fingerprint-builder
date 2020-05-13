using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace FingerprintBuilder.Tests
{
    public class ThreadSafeFingerprintBuilderTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ThreadSafeFingerprintBuilderTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task UserInfo_Sha1_LoopThread()
        {
            var fingerprint = FingerprintBuilder<UserInfo>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Name)
                .Build();

            var tasks = new List<Task>();
            for (var p = 0; p < 10000; p++)
            {
                var task = Task.Run(() =>
                {
                    for (int i = 0; i < 100000; i++)
                    {
                        var hash = fingerprint(new UserInfo { Name = Guid.NewGuid().ToString() }).ToLowerHexString();
                        Assert.NotEqual("0000000000000000000000000000000000000000", hash);
                    }
                });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        private class UserInfo
        {
            public string Name { get; set; }
        }
    }
}
