using System;
using Microsoft.Azure.WebJobs.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using Twia.AzureFunction.SimpleInjector.Config;

namespace Twia.AzureFunction.SimpleInjector
{
    public static class ContainerLoggingExtension
    {
        public static void AddILogger(this Container container, IServiceProvider serviceProvider, string categoryName = default)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(categoryName ?? LogCategories.CreateFunctionUserCategory("Common"));
            container.RegisterInstance(logger);
        }

        public static void AddILoggerOfT(this Container container, IServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            container.RegisterInstance(loggerFactory);
            container.Register(typeof(ILogger<>), typeof(LoggerAdapter<>), Lifestyle.Singleton);
        }
    }
}