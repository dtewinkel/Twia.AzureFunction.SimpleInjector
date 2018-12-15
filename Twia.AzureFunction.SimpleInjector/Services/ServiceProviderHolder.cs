using System;
using EnsureThat;

namespace Twia.AzureFunction.SimpleInjector.Services
{
    public class ServiceProviderHolder
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderHolder(IServiceProvider serviceProvider)
        {
            EnsureArg.IsNotNull(serviceProvider, nameof(serviceProvider));

            _serviceProvider = serviceProvider;
        }

        public object GetRequiredService(Type serviceType)
        {
            EnsureArg.IsNotNull(serviceType, nameof(serviceType));

            return _serviceProvider.GetService(serviceType);
        }
    }
}
