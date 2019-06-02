using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace Twia.AzureFunction.SimpleInjector
{
    public static class SimpleInjectorExtensions
    {
        public static TInstance CrossWire<TInstance>(this Container container, IServiceProvider serviceProvider)
            where TInstance : class
        {
            var instance = serviceProvider.GetRequiredService<TInstance>();
            container.RegisterSingleton(() => instance);
            return instance;
        }

        public static TInterface RegisterSettings<TInterface, TConcrete>(this Container container, IConfiguration configuration, string sectionName)
            where TConcrete : TInterface, new()
            where TInterface : class
        {
            var settings = configuration.BindSettings<TInterface, TConcrete>(sectionName);
            container.RegisterSingleton(() => settings);
            return settings;
        }

    }
}