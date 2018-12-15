using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Twia.AzureFunction.SimpleInjector.ExampleFunction
{
    public interface IExampleService
    {
        Task<IActionResult> ShowInjection(HttpRequest request);
    }
}