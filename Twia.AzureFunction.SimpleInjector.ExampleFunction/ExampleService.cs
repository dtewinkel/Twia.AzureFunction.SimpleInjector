using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class ExampleService : IExampleService
{
    private readonly ILogger _logger;
    private readonly ILogger<ExampleService> _loggerOfT;
    private readonly IExampleServiceSettings _settings;

    public ExampleService(ILogger logger, ILogger<ExampleService> loggerOfT, IExampleServiceSettings settings)
    {
        _logger = logger;
        _loggerOfT = loggerOfT;
        _settings = settings;
    }

    public async Task<IActionResult> ShowInjection(HttpRequest request)
    {
        var greeting = new
        {
            Greeting = _settings.ExampleGreeting,
            GreetingFromEnvironment = _settings.ExampleGreetingFromEnvironment
        };
        _logger.LogInformation("In service.");
        _logger.LogWarning("Working...");
        _loggerOfT.LogWarning("WorkingOfT...");
        return await Task.FromResult(new OkObjectResult(greeting));
    }
}