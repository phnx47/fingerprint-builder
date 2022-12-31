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
var fingerprint = FingerprintBuilder<User>
    .Create(SHA256.Create())
    .For(p => p.FirstName)
    .For(p => p.LastName)
    .Build();
```

Get hash:

```c#
var user = new User { FirstName = "John", LastName = "Smith" };
var hash = fingerprint(user).ToLowerHexString(); // 62565a67bf16004038c502eb68907411fcf7871c66ee01a1aa274cc18d9fb541
```

## Benchmarks

``` ini
BenchmarkDotNet=v0.13.2, OS=arch
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 7.0.1 (7.0.122.61501), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.1 (7.0.122.61501), X64 RyuJIT AVX2
```

|              Method |     Mean |     Error |    StdDev |      Min |      Max |   Median |
|-------------------- |---------:|----------:|----------:|---------:|---------:|---------:|
|    MD5_Model_To_Hex | 2.113 us | 0.0094 us | 0.0078 us | 2.101 us | 2.130 us | 2.112 us |
|   SHA1_Model_To_Hex | 2.326 us | 0.0228 us | 0.0190 us | 2.307 us | 2.369 us | 2.320 us |
| SHA256_Model_To_Hex | 2.996 us | 0.0238 us | 0.0211 us | 2.966 us | 3.044 us | 2.997 us |
| SHA512_Model_To_Hex | 4.421 us | 0.0243 us | 0.0215 us | 4.390 us | 4.458 us | 4.414 us |

## License

All contents of this package are licensed under the [MIT license](https://opensource.org/licenses/MIT).
