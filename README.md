# FingerprintBuilder

[![master](https://github.com/phnx47/FingerprintBuilder/workflows/master/badge.svg)](https://github.com/phnx47/FingerprintBuilder/actions?query=workflow%3Amaster)
[![NuGet](https://img.shields.io/nuget/v/FingerprintBuilder.svg)](https://www.nuget.org/packages/FingerprintBuilder)
[![NuGet](https://img.shields.io/nuget/dt/FingerprintBuilder.svg)](https://www.nuget.org/packages/FingerprintBuilder)
[![CodeFactor](https://www.codefactor.io/repository/github/phnx47/fingerprintbuilder/badge/master)](https://www.codefactor.io/repository/github/phnx47/fingerprintbuilder/overview/master)
[![License MIT](https://img.shields.io/badge/license-MIT-green.svg)](https://opensource.org/licenses/MIT)

## Installation

```sh
 dotnet add package FingerprintBuilder
```

## Use

[Tests](https://github.com/phnx47/FingerprintBuilder/tree/master/tests)

Declare class:
```c#
class UserInfo
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```

Configure Func:
```c#
var fingerprint = FingerprintBuilder<UserInfo>
    .Create(SHA256.Create().ComputeHash)
    .For(p => p.FirstName)
    .For(p => p.LastName)
    .Build();
```
Get hash:
```c#
var user = new UserInfo { FirstName = "John", LastName = "Smith" };
var hash = fingerprint(user).ToLowerHexString(); // 9996c4bbc1da4938144886b27b7c680e75932b5a56d911754d75ae4e0a9b4f1a
```

## Benchmarks

``` ini

BenchmarkDotNet=v0.12.1, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.300
  [Host]     : .NET Core 3.1.4 (CoreCLR 4.700.20.20201, CoreFX 4.700.20.22101), X64 RyuJIT
  Job-PCQRMO : .NET Core 3.1.4 (CoreCLR 4.700.20.20201, CoreFX 4.700.20.22101), X64 RyuJIT

Runtime=.NET Core 3.1  IterationCount=50  LaunchCount=2  
RunStrategy=Throughput  WarmupCount=10  

```
|              Method |     Mean |     Error |    StdDev |      Min |      Max |   Median |
|-------------------- |---------:|----------:|----------:|---------:|---------:|---------:|
|    MD5_Model_To_Hex | 4.277 μs | 0.0763 μs | 0.2128 μs | 4.007 μs | 4.612 μs | 4.275 μs |
|   SHA1_Model_To_Hex | 4.303 μs | 0.0232 μs | 0.0639 μs | 4.178 μs | 4.475 μs | 4.300 μs |
| SHA256_Model_To_Hex | 5.183 μs | 0.0526 μs | 0.1500 μs | 4.987 μs | 5.627 μs | 5.151 μs |
| SHA512_Model_To_Hex | 6.842 μs | 0.0688 μs | 0.1908 μs | 6.626 μs | 7.470 μs | 6.795 μs |


## Contribute

Contributions to the package are always welcome!

* Report any bugs or issues you find on the [issue tracker](https://github.com/phnx47/FingerprintBuilder/issues).
* You can grab the source code at the package's [git repository](https://github.com/phnx47/FingerprintBuilder).

## License

All contents of this package are licensed under the [MIT license](https://opensource.org/licenses/MIT).
