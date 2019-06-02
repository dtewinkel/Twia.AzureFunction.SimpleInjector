using Microsoft.Azure.WebJobs;

namespace Twia.AzureFunction.SimpleInjector
{
    /// <summary>
    /// Interface that can be implemented on top of an instance that implements IStartup.
    /// </summary>
    /// <remarks>
    /// The presence of this interface ofn a class will be detected by SimpleInjectorStartup and this method will be
    /// called to configure the function before SimpleInjector is configured.
    /// </remarks>
    public interface IConfigure
    {
        /// <summary>
        /// Extend the 
        /// </summary>
        /// <param name="builder">The builder for this Azure Function.</param>
        void Configure(IWebJobsBuilder builder);
    }
}
