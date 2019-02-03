using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Twia.AzureFunction.SimpleInjector.Config;
using Twia.AzureFunction.SimpleInjector.UnitTests.Config;

namespace Twia.AzureFunction.SimpleInjector.UnitTests
{
    [TestFixture]
    public class SimpleInjectorStartupTest
    {
        private IWebJobsBuilder _builder;
        private Mock<IWebJobsBuilder> _builderMock;
        private IServiceCollection _serviceCollection;
        private ILoggerFactory _loggerFactory;
        private Mock<ILoggerFactory> _loggerFactoryMock;
        private ILogger _internalLogger;
        private Mock<ILogger> _internalLoggerMock;


        [SetUp]
        public void SetUp()
        {
            _builderMock = new Mock<IWebJobsBuilder>();
            _builder = _builderMock.Object;
            _serviceCollection = new ServiceCollection();
            _loggerFactoryMock = new Mock<ILoggerFactory>();
            _loggerFactory = _loggerFactoryMock.Object;
            _internalLoggerMock = new Mock<ILogger>();
            _internalLogger = _internalLoggerMock.Object;
            _loggerFactoryMock
                .Setup(mock => mock.CreateLogger(It.IsAny<string>()))
                .Returns(_internalLogger);

            _builderMock
                .SetupGet(mock => mock.Services)
                .Returns(_serviceCollection);

            _serviceCollection.AddSingleton(_loggerFactory);
        }

        [Test]
        public void Configure_WithNullForBuilder_ThrowsException()
        {
            var sut = new SimpleInjectorStartup<StartupStub, ConfigurationStub>();

            var exception = Assert.Throws<ArgumentNullException>(() => sut.Configure(null));

            Assert.That(exception.ParamName, Is.EqualTo("builder"));
        }

        [Test]
        public void GenericsOverload_WithDefaultConfiguration_GetsTheRightConfig()
        {
            var sut = new SimpleInjectorStartup<StartupStub>();

            sut.Configure(_builder);

            var provider = _builder.Services.BuildServiceProvider();
            var startup = provider.GetService<IStartup>();
            Assert.That(startup, Is.TypeOf<StartupStub>());

            var serviceProviderHolder = provider.GetService<IServiceProviderHolder>();
            var logger = serviceProviderHolder.GetRequiredService(typeof(ILogger));
            Assert.That(logger, Is.SameAs(_internalLogger));
            var loggerOfT = serviceProviderHolder.GetRequiredService(typeof(ILogger<SimpleInjectorStartupTest>));
            Assert.That(loggerOfT, Is.Null);
        }

        [Test]
        public void GenericsOverload_WithNoLoggersConfigured_GetsTheRightConfig()
        {
            ConfigurationStub.AddILoggerOfTGlobal = false;
            ConfigurationStub.AddILoggerGlobal = false;
            var sut = new SimpleInjectorStartup<StartupStub, ConfigurationStub>();

            sut.Configure(_builder);

            var provider = _builder.Services.BuildServiceProvider();
            var startup = provider.GetService<IStartup>();
            Assert.That(startup, Is.TypeOf<StartupStub>());

            var serviceProviderHolder = provider.GetService<IServiceProviderHolder>();
            var logger = serviceProviderHolder.GetRequiredService(typeof(ILogger));
            Assert.That(logger, Is.Null);
            var loggerOfT = serviceProviderHolder.GetRequiredService(typeof(ILogger<SimpleInjectorStartupTest>));
            Assert.That(loggerOfT, Is.Null);
        }

        [Test]
        public void GenericsOverload_WithAllLoggersConfigured_GetsTheRightConfig()
        {
            ConfigurationStub.AddILoggerOfTGlobal = true;
            ConfigurationStub.AddILoggerGlobal = true;
            var sut = new SimpleInjectorStartup<StartupStub, ConfigurationStub>();

            sut.Configure(_builder);

            var provider = _builder.Services.BuildServiceProvider();
            var startup = provider.GetService<IStartup>();
            Assert.That(startup, Is.TypeOf<StartupStub>());

            var serviceProviderHolder = provider.GetService<IServiceProviderHolder>();
            var logger = serviceProviderHolder.GetRequiredService(typeof(ILogger));
            Assert.That(logger, Is.SameAs(_internalLogger));
            var loggerOfT = serviceProviderHolder.GetRequiredService(typeof(ILogger<SimpleInjectorStartupTest>));
            Assert.That(loggerOfT, Is.InstanceOf<ILogger>());
            Assert.That(loggerOfT, Is.TypeOf<LoggerAdapter<SimpleInjectorStartupTest>>());
        }

    }
}