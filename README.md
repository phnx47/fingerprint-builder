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

|              Method |     Mean |     Error |    StdDev |   Median |   Gen0 | Allocated |
|-------------------- |---------:|----------:|----------:|---------:|-------:|----------:|
|    MD5_Model_To_Hex | 2.059 μs | 0.0078 μs | 0.0065 μs | 2.059 μs | 0.3242 |   1.34 KB |
|   SHA1_Model_To_Hex | 2.248 μs | 0.0117 μs | 0.0097 μs | 2.244 μs | 0.3624 |   1.49 KB |
| SHA256_Model_To_Hex | 2.933 μs | 0.0155 μs | 0.0138 μs | 2.931 μs | 0.4692 |   1.93 KB |
| SHA512_Model_To_Hex | 4.578 μs | 0.0176 μs | 0.0156 μs | 4.582 μs | 0.7629 |   3.12 KB |

## License

All contents of this package are licensed under the [MIT license](https://opensource.org/licenses/MIT).
