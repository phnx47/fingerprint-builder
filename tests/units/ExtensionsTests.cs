using System.Security.Cryptography;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests;

public class ExtensionsTests
{
    [Fact]
    public void Sha1_ToLower_Trim()
    {
        var fingerprint = FingerprintBuilder<BaseUser>
            .Create(SHA1.Create())
            .For(p => p.FirstName, true, true)
            .For(p => p.LastName, true, true)
            .Build();

        var user = new BaseUser { FirstName = "John ", LastName = "Smith" };

        const string expectedHash = "302ad30676be9e618daed716b3710ab70c1323db";

        var hash = fingerprint(user).ToLowerHexString();

        Assert.Equal(expectedHash, hash);

        user.FirstName = user.FirstName.ToLowerInvariant();
        user.LastName = user.LastName.ToLowerInvariant();

        var hash1 = fingerprint(user).ToLowerHexString();
        Assert.Equal(expectedHash, hash1);
    }

    [Fact]
    public void Sha256_ToLower_Trim()
    {
        var fingerprint = FingerprintBuilder<BaseUser>
            .Create(SHA256.Create())
            .For(p => p.FirstName, true, true)
            .For(p => p.LastName, true, true)
            .Build();

        var user = new BaseUser { FirstName = "John", LastName = "Smith " };

        const string expectedHash = "fdd11c24f2c3f4cd9e57fbbdf77aa4c3332959fda1f6097a92d6e212aa2a533f";

        var hash = fingerprint(user).ToLowerHexString();

        Assert.Equal(expectedHash, hash);

        user.FirstName = user.FirstName.ToLowerInvariant();
        user.LastName = user.LastName.ToLowerInvariant();

        var hash1 = fingerprint(user).ToLowerHexString();
        Assert.Equal(expectedHash, hash1);
    }

    [Fact]
    public void ToLower_Trim_Null()
    {
        var fingerprint = FingerprintBuilder<BaseUser>
            .Create(SHA1.Create())
            .For(p => p.FirstName, true, true)
            .For(p => p.LastName, true, true)
            .Build();

        var user = new BaseUser { FirstName = null, LastName = "Smith " };

        const string expectedHash = "a4134a2d9c98e1a5dab785714fee4b84c4fcb364";

        var hash = fingerprint(user).ToLowerHexString();

        Assert.Equal(expectedHash, hash);

        user.LastName = user.LastName.ToLowerInvariant();

        var hash1 = fingerprint(user).ToLowerHexString();
        Assert.Equal(expectedHash, hash1);
    }
}
