# Twia.AzureFunction.SimpleInjector

This project provides a [NuGet](https://www.nuget.org/) package to support the use of [Simple Injector](https://simpleinjector.org) to inject dependencies as bindings into an [Azure Function](https://azure.microsoft.com/services/functions/).    

Latest baster branch build: [![Build status](https://twia.visualstudio.com/Twia.AzureFunction.SimpleInjector/_apis/build/status/Twia.AzureFunction.SimpleInjector-CI)](https://twia.visualstudio.com/Twia.AzureFunction.SimpleInjector/_build/latest?definitionId=12) and Release: [![Release build](https://twia.vsrm.visualstudio.com/_apis/public/Release/badge/ce3539e0-dd5a-4fb2-bcb0-823f6249db07/1/2)](https://www.nuget.org/packages/Twia.AzureFunction.SimpleInjector/)

## Introduction

This package is based on the nice start given by Boris Wilhelms in his blog [Dependency injection for Azure Function v2](https://blog.wille-zone.de/post/dependency-injection-for-azure-functions/). This has been used to specialize it for Simple Injector and also simplify its usage.

## Get the package

The package is available on NuGet with ID [`Twia.AzureFunction.SimpleInjector`](https://www.nuget.org/packages/Twia.AzureFunction.SimpleInjector/).

## Usage

In short:

* Create a class derived from `IStartup` and implement `void Build(Container container)`. 
* Register this class in the `WebJobsStartupAttribute` using `SimpleInjectorStartup<T>`, where `T` is this previously created class.

### Using the default configuration

The following example code shows how to create configure the dependicyinjection, using the default configuration for logging:

```csharp
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.WebJobs.Hosting;
using SimpleInjector;
using Twia.AzureFunction.SimpleInjector;

// Register the Startup class as the entry point for the extension.
[assembly: WebJobsStartup(typeof(SimpleInjectorStartup<Startup>))]

/// <summary>
/// This class takes care of registering the SimpleInjector bindings required by this Function App.
/// </summary>
internal class Startup : IStartup
{
    /// <summary>
    /// Build the Simple Injector Container.
    /// </summary>
    /// <param name="container">The container in which to register all dependencies.</param>
    public void Build(Container container)
    {
        container.RegisterInstance(BuildConfiguration());
        container.RegisterSingleton<IExampleService, ExampleService>();
    }

    /// <summary>
    ///  Add support for the use of configuration in services.
    /// </summary>
    /// <returns>An instance of <see cref="IConfiguration"/>.</returns>
    private static IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();
    }
}
```

### Using your own configuration

A configuration class can be provided that defines which support for logging must be added. By default support for `ILogger` is addded, but through this configuration also support for `ILogger<T>` can be added. 

The configuration class can be implemented according to the following example:
```csharp
using Twia.AzureFunction.SimpleInjector;

public class StartupConfiguration : IStartConfiguration
{
    public bool AddILogger => true;

    public bool AddILoggerOfT => true;
}
```

Then change the line `[assembly: WebJobsStartup(typeof(SimpleInjectorStartup<Startup>))]` to `[assembly: WebJobsStartup(typeof(SimpleInjectorStartup<Startup, StartupConfiguration>))]` to use the configuration.


## Known issues

There is a know issue in Azure Function V2 resulting in not always generating a correct `extensions.json`. See [Issue 972](https://github.com/Azure/Azure-Functions/issues/972) in the [Azure Functions GitHub repository](https://github.com/Azure/Azure-Functions) for a detailed description and suggested workarounds.