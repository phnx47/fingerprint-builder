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

## Contribute

Contributions to the package are always welcome!

* Report any bugs or issues you find on the [issue tracker](https://github.com/phnx47/FingerprintBuilder/issues).
* You can grab the source code at the package's [git repository](https://github.com/phnx47/FingerprintBuilder).

## License

All contents of this package are licensed under the [MIT license](https://opensource.org/licenses/MIT).
