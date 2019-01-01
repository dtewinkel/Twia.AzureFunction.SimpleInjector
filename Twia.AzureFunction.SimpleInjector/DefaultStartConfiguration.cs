namespace Twia.AzureFunction.SimpleInjector
{
    public class DefaultStartConfiguration : IStartConfiguration
    {
        public bool AddILogger => true;

        public bool AddILoggerOfT => false;
    }
}