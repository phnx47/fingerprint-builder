using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests;

public class ThreadSafeTests
{
    [Fact]
    public async Task UserInfo_Sha1_LoopThread()
    {
        var sha1 = FingerprintBuilder<BaseUser>
            .Create(SHA1.Create())
            .For(p => p.FirstName)
            .Build();

        var tasks = new List<Task>();
        for (var p = 0; p < 100; p++)
        {
            var task = Task.Run(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    var hash = sha1(new BaseUser { FirstName = Guid.NewGuid().ToString() }).ToLowerHexString();
                    Assert.NotEqual("0000000000000000000000000000000000000000", hash);
                }
            });
            tasks.Add(task);
        }

        await Task.WhenAll(tasks);
    }
}
