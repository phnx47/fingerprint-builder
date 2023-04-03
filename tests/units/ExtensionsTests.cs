using System;
using Xunit;

namespace FingerprintBuilder.Tests;

public class ExtensionsTests
{
    [Fact]
    public void Bytes_ToLowerHexString()
    {
        byte[] bytes = { 1, 12 };
        Assert.Equal("010c", bytes.ToLowerHexString());
    }

    [Fact]
    public void Bytes_ToUpperHexString()
    {
        byte[] bytes = { 1, 12 };
        Assert.Equal("010C", bytes.ToUpperHexString());
    }

    [Fact]
    public void Bytes_Null_Throw_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => ((byte[])null).ToUpperHexString());
    }
}
