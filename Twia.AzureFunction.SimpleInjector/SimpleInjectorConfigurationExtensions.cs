using EnsureThat;
using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace Twia.AzureFunction.SimpleInjector
{
    public static class SimpleInjectorConfigurationExtensions
    {
        public static TInterface RegisterSettings<TInterface, TConcrete>(this Container container, IConfiguration configuration, string sectionName)
            where TConcrete : class, TInterface, new()
            where TInterface : class
        {
            EnsureArg.IsNotNull(container, nameof(container));
            EnsureArg.IsNotNull(configuration, nameof(configuration));
            EnsureArg.IsNotNullOrWhiteSpace(sectionName, nameof(sectionName));

            TInterface settings = configuration.BindSettings<TConcrete>(sectionName);
            container.RegisterSingleton(() => settings);
            return settings;
        }

        public static TInterface RegisterSettings<TInterface, TConcrete>(this Container container, IConfiguration configuration)
            where TConcrete : class, TInterface, new()
            where TInterface : class
        {
            EnsureArg.IsNotNull(container, nameof(container));
            EnsureArg.IsNotNull(configuration, nameof(configuration));

            TInterface settings = configuration.BindSettings<TConcrete>();
            container.RegisterSingleton(() => settings);
            return settings;
        }

        public static TConcrete RegisterSettings<TConcrete>(this Container container, IConfiguration configuration)
            where TConcrete : class, new()
            => container.RegisterSettings<TConcrete, TConcrete>(configuration);
    }
}