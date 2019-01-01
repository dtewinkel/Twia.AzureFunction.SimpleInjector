using System;
using Microsoft.Azure.WebJobs.Description;

namespace Twia.AzureFunction.SimpleInjector
{
    /// <summary>
    /// Attribute used to inject a dependency as a binding into a function.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public sealed class InjectAttribute : Attribute
    {
    }
}
