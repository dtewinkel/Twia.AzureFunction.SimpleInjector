using System;
using System.IO;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using Twia.AzureFunction.SimpleInjector;

// Register the Startup class as the entry point for the extension.
[assembly: WebJobsStartup(typeof(SimpleInjectorStartup<Startup, FullLoggingStartConfiguration>))]

/// <summary>
/// This class takes care of registering the SimpleInjector bindings required by this Function App.
/// </summary>
internal class Startup : IStartup, IConfigureFunction
{
    /// <summary>
    /// Build the Simple Injector Container.
    /// </summary>
    /// <param name="container">The SimpleInjector container in which to register all dependencies.</param>
    /// <param name="serviceProvider">The service provider as created by the Azure Function framework.</param>
    public void Build(Container container, IServiceProvider serviceProvider)
    {
        var configuration = BuildConfiguration();
        container.RegisterInstance(configuration);

        container.CrossWireSingleton<IHttpClientFactory>(serviceProvider);
        container.RegisterSettingsSingleton<IExampleServiceSettings, ExampleServiceSettings>(configuration);
        container.RegisterSingleton<IExampleService, ExampleService>();
    }

    /// <summary>
    /// Add support for the use of configuration in services.
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

    public void ConfigureFunction(IWebJobsBuilder builder)
    {
        builder.Services.AddHttpClient();
    }
}
