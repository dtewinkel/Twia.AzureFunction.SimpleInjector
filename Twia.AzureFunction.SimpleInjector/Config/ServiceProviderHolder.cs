using System;
using EnsureThat;
using SimpleInjector;

namespace Twia.AzureFunction.SimpleInjector.Config
{
    public class ServiceProviderHolder
    {
        private readonly Container _serviceProvider;

        public ServiceProviderHolder(Container serviceProvider)
        {
            EnsureArg.IsNotNull(serviceProvider, nameof(serviceProvider));

            _serviceProvider = serviceProvider;
        }

        public object GetRequiredService(Type serviceType)
        {
            EnsureArg.IsNotNull(serviceType, nameof(serviceType));

            return _serviceProvider.GetInstance(serviceType);
        }
    }
}
