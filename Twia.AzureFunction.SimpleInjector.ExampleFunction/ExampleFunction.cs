using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Twia.AzureFunction.SimpleInjector;

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

    [FunctionName(nameof(FindBlobs))]
    [return: Queue("FoundBlobs", Connection = "QueueConnectionString")]
    public static BlobFoundMessage FindBlobs(
        [BlobTrigger("input/{name}", Connection = "BlobConnectionString")]
        Stream myBlob,
        string name)
    {
        return new BlobFoundMessage
        {
            Name = name
        };
    }

}

public class BlobFoundMessage
{
    public string Name { get; set; }
}