using System;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Twia.AzureFunction.SimpleInjector.Config;

namespace Twia.AzureFunction.SimpleInjector.UnitTests.Config
{
    [TestFixture]
    public class LoggerAdapterTests
    {
        private ILoggerFactory _loggerFactory;
        private Mock<ILoggerFactory> _loggerFactoryMock;
        private ILogger _internalLogger;
        private Mock<ILogger> _internalLoggerMock;

        private LoggerAdapter<LoggerAdapterTests> _sut;

        [SetUp]
        public void SetUp()
        {
            _loggerFactoryMock = new Mock<ILoggerFactory>();
            _loggerFactory = _loggerFactoryMock.Object;
            _internalLoggerMock = new Mock<ILogger>();
            _internalLogger = _internalLoggerMock.Object;
            _loggerFactoryMock
                .Setup(mock => mock.CreateLogger(It.IsAny<string>()))
                .Returns(_internalLogger);
            _sut = new LoggerAdapter<LoggerAdapterTests>(_loggerFactory);
        }

        [Test]
        public void Constructor_WithNullForFactory_ThrowsException()
        {
            // ReSharper disable once ObjectCreationAsStatement
            var exception = Assert.Throws<ArgumentNullException>(() => new LoggerAdapter<string>(null));

            Assert.That(exception.ParamName, Is.EqualTo("factory"));
        }

        [Test]
        public void BeginScope_PassesStateToInternalLogger()
        {
            const string state = "StateObject";
            var expectedIDisposable = Mock.Of<IDisposable>();
            _internalLoggerMock.Setup(mock => mock.BeginScope(state))
                .Returns(expectedIDisposable);

            var iDisposable = _sut.BeginScope(state);

            Assert.That(iDisposable, Is.EqualTo(expectedIDisposable));
        }

        [Test]
        public void Log_PassesParametersToInternalLogger()
        {
            const string state = "StateObject";
            var logLevel = LogLevel.Error;
            var eventId = new EventId();
            var exception = new Exception();
            Func<string, Exception, string> formatter = (string x, Exception y) => x;

            _sut.Log(logLevel, eventId, state, exception, formatter);

            _internalLoggerMock.Verify(mock => mock.Log(logLevel, eventId, state, exception, formatter), Times.Once);
        }

        [Test]
        [TestCase(LogLevel.Critical, true)]
        [TestCase(LogLevel.Information, false)]
        public void IsEnabled_PassesLogLevelToInternalLogger_AndReturnsResult(LogLevel logLevel, bool expectedResult)
        {
            _internalLoggerMock
                .Setup(mock => mock.IsEnabled(logLevel))
                .Returns(expectedResult);

            var result = _sut.IsEnabled(logLevel);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}