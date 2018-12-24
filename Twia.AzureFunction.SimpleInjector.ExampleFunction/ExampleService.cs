using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Twia.AzureFunction.SimpleInjector.ExampleFunction
{
    public class ExampleService : IExampleService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public ExampleService(ILogger<ExampleService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> ShowInjection(HttpRequest request)
        {
            var greeting = _configuration["ExampleGreeting"] ?? "Hello World!";
            _logger.LogInformation("In service.");
            _logger.LogWarning("Working...");
            return await Task.FromResult(new OkObjectResult(greeting));
        }
    }
}