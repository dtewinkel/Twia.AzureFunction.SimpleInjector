using EnsureThat;
using Microsoft.Extensions.Configuration;

namespace Twia.AzureFunction.SimpleInjector
{
    public static class ConfigurationExtensions
    {
        public static TConcrete BindSettings<TConcrete>(this IConfiguration configuration)
            where TConcrete : class, new()
        {
            EnsureArg.IsNotNull(configuration, nameof(configuration));

            return Bind<TConcrete>(configuration);
        }

        public static TConcrete BindSettings<TConcrete>(this IConfiguration configuration, string sectionName)
            where TConcrete : class, new()
        {
            EnsureArg.IsNotNull(configuration, nameof(configuration));
            EnsureArg.IsNotNullOrWhiteSpace(sectionName, nameof(sectionName));

            return Bind<TConcrete>(configuration.GetSection(sectionName));
        }

        private static TConcrete Bind<TConcrete>(IConfiguration configuration)
            where TConcrete : class, new()
        {
            var settings = new TConcrete();
            configuration.Bind(settings);
            return settings;
        }
    }
}