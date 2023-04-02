using System;
using FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests;

public class CreateTests
{
    [Fact]
    public void ComputeHash_Null_Throw_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => FingerprintBuilder<User>.Create(computeHash: null));
    }
}
