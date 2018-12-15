using System;
using EnsureThat;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using Twia.AzureFunction.SimpleInjector.Bindings;
using Twia.AzureFunction.SimpleInjector.Config;
using Twia.AzureFunction.SimpleInjector.Services;

namespace Twia.AzureFunction.SimpleInjector
{
    public class SimpleInjectorStartup<TStartup> : IWebJobsStartup where TStartup : IStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            EnsureArg.IsNotNull(builder, nameof(builder));

            builder.Services.AddSingleton(typeof(IStartup), typeof(TStartup));
            builder.Services.AddSingleton(provider => new ServiceProviderHolder(GetSimpleInjectContainer(provider)));
            builder.Services.AddSingleton<InjectBindingProvider>();
            builder.AddExtension<InjectConfiguration>();
        }

        private static Container GetSimpleInjectContainer(IServiceProvider provider)
        {
            var container = new Container();
            container.AddLogging(provider);
            container.AddUserServices(provider);
            container.Verify();
            return container;
        }
    }
}