using System;
using EnsureThat;
using Microsoft.Extensions.Logging;

namespace Twia.AzureFunction.SimpleInjector.Config
{
    internal class LoggerAdapter<T> : ILogger<T>
    {
        private readonly ILogger _logger;

        public LoggerAdapter(ILoggerFactory factory)
        {
            EnsureArg.IsNotNull(factory, nameof(factory));

            _logger = factory.CreateLogger<T>();
        }

        public IDisposable BeginScope<TState>(TState state) 
            => _logger.BeginScope(state);
        
        public bool IsEnabled(LogLevel logLevel) 
            => _logger.IsEnabled(logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            => _logger.Log(logLevel, eventId, state, exception, formatter);
    }
}