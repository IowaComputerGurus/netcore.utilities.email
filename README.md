# NetCore.Utilities.Email ![](https://img.shields.io/github/license/iowacomputergurus/netcore.utilities.email.svg)
This is a base library to provide utilities for working with email in .NET 5.  This project is used by more concrete implementations such as NetCore.Utilities.Email.Smtp.

## Build Status
Master Branch ![Master Status](https://iowacomputergurus.visualstudio.com/ICG%20Open%20Source/_apis/build/status/NetCore%20Utilities%20Email?branchName=master)
Develop Branch ![Develop Status](https://iowacomputergurus.visualstudio.com/ICG%20Open%20Source/_apis/build/status/NetCore%20Utilities%20Email?branchName=develop)


## NuGet Package Information
ICG.NetCore.Utilities.Email ![](https://img.shields.io/nuget/v/icg.netcore.utilities.email.svg) ![](https://img.shields.io/nuget/dt/icg.netcore.utilities.email.svg)

## Usage

## Installation
Standard installation via NuGet Package Manager
```
Install-Package ICG.NetCore.Utilities.Email
```

However, it should be noted this is typically added by association from other packages

## Setup
To setup the needed dependency injection items for this library, add the following line in your DI setup.
```
services.UseIcgNetCoreUtilitiesEmail();
```

You can then configure email templates inside of your applications configuration file.

```
{
  "EmailTemplateSettings": {
    "DefaultTemplatePath": "Template.html",
    "AdditionalTemplates": { "SpecialTemplate": "File.html" }
  }
}
```

You may configure as many additional templates as desired.  All paths are relative to the ContentRoot as identified in the IHostingEnvironment value for the currently running application.

## Usage

Inline code documentation is included

## Related Projects

ICG has a number of other related projects as well

* [AspNetCore.Utilities](https://www.github.com/iowacomputergurus/aspnetcore.utilities)
* [NetCore.Utilities.Email.Smtp](https://www.github.com/iowacomputergurus/netcore.utilities.email.smtp)
* [NetCore.Utilities.Spreadsheet](https://www.github.com/iowacomputergurus/netcore.utilities.spreadsheet)
* [NetCore.Utilities.UnitTesting](https://www.github.com/iowacomputergurus/netcore.utilities.unittesting)

