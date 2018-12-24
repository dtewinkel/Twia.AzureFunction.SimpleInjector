namespace Twia.AzureFunction.SimpleInjector
{
    public interface IStartConfiguration
    {
        bool AddILogger { get; } 

        bool AddILoggerOfT { get; }
    }
}