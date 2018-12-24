using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.WebJobs.Hosting;
using SimpleInjector;
using Twia.AzureFunction.SimpleInjector;
using Twia.AzureFunction.SimpleInjector.ExampleFunction.DependencyInjection;

// Register the Startup class as the entry point for the extension.
[assembly: WebJobsStartup(typeof(SimpleInjectorStartup<Startup, StartupConfiguration>))]

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
        /// <param name="serviceProvider">The service provider as create by the Azure Function framework.</param>
        public void Build(Container container, IServiceProvider serviceProvider)
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