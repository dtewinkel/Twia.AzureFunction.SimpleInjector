using Twia.AzureFunction.SimpleInjector;

public class StartupConfiguration : IStartConfiguration
{
    public bool AddILogger => true;

    public bool AddILoggerOfT => true;
}
