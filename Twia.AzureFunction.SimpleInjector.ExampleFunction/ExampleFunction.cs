using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace Twia.AzureFunction.SimpleInjector.ExampleFunction
{
    public static class ExampleFunction
    {
        [FunctionName(nameof(ShowInjection))]
        public static async Task<IActionResult> ShowInjection(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
            HttpRequest req,
            [Inject]
            IExampleService exampleService)
        {
            return await exampleService.ShowInjection(req);
        }
    }
}
