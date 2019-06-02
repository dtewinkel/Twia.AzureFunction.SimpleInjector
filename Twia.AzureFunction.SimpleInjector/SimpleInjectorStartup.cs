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
        where TStartup : IStartup, new()
    {
    }

    public class SimpleInjectorStartup<TStartup, TConfiguration> : IWebJobsStartup
        where TStartup : IStartup, new()
        where TConfiguration : IStartConfiguration, new()
    {
        private IStartup _startup;

        public void Configure(IWebJobsBuilder builder)
        {
            EnsureArg.IsNotNull(builder, nameof(builder));

            _startup = new TStartup();

            (_startup as IConfigure)?.Configure(builder);

            builder.Services.AddSingleton<IServiceProviderHolder>(provider => new ServiceProviderHolder(GetSimpleInjectContainer(provider, _startup)));
            builder.Services.AddSingleton<IInjectBindingProvider, InjectBindingProvider>();
            builder.AddExtension<InjectConfiguration>();
        }

        private static IServiceProvider GetSimpleInjectContainer(IServiceProvider serviceProvider, IStartup startup)
        {
            var container = new Container();

            var configuration = new TConfiguration();
            if (configuration.AddILogger)
            {
                container.AddILogger(serviceProvider);
            }
            if (configuration.AddILoggerOfT)
            {
                container.AddILoggerOfT(serviceProvider);
            }

            startup.Build(container, serviceProvider);
            container.Verify();
            return container;
        }
    }
}