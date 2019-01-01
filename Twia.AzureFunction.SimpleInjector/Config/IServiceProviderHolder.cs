using System;

namespace Twia.AzureFunction.SimpleInjector.Config
{
    public interface IServiceProviderHolder
    {
        object GetRequiredService(Type serviceType);
    }
}