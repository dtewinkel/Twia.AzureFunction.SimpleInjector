namespace Twia.AzureFunction.SimpleInjector.UnitTests
{
    internal class ConfigurationStub : IStartConfiguration
    {
        public static bool AddILoggerGlobal { get; set; } = true;

        public static bool AddILoggerOfTGlobal { get; set; } = true;

        public bool AddILogger => AddILoggerGlobal;

        public bool AddILoggerOfT => AddILoggerOfTGlobal;
    }
}