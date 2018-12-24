namespace Twia.AzureFunction.SimpleInjector.ExampleFunction.DependencyInjection
{
    public class StartupConfiguration : IStartConfiguration
    {
        public bool AddILogger => false;

        public bool AddILoggerOfT => true;
    }
}