using EnsureThat;
using Microsoft.Azure.WebJobs.Host.Config;
using Twia.AzureFunction.SimpleInjector.Binding;

namespace Twia.AzureFunction.SimpleInjector.Config
{
    internal class InjectConfiguration : IExtensionConfigProvider
    {
        private readonly IInjectBindingProvider _injectBindingProvider;

        public InjectConfiguration(IInjectBindingProvider injectBindingProvider)
        {
            EnsureArg.IsNotNull(injectBindingProvider, nameof(injectBindingProvider));

            _injectBindingProvider = injectBindingProvider;
        }

        public void Initialize(ExtensionConfigContext context)
        {
            EnsureArg.IsNotNull(context, nameof(context));

            context
                .AddBindingRule<InjectAttribute>()
                .Bind(_injectBindingProvider);
        }
    }
}

