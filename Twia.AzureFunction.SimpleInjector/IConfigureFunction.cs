using Microsoft.Azure.WebJobs;

namespace Twia.AzureFunction.SimpleInjector
{
    /// <summary>
    /// Interface that can be implemented on top of an instance that implements IStartup.
    /// </summary>
    /// <remarks>
    /// The presence of this interface on a class will be detected by SimpleInjectorStartup and this method will be
    /// called to configure the function before SimpleInjector is configured.
    /// </remarks>
    public interface IConfigureFunction
    {
        /// <summary>
        /// Allow access to the builder before configuring SimpleInjector.
        /// </summary>
        /// <param name="builder">The builder for this Azure Function.</param>
        void ConfigureFunction(IWebJobsBuilder builder);
    }
}
