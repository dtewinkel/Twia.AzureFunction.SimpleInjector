namespace Twia.AzureFunction.SimpleInjector
{
    public class FullLoggingStartConfiguration : IStartConfiguration
    {
        public bool AddILogger => true;

        public bool AddILoggerOfT => true;
    }
}