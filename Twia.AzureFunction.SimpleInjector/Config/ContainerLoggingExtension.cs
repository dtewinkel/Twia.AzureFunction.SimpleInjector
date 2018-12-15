using System;
using Microsoft.Azure.WebJobs.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;

namespace Twia.AzureFunction.SimpleInjector.Config
{

    public static class ContainerLoggingExtension
    {
        public static void AddLogging(this Container container, IServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(LogCategories.CreateFunctionUserCategory("Common"));
            container.RegisterInstance(logger);
        }
    }
}