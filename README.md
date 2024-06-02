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
var hash = sha256(user).ToLowerHexString(); // 62565a67bf16004038c502eb68907411fcf7871c66ee01a1aa274cc18d9fb541
```

## Benchmarks

```
BenchmarkDotNet v0.13.12, Ubuntu 24.04 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 8.0.300
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2
```
| Method              | Mean     | Error     | StdDev    | Median   | Gen0   | Allocated |
|-------------------- |---------:|----------:|----------:|---------:|-------:|----------:|
| MD5_Model_To_Hex    | 1.343 μs | 0.0130 μs | 0.0122 μs | 1.340 μs | 0.0801 |   1.34 KB |
| SHA1_Model_To_Hex   | 1.675 μs | 0.0131 μs | 0.0123 μs | 1.674 μs | 0.0896 |   1.49 KB |
| SHA256_Model_To_Hex | 2.022 μs | 0.0187 μs | 0.0166 μs | 2.024 μs | 0.1144 |   1.93 KB |
| SHA512_Model_To_Hex | 2.937 μs | 0.0268 μs | 0.0250 μs | 2.931 μs | 0.1907 |   3.12 KB |

## License

All contents of this package are licensed under the [MIT license](https://opensource.org/licenses/MIT).
