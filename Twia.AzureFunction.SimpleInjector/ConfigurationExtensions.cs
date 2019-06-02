using Microsoft.Extensions.Configuration;

namespace Twia.AzureFunction.SimpleInjector
{
    public static class ConfigurationExtensions
    {
        public static TInterface BindSettings<TInterface, TConcrete>(this IConfiguration configuration, string sectionName)
            where TConcrete : TInterface, new()
        {
            var settings = new TConcrete();
            configuration.GetSection(sectionName).Bind(settings);
            return settings;
        }

        public static TConcrete BindSettings<TConcrete>(this IConfiguration configuration, string sectionName)
            where TConcrete : new()
        {
            return configuration.BindSettings<TConcrete, TConcrete>(sectionName);
        }
    }
}