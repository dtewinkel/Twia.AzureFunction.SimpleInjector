using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace Twia.AzureFunction.SimpleInjector.UnitTests
{
    [TestFixture]
    public class ConfigurationExtensionsTests
    {
        private readonly Dictionary<string, string> _customConfiguration =
            new Dictionary<string, string>
            {
                {"mySection:MyValue", "value1"},
                {"MyValue", "value2"}
            };

        private class DummySettings
        {
            public string MyValue { get; set; }
        }

        [Test]
        public void BindSettings_WithSection_WithNullForConfiguration_ThrowsException()
        {
            // ReSharper disable once ObjectCreationAsStatement
            var exception = Assert.Throws<ArgumentNullException>(() => ConfigurationExtensions.BindSettings<DummySettings>(null, "section"));

            Assert.That(exception.ParamName, Is.EqualTo("configuration"));
        }

        [Test]
        public void BindSettings_WithSection_WithNullForSection_ThrowsException()
        {
            var configuration = Mock.Of<IConfiguration>();
            // ReSharper disable once ObjectCreationAsStatement
            var exception = Assert.Throws<ArgumentNullException>(() => configuration.BindSettings<DummySettings>(null));

            Assert.That(exception.ParamName, Is.EqualTo("sectionName"));
        }

        [Test]
        public void BindSettings_WithSection_WithEmptySection_ThrowsException()
        {
            var configuration = Mock.Of<IConfiguration>();
            // ReSharper disable once ObjectCreationAsStatement
            var exception = Assert.Throws<ArgumentException>(() => configuration.BindSettings<DummySettings>(""));

            Assert.That(exception.ParamName, Is.EqualTo("sectionName"));
        }

        [Test]
        public void BindSettings_WithoutSection_WithNullForConfiguration_ThrowsException()
        {
            // ReSharper disable once ObjectCreationAsStatement
            var exception = Assert.Throws<ArgumentNullException>(() => ConfigurationExtensions.BindSettings<DummySettings>(null));

            Assert.That(exception.ParamName, Is.EqualTo("configuration"));
        }

        [Test]
        public void BindSettings_WithSection_CreatesSettingsClassAndProvidesItToBind()
        {
            var section = "mySection";
            var configuration = BuildConfiguration();

            var result = configuration.BindSettings<DummySettings>(section);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<DummySettings>());
            Assert.That(result.MyValue, Is.EqualTo("value1"));
        }

        [Test]
        public void BindSettings_WithoutSection_CreatesSettingsClassAndProvidesItToBind()
        {
            var configuration = BuildConfiguration();

            var result = configuration.BindSettings<DummySettings>();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<DummySettings>());
            Assert.That(result.MyValue, Is.EqualTo("value2"));
        }

        private IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .AddInMemoryCollection(_customConfiguration)
                .Build();
        }
    }
}