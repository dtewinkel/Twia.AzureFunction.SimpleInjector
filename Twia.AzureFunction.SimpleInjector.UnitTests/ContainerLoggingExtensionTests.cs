using System;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SimpleInjector;

namespace Twia.AzureFunction.SimpleInjector.UnitTests
{
    [TestFixture]
    public class ContainerLoggingExtensionTests
    {
        private ILoggerFactory _loggerFactory;
        private Mock<ILoggerFactory> _loggerFactoryMock;
        private IServiceProvider _serviceProvider;
        private Mock<IServiceProvider> _serviceProviderMock;
        private Container _container;

        [SetUp]
        public void SetUp()
        {
            _loggerFactoryMock = new Mock<ILoggerFactory>();
            _loggerFactory = _loggerFactoryMock.Object;
            _serviceProviderMock = new Mock<IServiceProvider>();
            _serviceProvider = _serviceProviderMock.Object;
            _serviceProviderMock
                .Setup(mock => mock.GetService(typeof(ILoggerFactory)))
                .Returns(_loggerFactory);
            _container = new Container();
        }


        [Test]
        public void AddILogger_WithNullForContainer_ThrowsException()
        {
            // ReSharper disable once ObjectCreationAsStatement
            var exception = Assert.Throws<ArgumentNullException>(() => ContainerLoggingExtension.AddILogger(null, _serviceProvider));

            Assert.That(exception.ParamName, Is.EqualTo("container"));
        }

        [Test]
        public void AddILogger_WithNullForServiceProvider_ThrowsException()
        {
            // ReSharper disable once ObjectCreationAsStatement
            var exception = Assert.Throws<ArgumentNullException>(() => _container.AddILogger(null));

            Assert.That(exception.ParamName, Is.EqualTo("serviceProvider"));
        }

        [Test]
        public void AddILoggerOfT_WithNullForContainer_ThrowsException()
        {
            // ReSharper disable once ObjectCreationAsStatement
            var exception = Assert.Throws<ArgumentNullException>(() => ContainerLoggingExtension.AddILoggerOfT(null, _serviceProvider));

            Assert.That(exception.ParamName, Is.EqualTo("container"));
        }

        [Test]
        public void AddILoggerOfT_WithNullForServiceProvider_ThrowsException()
        {
            // ReSharper disable once ObjectCreationAsStatement
            var exception = Assert.Throws<ArgumentNullException>(() => _container.AddILoggerOfT(null));

            Assert.That(exception.ParamName, Is.EqualTo("serviceProvider"));
        }

        [Test]
        public void AddILogger_UsesFactory_AndAddsLoggerWithDefaultCategory()
        {
            var logger = Mock.Of<ILogger>();
            _loggerFactoryMock.Setup(mock => mock.CreateLogger(It.Is<string>(category => category.Contains("Common")))).Returns(logger);

            _container.AddILogger(_serviceProvider);

            _container.Verify();
            var instance = _container.GetInstance<ILogger>();
            Assert.That(instance, Is.SameAs(logger));
        }

        [Test]
        public void AddILoggerOfT_UsesFactory_AndAddsLoggerAdapter()
        {
            _container.AddILoggerOfT(_serviceProvider);

            _container.Verify();
            var instance = _container.GetInstance<ILogger<string>>();
            Assert.That(instance, Is.TypeOf<Logger<string>>());
        }

        [Test]
        public void AddILoggerOfT_AddsFactoryToContainer()
        {
            _container.AddILoggerOfT(_serviceProvider);

            _container.Verify();
            var instance = _container.GetInstance<ILoggerFactory>();
            Assert.That(instance, Is.SameAs(_loggerFactory));
        }
    }
}