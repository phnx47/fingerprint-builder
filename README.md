# fingerprint-builder

[![ci](https://img.shields.io/github/actions/workflow/status/phnx47/fingerprint-builder-net/ci.yml?branch=main&label=ci&logo=github&style=flat-square)](https://github.com/phnx47/fingerprint-builder-net/actions/workflows/ci.yml)
[![nuget](https://img.shields.io/nuget/v/FingerprintBuilder?logo=nuget&style=flat-square)](https://www.nuget.org/packages/FingerprintBuilder)
[![nuget](https://img.shields.io/nuget/dt/FingerprintBuilder?logo=nuget&style=flat-square)](https://www.nuget.org/packages/FingerprintBuilder)
[![codecov](https://img.shields.io/codecov/c/github/phnx47/fingerprint-builder?logo=codecov&style=flat-square)](https://app.codecov.io/gh/phnx47/fingerprint-builder)
[![license](https://img.shields.io/github/license/phnx47/fingerprint-builder-net?style=flat-square)](https://github.com/phnx47/fingerprint-builder-net/blob/main/LICENSE)

Inspired by [he-dev/reusable:FingerprintBuilder](https://github.com/he-dev/reusable/blob/dev/Reusable.Cryptography/src/FingerprintBuilder.cs)

## Installation

```sh
 dotnet add package FingerprintBuilder
```

## How to Use

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
var hash = sha256(user).ToLowerHexString();
Console.WriteLine(hash); // 62565a67bf16004038c502eb68907411fcf7871c66ee01a1aa274cc18d9fb541
```

## Benchmarks

```
BenchmarkDotNet v0.15.8, Linux Ubuntu 24.04.4 LTS (Noble Numbat)
AMD EPYC 7763 2.45GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 10.0.302
  [Host]     : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v3
```
| Method              | Mean     | Error     | StdDev    | Median   | Gen0   | Allocated |
|-------------------- |---------:|----------:|----------:|---------:|-------:|----------:|
| MD5_Model_To_Hex    | 1.145 μs | 0.0038 μs | 0.0032 μs | 1.144 μs | 0.0725 |   1.21 KB |
| SHA1_Model_To_Hex   | 1.166 μs | 0.0078 μs | 0.0070 μs | 1.166 μs | 0.0820 |   1.37 KB |
| SHA256_Model_To_Hex | 1.381 μs | 0.0071 μs | 0.0066 μs | 1.382 μs | 0.1087 |    1.8 KB |
| SHA512_Model_To_Hex | 2.287 μs | 0.0068 μs | 0.0060 μs | 2.287 μs | 0.1831 |   2.99 KB |

## License

All contents of this package are licensed under the [MIT license](https://opensource.org/licenses/MIT).
