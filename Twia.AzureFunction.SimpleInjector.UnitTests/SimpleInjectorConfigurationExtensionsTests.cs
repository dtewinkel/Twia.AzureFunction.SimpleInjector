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
        private const string _sectionName = "mySection";

        [Test]
        public void RegisterSettingsSingleton_ConcreteNoSection_WithNullForContainer_ThrowsException()
        {
            var configuration = Mock.Of<IConfiguration>();

            var exception = Assert.Throws<ArgumentNullException>(() => SimpleInjectorConfigurationExtensions.RegisterSettingsSingleton<DummySettings>(null, configuration));

            Assert.That(exception.ParamName, Is.EqualTo("container"));
        }

        [Test]
        public void RegisterSettingsSingleton_ConcreteNoSection_WithNullForConfiguration_ThrowsException()
        {
            var container = new Container();

            var exception = Assert.Throws<ArgumentNullException>(() => container.RegisterSettingsSingleton<DummySettings>(null));

            Assert.That(exception.ParamName, Is.EqualTo("configuration"));
        }

        [Test]
        public void RegisterSettingsSingleton_ConcreteSection_WithNullForContainer_ThrowsException()
        {
            var configuration = Mock.Of<IConfiguration>();

            var exception = Assert.Throws<ArgumentNullException>(() => SimpleInjectorConfigurationExtensions.RegisterSettingsSingleton<DummySettings>(null, configuration, _sectionName));

            Assert.That(exception.ParamName, Is.EqualTo("container"));
        }

        [Test]
        public void RegisterSettingsSingleton_ConcreteSection_WithNullForConfiguration_ThrowsException()
        {
            var container = new Container();

            var exception = Assert.Throws<ArgumentNullException>(() => container.RegisterSettingsSingleton<DummySettings>(null, _sectionName));

            Assert.That(exception.ParamName, Is.EqualTo("configuration"));
        }

        [Test]
        public void RegisterSettingsSingleton_ConcreteSection_WithNullForSectionName_ThrowsException()
        {
            var configuration = Mock.Of<IConfiguration>();
            var container = new Container();

            var exception = Assert.Throws<ArgumentNullException>(() => container.RegisterSettingsSingleton<DummySettings>(configuration, null));

            Assert.That(exception.ParamName, Is.EqualTo("sectionName"));
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]
        public void RegisterSettingsSingleton_ConcreteSection_WithEmptySectionName_ThrowsException(string sectionName)
        {
            var configuration = Mock.Of<IConfiguration>();
            var container = new Container();

            var exception = Assert.Throws<ArgumentException>(() => container.RegisterSettingsSingleton<DummySettings>(configuration, sectionName));

            Assert.That(exception.ParamName, Is.EqualTo("sectionName"));
        }

        [Test]
        public void RegisterSettingsSingleton_InterfaceNoSection_WithNullForContainer_ThrowsException()
        {
            var configuration = Mock.Of<IConfiguration>();

            var exception = Assert.Throws<ArgumentNullException>(() => SimpleInjectorConfigurationExtensions.RegisterSettingsSingleton<IDummySettings, DummySettings>(null, configuration));

            Assert.That(exception.ParamName, Is.EqualTo("container"));
        }

        [Test]
        public void RegisterSettingsSingleton_InterfaceNoSection_WithNullForConfiguration_ThrowsException()
        {
            var container = new Container();

            var exception = Assert.Throws<ArgumentNullException>(() => container.RegisterSettingsSingleton<IDummySettings, DummySettings>(null));

            Assert.That(exception.ParamName, Is.EqualTo("configuration"));
        }

        [Test]
        public void RegisterSettingsSingleton_InterfaceSection_WithNullForContainer_ThrowsException()
        {
            var configuration = Mock.Of<IConfiguration>();

            var exception = Assert.Throws<ArgumentNullException>(() => SimpleInjectorConfigurationExtensions.RegisterSettingsSingleton<IDummySettings, DummySettings>(null, configuration, _sectionName));

            Assert.That(exception.ParamName, Is.EqualTo("container"));
        }

        [Test]
        public void RegisterSettingsSingleton_InterfaceSection_WithNullForConfiguration_ThrowsException()
        {
            var container = new Container();

            var exception = Assert.Throws<ArgumentNullException>(() => container.RegisterSettingsSingleton<IDummySettings, DummySettings>(null, _sectionName));

            Assert.That(exception.ParamName, Is.EqualTo("configuration"));
        }

        [Test]
        public void RegisterSettingsSingleton_InterfaceSection_WithNullForSectionName_ThrowsException()
        {
            var configuration = Mock.Of<IConfiguration>();
            var container = new Container();

            var exception = Assert.Throws<ArgumentNullException>(() => container.RegisterSettingsSingleton<IDummySettings, DummySettings>(configuration, null));

            Assert.That(exception.ParamName, Is.EqualTo("sectionName"));
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]
        public void RegisterSettingsSingleton_InterfaceSection_WithEmptySectionName_ThrowsException(string sectionName)
        {
            var configuration = Mock.Of<IConfiguration>();
            var container = new Container();

            var exception = Assert.Throws<ArgumentException>(() => container.RegisterSettingsSingleton<IDummySettings, DummySettings>(configuration, sectionName));

            Assert.That(exception.ParamName, Is.EqualTo("sectionName"));
        }
    }
}