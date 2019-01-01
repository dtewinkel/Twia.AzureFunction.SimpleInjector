using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Moq;
using NUnit.Framework;
using Twia.AzureFunction.SimpleInjector.Binding;
using Twia.AzureFunction.SimpleInjector.Config;

namespace Twia.AzureFunction.SimpleInjector.UnitTests.Binding
{
    [TestFixture]
    public class InjectBindingProviderTests
    {
        public class ParameterInfoProvider
        {
            public void WithParameter(string x)
            {
                x.Should().NotBeNullOrWhiteSpace();
            }
        }

        [Test]
        public async Task TryCreateAsync_WithContext_ReturnsNewBinding()
        {
            var bindingValue = "a string";
            var serviceProviderHolderMock = new Mock<IServiceProviderHolder>();
            var serviceProviderHolder = serviceProviderHolderMock.Object;
            serviceProviderHolderMock.Setup(mock => mock.GetRequiredService(typeof(string)))
                .Returns(bindingValue);
            var injectBindingProvider = new InjectBindingProvider(serviceProviderHolder);
            var type = typeof(ParameterInfoProvider);
            var method = type.GetMethod("WithParameter");
            var parameter = method.GetParameters().Single(m => m.Name == "x");

            var context = new BindingProviderContext(parameter, new Dictionary<string, Type>(),CancellationToken.None);

            var binding = await injectBindingProvider.TryCreateAsync(context);

            binding.Should().NotBeNull().And.BeAssignableTo<IBinding>();
        }
    }
}