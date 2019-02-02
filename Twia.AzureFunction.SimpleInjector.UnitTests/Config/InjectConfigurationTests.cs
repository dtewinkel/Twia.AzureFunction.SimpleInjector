using System;
using Moq;
using NUnit.Framework;
using Twia.AzureFunction.SimpleInjector.Binding;
using Twia.AzureFunction.SimpleInjector.Config;

namespace Twia.AzureFunction.SimpleInjector.UnitTests.Config
{
    [TestFixture]
    public class InjectConfigurationTests
    {
        private IInjectBindingProvider _injectBindingProvider;
        private Mock<IInjectBindingProvider> _injectBindingProviderMock;

        [SetUp]
        public void SetUp()
        {
            _injectBindingProviderMock = new Mock<IInjectBindingProvider>();
            _injectBindingProvider = _injectBindingProviderMock.Object;
        }

        [Test]
        public void Constructor_WithNullForInjectBindingProvider_ThrowsException()
        {
            // ReSharper disable once ObjectCreationAsStatement
            var exception = Assert.Throws<ArgumentNullException>(() => new InjectConfiguration(null));

            Assert.That(exception.ParamName, Is.EqualTo("injectBindingProvider"));
        }

        [Test]
        public void Initialize_WithNullForContext_ThrowsException()
        {
            var sut = new InjectConfiguration(_injectBindingProvider);

            var exception = Assert.Throws<ArgumentNullException>(() => sut.Initialize(null));

            Assert.That(exception.ParamName, Is.EqualTo("context"));
        }
    }
}