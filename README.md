# fingerprint-builder

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
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Ubuntu 22.04.3 LTS (Jammy Jellyfish)
Intel Xeon Platinum 8272CL CPU 2.60GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK 7.0.402
  [Host]     : .NET 7.0.12 (7.0.1223.47720), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.12 (7.0.1223.47720), X64 RyuJIT AVX2
```
| Method              | Mean     | Error     | StdDev    | Median   | Gen0   | Allocated |
|-------------------- |---------:|----------:|----------:|---------:|-------:|----------:|
| MD5_Model_To_Hex    | 2.613 μs | 0.0076 μs | 0.0071 μs | 2.614 μs | 0.0725 |   1.34 KB |
| SHA1_Model_To_Hex   | 2.910 μs | 0.0172 μs | 0.0161 μs | 2.910 μs | 0.0801 |   1.49 KB |
| SHA256_Model_To_Hex | 3.540 μs | 0.0064 μs | 0.0053 μs | 3.539 μs | 0.1030 |   1.93 KB |
| SHA512_Model_To_Hex | 5.449 μs | 0.0173 μs | 0.0153 μs | 5.449 μs | 0.1678 |   3.12 KB |

<!-- Sticky Pull Request Comment -->

## License

All contents of this package are licensed under the [MIT license](https://opensource.org/licenses/MIT).
