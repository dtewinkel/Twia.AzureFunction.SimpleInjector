using System;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using SimpleInjector;

namespace Twia.AzureFunction.SimpleInjector.UnitTests
{
    [TestFixture]
    public class SimpleInjectorConfigurationExtensionsTests : ConfigurationTestBase
    {
        [Test]
        public void RegisterSettingsSingleton_WithNullForContainer_ThrowsException()
        {
            var configuration = Mock.Of<IConfiguration>();

            var exception = Assert.Throws<ArgumentNullException>(() => SimpleInjectorConfigurationExtensions.RegisterSettingsSingleton<DummySettings>(null, configuration));

            Assert.That(exception.ParamName, Is.EqualTo("container"));
        }

        [Test]
        public void RegisterSettingsSingleton_WithNullForConfiguration_ThrowsException()
        {
            var container = new Container();

            var exception = Assert.Throws<ArgumentNullException>(() => container.RegisterSettingsSingleton<DummySettings>(null));

            Assert.That(exception.ParamName, Is.EqualTo("configuration"));
        }
    }
}