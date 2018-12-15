using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Twia.AzureFunction.SimpleInjector.Config;

namespace Twia.AzureFunction.SimpleInjector.Binding
{
    internal class InjectBindingProvider : IBindingProvider
    {
        private readonly ServiceProviderHolder _serviceProviderHolder;

        public InjectBindingProvider(ServiceProviderHolder serviceProviderHolder) =>
            _serviceProviderHolder = serviceProviderHolder;

        public async Task<IBinding> TryCreateAsync(BindingProviderContext context) =>
            await Task.FromResult(new InjectBinding(_serviceProviderHolder, context.Parameter.ParameterType));
    }
}
