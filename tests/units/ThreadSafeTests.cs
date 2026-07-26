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
        var sha1 = FingerprintBuilder<User>
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
                    var hash = sha1(new User { FirstName = Guid.NewGuid().ToString() }).ToLowerHexString();
                    Assert.NotEqual("0000000000000000000000000000000000000000", hash);
                }
            });
            tasks.Add(task);
        }

        await Task.WhenAll(tasks);
    }

    [Fact]
    public async Task UserInfo_Sha1_SharedHashAlgorithm_LoopThread()
    {
        var hashAlgorithm = SHA1.Create();

        var firstName = FingerprintBuilder<User>
            .Create(hashAlgorithm)
            .For(p => p.FirstName)
            .Build();

        var lastName = FingerprintBuilder<User>
            .Create(hashAlgorithm)
            .For(p => p.LastName)
            .Build();

        var user = new User { FirstName = "John", LastName = "Smith" };

        var expectedFirstName = firstName(user).ToLowerHexString();
        var expectedLastName = lastName(user).ToLowerHexString();

        var tasks = new List<Task>();
        for (var p = 0; p < 100; p++)
        {
            var fingerprint = p % 2 == 0 ? firstName : lastName;
            var expected = p % 2 == 0 ? expectedFirstName : expectedLastName;

            var task = Task.Run(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    var hash = fingerprint(user).ToLowerHexString();
                    Assert.Equal(expected, hash);
                }
            });
            tasks.Add(task);
        }

        await Task.WhenAll(tasks);
    }
}
