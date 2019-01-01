using System;
using System.Collections.Concurrent;
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
    public class InjectBindingTests
    {
        private InjectBinding _injectBinding;
        private Mock<IServiceProviderHolder> _serviceProviderHolderMock;
        private IServiceProviderHolder _serviceProviderHolder;
        private readonly Type _objectType = typeof(string);
        private const string _objectValue = "A value from the service provider";

        [SetUp]
        public void SetUp()
        {
            _serviceProviderHolderMock = new Mock<IServiceProviderHolder>();
            _serviceProviderHolder = _serviceProviderHolderMock.Object;
            _serviceProviderHolderMock
                .Setup(mock => mock.GetRequiredService(typeof(string)))
                .Returns(_objectValue);
            _injectBinding = new InjectBinding(_serviceProviderHolder, _objectType);
        }

        [Test]
        public void FromAttribute_ReturnsTrue()
        {
            _injectBinding.FromAttribute.Should().BeTrue();
        }

        [Test]
        public async Task BindAsync_WithValue_ReturnsIValueProvider()
        {
            const string value = "value string";
            var cancellationToken = new CancellationToken();
            var context = new ValueBindingContext(new FunctionBindingContext(new Guid(), cancellationToken), cancellationToken);

            var result = await _injectBinding.BindAsync(value, context);

            result.Should().NotBeNull()
                .And.BeAssignableTo<IValueProvider>();
            result.Type.Should().Be<string>();
            var resultValue = await result.GetValueAsync();
            resultValue.Should().Be(value);
            result.ToInvokeString().Should().Be(value);
        }

        [Test]
        public async Task BindAsync_WithoutValue_ReturnsIValueProviderWithTypeFromConstructor()
        {
            var cancellationToken = new CancellationToken();
            var context = new BindingContext(new ValueBindingContext(new FunctionBindingContext(new Guid(), cancellationToken), cancellationToken), new ConcurrentDictionary<string, object>());

            var result = await _injectBinding.BindAsync(context);

            result.Should().NotBeNull()
                .And.BeAssignableTo<IValueProvider>();
            result.Type.Should().Be<string>();
            var resultValue = await result.GetValueAsync();
            resultValue.Should().Be(_objectValue);
            result.ToInvokeString().Should().Be(_objectValue);
        }
    }
}