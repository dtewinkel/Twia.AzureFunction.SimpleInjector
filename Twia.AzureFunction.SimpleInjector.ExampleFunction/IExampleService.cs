using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public interface IExampleService
{
    Task<IActionResult> ShowInjection(HttpRequest request);
}