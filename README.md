# fingerprint-builder-net

[![ci](https://img.shields.io/github/actions/workflow/status/phnx47/fingerprint-builder-net/ci.yml?branch=main&label=ci&logo=github&style=flat-square)](https://github.com/phnx47/fingerprint-builder-net/actions/workflows/ci.yml)
[![nuget](https://img.shields.io/nuget/v/FingerprintBuilder?logo=nuget&style=flat-square)](https://www.nuget.org/packages/FingerprintBuilder)
[![nuget](https://img.shields.io/nuget/dt/FingerprintBuilder?logo=nuget&style=flat-square)](https://www.nuget.org/packages/FingerprintBuilder)
[![codecov](https://img.shields.io/codecov/c/github/phnx47/fingerprint-builder-net?logo=codecov&style=flat-square&token=RW58OCIQPR)](https://app.codecov.io/gh/phnx47/fingerprint-builder-net)
[![license](https://img.shields.io/github/license/phnx47/fingerprint-builder-net?style=flat-square)](https://github.com/phnx47/fingerprint-builder-net/blob/main/LICENSE)

## Installation

```sh
 dotnet add package FingerprintBuilder
```

## Use

Declare class:

```c#
class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```

Configure Func:

```c#
var sha256 = FingerprintBuilder<User>
    .Create(SHA256.Create())
    .For(p => p.FirstName)
    .For(p => p.LastName)
    .Build();
```

Get hash:

```c#
var user = new User { FirstName = "John", LastName = "Smith" };
var hash = sha256(user).ToLowerHexString(); // 62565a67bf16004038c502eb68907411fcf7871c66ee01a1aa274cc18d9fb541
```

## Benchmarks

```ini

BenchmarkDotNet=v0.13.5, OS=arch
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.12801), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.12801), X64 RyuJIT AVX2

```
|              Method |     Mean |     Error |    StdDev |      Min |      Max |   Median |
|-------------------- |---------:|----------:|----------:|---------:|---------:|---------:|
|    MD5_Model_To_Hex | 2.142 μs | 0.0142 μs | 0.0118 μs | 2.125 μs | 2.163 μs | 2.146 μs |
|   SHA1_Model_To_Hex | 2.379 μs | 0.0155 μs | 0.0121 μs | 2.355 μs | 2.400 μs | 2.384 μs |
| SHA256_Model_To_Hex | 3.059 μs | 0.0245 μs | 0.0217 μs | 3.031 μs | 3.107 μs | 3.054 μs |
| SHA512_Model_To_Hex | 4.564 μs | 0.0182 μs | 0.0161 μs | 4.540 μs | 4.598 μs | 4.563 μs |



## License

All contents of this package are licensed under the [MIT license](https://opensource.org/licenses/MIT).
