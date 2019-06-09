using System;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SimpleInjector;

namespace Twia.AzureFunction.SimpleInjector.UnitTests
{
    [TestFixture]
    public class SimpleInjectorCrossWireExtensionsTests
    {
        [Test]
        public void CrossWire_WithNullForContainer_ThrowsException()
        {
            var serviceProvider = Mock.Of<IServiceProvider>();

            // ReSharper disable once ObjectCreationAsStatement
            var exception = Assert.Throws<ArgumentNullException>(() => SimpleInjectorCrossWireExtensions.CrossWireSingleton<ILogger>(null, serviceProvider));

            Assert.That(exception.ParamName, Is.EqualTo("container"));
        }

        [Test]
        public void CrossWire_WithNullForServiceProvider_ThrowsException()
        {
            var container = new Container();
            
            // ReSharper disable once ObjectCreationAsStatement
            var exception = Assert.Throws<ArgumentNullException>(() => container.CrossWireSingleton<ILogger>(null));

            Assert.That(exception.ParamName, Is.EqualTo("serviceProvider"));
        }
    }
}