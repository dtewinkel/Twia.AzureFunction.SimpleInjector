using System;
using EnsureThat;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using Twia.AzureFunction.SimpleInjector.Binding;
using Twia.AzureFunction.SimpleInjector.Config;

namespace Twia.AzureFunction.SimpleInjector
{
    public class SimpleInjectorStartup<TStartup> : SimpleInjectorStartup<TStartup, DefaultStartConfiguration>
        where TStartup : IStartup
    {
    }

    public class SimpleInjectorStartup<TStartup, TConfiguration> : IWebJobsStartup
        where TStartup : IStartup
        where TConfiguration : IStartConfiguration, new()
    {
        public void Configure(IWebJobsBuilder builder)
        {
            EnsureArg.IsNotNull(builder, nameof(builder));

            builder.Services.AddSingleton(typeof(IStartup), typeof(TStartup));
            builder.Services.AddSingleton<IServiceProviderHolder>(provider => new ServiceProviderHolder(GetSimpleInjectContainer(provider)));
            builder.Services.AddSingleton<IInjectBindingProvider, InjectBindingProvider>();
            builder.AddExtension<InjectConfiguration>();
        }

        private static IServiceProvider GetSimpleInjectContainer(IServiceProvider serviceProvider)
        {
            var configuration = new TConfiguration();

            var container = new Container();
            if (configuration.AddILogger)
            {
                container.AddILogger(serviceProvider);
            }
            if (configuration.AddILoggerOfT)
            {
                container.AddILoggerOfT(serviceProvider);
            }
            var serviceProviderBuilder = serviceProvider.GetRequiredService<IStartup>();
            serviceProviderBuilder.Build(container, serviceProvider);
            container.Verify();
            return container;
        }
    }
}