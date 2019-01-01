using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Twia.AzureFunction.SimpleInjector.Config;

namespace Twia.AzureFunction.SimpleInjector.UnitTests.Config
{
    [TestFixture]
    public class ServiceProviderHolderTests
    {
        private Mock<IServiceProvider> _serviceProviderMock;
        private IServiceProvider _serviceProvider;
        private ServiceProviderHolder _serviceProviderHolder;

        [SetUp]
        public void SetUp()
        {
            _serviceProviderMock = new Mock<IServiceProvider>();
            _serviceProvider = _serviceProviderMock.Object;
            _serviceProviderHolder = new ServiceProviderHolder(_serviceProvider);
        }

        [Test]
        public void Constructor_WithNullForServiceProvider_ThrowsException()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Action action = () => new ServiceProviderHolder(null);

            action.Should().Throw<ArgumentNullException>()
                .And.ParamName.Should().Be("serviceProvider");
        }

        [Test]
        public void GetRequiredService_WithNullForServiceType_ThrowsException()
        {
            Action action = () => _serviceProviderHolder.GetRequiredService(null);

            action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("serviceType");
        }

        [Test]
        public void GetRequiredService_WithServiceType_GetTypeFromServiceProvider()
        {
            var serviceType = typeof(IStartup);
            var startupMock = new Mock<IStartup>();
            _serviceProviderMock.Setup(mock => mock.GetService(serviceType)).Returns(startupMock.Object);

            var instance = _serviceProviderHolder.GetRequiredService(serviceType);

            _serviceProviderMock.Verify(mock => mock.GetService(serviceType), Times.Once);
            instance.Should().BeSameAs(startupMock.Object);
        }
    }
}