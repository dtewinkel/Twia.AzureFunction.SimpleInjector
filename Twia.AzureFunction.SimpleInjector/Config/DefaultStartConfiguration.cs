namespace Twia.AzureFunction.SimpleInjector.Config
{
    public class DefaultStartConfiguration : IStartConfiguration
    {
        public bool AddILogger => true;

        public bool AddILoggerOfT => false;
    }
}