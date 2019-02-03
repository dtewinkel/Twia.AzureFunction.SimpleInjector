using System;
using SimpleInjector;

namespace Twia.AzureFunction.SimpleInjector.UnitTests
{
    internal class StartupStub : IStartup
    {
        public void Build(Container container, IServiceProvider serviceProvider)
        {
        }
    }
}