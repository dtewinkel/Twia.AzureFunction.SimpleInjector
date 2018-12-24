using System;
using Microsoft.Azure.WebJobs.Logging;
using Microsoft.Extensions.Logging;

namespace Twia.AzureFunction.SimpleInjector.Config
{
    public class LoggerAdapter<T> : ILogger<T>
    {
        private readonly ILogger _logger;

        public LoggerAdapter(ILoggerFactory factory)
            => this._logger = factory.CreateLogger(LogCategories.CreateFunctionUserCategory(nameof(T)));

        public IDisposable BeginScope<TState>(TState state) 
            => this._logger.BeginScope(state);
        
        public bool IsEnabled(LogLevel logLevel) 
            => this._logger.IsEnabled(logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            => this._logger.Log(logLevel, eventId, state, exception, formatter);
    }
}