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
  Job-KTABYT : .NET 7.0.1 (7.0.122.61501), X64 RyuJIT AVX2

Runtime=.NET 7.0  RunStrategy=Throughput
```

|              Method |     Mean |     Error |    StdDev |      Min |      Max |   Median |
|-------------------- |---------:|----------:|----------:|---------:|---------:|---------:|
|    MD5_Model_To_Hex | 2.124 μs | 0.0288 μs | 0.0240 μs | 2.095 μs | 2.181 μs | 2.120 μs |
|   SHA1_Model_To_Hex | 2.308 μs | 0.0260 μs | 0.0230 μs | 2.281 μs | 2.364 μs | 2.301 μs |
| SHA256_Model_To_Hex | 2.830 μs | 0.0375 μs | 0.0313 μs | 2.794 μs | 2.902 μs | 2.816 μs |
| SHA512_Model_To_Hex | 4.276 μs | 0.0520 μs | 0.0461 μs | 4.229 μs | 4.384 μs | 4.259 μs |

## License

All contents of this package are licensed under the [MIT license](https://opensource.org/licenses/MIT).
