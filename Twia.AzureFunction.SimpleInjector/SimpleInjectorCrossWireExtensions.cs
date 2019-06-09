using System;
using EnsureThat;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace Twia.AzureFunction.SimpleInjector
{
    public static class SimpleInjectorCrossWireExtensions
    {
        /// <summary>
        /// Cross-wire a service from the <paramref name="serviceProvider"/> as a singleton in the <paramref name="container"/>.
        /// </summary>
        /// <typeparam name="TInstance">Type of the instance to get from <paramref name="serviceProvider"/>.</typeparam>
        /// <param name="container">The container to register the singleton in.</param>
        /// <param name="serviceProvider">The service provider to get the <typeparamref name="TInstance"/> instance from.</param>
        /// <returns>the registered instance.</returns>
        public static TInstance CrossWireSingleton<TInstance>(this Container container, IServiceProvider serviceProvider)
            where TInstance : class
        {
            EnsureArg.IsNotNull(container, nameof(container));
            EnsureArg.IsNotNull(serviceProvider, nameof(serviceProvider));

            var instance = serviceProvider.GetRequiredService<TInstance>();
            container.RegisterSingleton(() => instance);
            return instance;
        }
    }
}