using EnsureThat;
using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace Twia.AzureFunction.SimpleInjector
{
    public static class SimpleInjectorConfigurationExtensions
    {
        /// <summary>
        /// Register settings 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TConcrete"></typeparam>
        /// <param name="container"></param>
        /// <param name="configuration"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static TInterface RegisterSettingsSingleton<TInterface, TConcrete>(this Container container, IConfiguration configuration, string sectionName)
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

        public static TConcrete RegisterSettingsSingleton<TConcrete>(this Container container, IConfiguration configuration, string sectionName)
            where TConcrete : class, new()
            => container.RegisterSettingsSingleton<TConcrete, TConcrete>(configuration, sectionName);

        public static TInterface RegisterSettingsSingleton<TInterface, TConcrete>(this Container container, IConfiguration configuration)
            where TConcrete : class, TInterface, new()
            where TInterface : class
        {
            EnsureArg.IsNotNull(container, nameof(container));
            EnsureArg.IsNotNull(configuration, nameof(configuration));

            TInterface settings = configuration.BindSettings<TConcrete>();
            container.RegisterSingleton(() => settings);
            return settings;
        }

        public static TConcrete RegisterSettingsSingleton<TConcrete>(this Container container, IConfiguration configuration)
            where TConcrete : class, new()
            => container.RegisterSettingsSingleton<TConcrete, TConcrete>(configuration);
    }
}