using System;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace Twia.AzureFunction.SimpleInjector.Config
{
    public static class StartupExtension
    {
        public static void AddUserServices(this Container container, IServiceProvider serviceProvider)
        {
            var serviceProviderBuilder = serviceProvider.GetRequiredService<IStartup>();
            serviceProviderBuilder.Build(container);
        }
    }
}