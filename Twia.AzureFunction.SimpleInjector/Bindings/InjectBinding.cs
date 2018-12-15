using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Twia.AzureFunction.SimpleInjector.Services;

namespace Twia.AzureFunction.SimpleInjector.Bindings
{
    internal class InjectBinding : IBinding
    {
        private readonly Type _type;
        private readonly ServiceProviderHolder _serviceProviderHolder;

        internal InjectBinding(ServiceProviderHolder serviceProviderHolder, Type type)
        {
            _type = type;
            _serviceProviderHolder = serviceProviderHolder;
        }

        public bool FromAttribute => true;

        public async Task<IValueProvider> BindAsync(object value, ValueBindingContext context) =>
            await Task.FromResult((IValueProvider)new InjectValueProvider(value));

        public async Task<IValueProvider> BindAsync(BindingContext context) => 
            await BindAsync(_serviceProviderHolder.GetRequiredService(_type), context.ValueContext);
        
        public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor();

        private class InjectValueProvider : IValueProvider
        {
            private readonly object _value;

            public InjectValueProvider(object value) => _value = value;

            public Type Type => _value.GetType();

            public Task<object> GetValueAsync() => Task.FromResult(_value);

            public string ToInvokeString() => _value.ToString();
        }
    }
}
