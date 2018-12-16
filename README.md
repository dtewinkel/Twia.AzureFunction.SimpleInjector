# Twia.AzureFunction.SimpleInjector

This project provides a [NuGet](https://www.nuget.org/) package to support the use of [Simple Injector](https://simpleinjector.org) to inject dependencies as bindings into an [Azure Function](https://azure.microsoft.com/services/functions/).

## Introduction

This package is based on the nice start given bij Boris Wilhelms in his blog [Dependency injection for Azure Function v2](https://blog.wille-zone.de/post/dependency-injection-for-azure-functions/). This has been used to specialize it for Simple Injector and also simplify its usage.

## Get the package

The package is available on NuGet with ID [`Twia.AzureFunction.SimpleInjector`](https://www.nuget.org/packages/Twia.AzureFunction.SimpleInjector/).

## Usage

In short:

* Create a class derived from `IStartup` and implement `void Build(Container container)`. 
* Register this class in the `WebJobsStartupAttribute` using `SimpleInjectorStartup<T>`, where `T` is this previously created class.

The following example code shows this:

```csharp
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.WebJobs.Hosting;
using SimpleInjector;
using Twia.AzureFunction.SimpleInjector;
using Twia.AzureFunction.SimpleInjector.ExampleFunction.DependencyInjection;

// Register the Startup class as the entry point for the extension.
[assembly: WebJobsStartup(typeof(SimpleInjectorStartup<Startup>))]

namespace Twia.AzureFunction.SimpleInjector.ExampleFunction.DependencyInjection
{
    /// <summary>
    /// This class takes care of registering the SimpleInjector bindings required bij this Function App.
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
}
```

