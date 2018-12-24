using System;
using SimpleInjector;

namespace Twia.AzureFunction.SimpleInjector
{
    /// <summary>
    /// Defines the interface of builder that creates an instance of an <see cref="IServiceProvider"/>.
    /// </summary>
    public interface IStartup
    {
        /// <summary>
        /// Creates an instance of an <see cref="IServiceProvider"/>.
        /// </summary>
        /// <returns></returns>
        void Build(Container container, IServiceProvider serviceProvider);
    }
}
