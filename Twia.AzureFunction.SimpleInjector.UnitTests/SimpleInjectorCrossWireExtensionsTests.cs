using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SimpleInjector;

namespace Twia.AzureFunction.SimpleInjector.UnitTests
{
    [TestFixture]
    public class SimpleInjectorCrossWireExtensionsTests
    {
        public interface IDummy
        {
        }

        [Test]
        public void CrossWire_WithNullForContainer_ThrowsException()
        {
            var serviceProvider = Mock.Of<IServiceProvider>();

            var exception = Assert.Throws<ArgumentNullException>(() => SimpleInjectorCrossWireExtensions.CrossWireSingleton<ILogger>(null, serviceProvider));

            Assert.That(exception.ParamName, Is.EqualTo("container"));
        }

        [Test]
        public void CrossWire_WithNullForServiceProvider_ThrowsException()
        {
            var container = new Container();

            var exception = Assert.Throws<ArgumentNullException>(() => container.CrossWireSingleton<ILogger>(null));

            Assert.That(exception.ParamName, Is.EqualTo("serviceProvider"));
        }

        [Test]
        public void CrossWire_AddsInstanceFromServiceProviderToContainer()
        {
            var container = new Container();
            var dummyMock = Mock.Of<IDummy>();
            var serviceProvider = BuildServiceProvider(dummyMock);

            container.CrossWireSingleton<IDummy>(serviceProvider);

            Assert.AreSame(container.GetInstance<IDummy>(), dummyMock);
        }

        [Test]
        public void CrossWire_ReturnsCrossWiredInstance()
        {
            var container = new Container();
            var dummyMock = Mock.Of<IDummy>();
            var serviceProvider = BuildServiceProvider(dummyMock);

            var instance = container.CrossWireSingleton<IDummy>(serviceProvider);

            Assert.AreSame(instance, dummyMock);
        }

        private IServiceProvider BuildServiceProvider(IDummy dummy)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IDummy>(dummy);
            return serviceCollection.BuildServiceProvider();
        }
    }
}