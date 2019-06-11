using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Twia.AzureFunction.SimpleInjector.UnitTests
{
    public class ConfigurationTestBase
    {
        protected Dictionary<string, string> CustomConfiguration { get; } =
            new Dictionary<string, string>
            {
                {"mySection:MyValue", "value1"},
                {"MyValue", "value2"}
            };

        protected interface IDummySettings
        {
            string MyValue { get; set; }
        }

        protected class DummySettings : IDummySettings
        {
            public string MyValue { get; set; }
        }
        protected IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .AddInMemoryCollection(CustomConfiguration)
                .Build();
        }
    }
}