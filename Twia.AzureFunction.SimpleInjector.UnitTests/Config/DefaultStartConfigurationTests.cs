using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twia.AzureFunction.SimpleInjector.Config;

namespace Twia.AzureFunction.SimpleInjector.UnitTests.Config
{
    [TestClass]
    public class DefaultStartConfigurationTests
    {
        [TestMethod]
        public void AddILogger_ReturnsExpectedDefault()
        {
            var sut = new DefaultStartConfiguration();

            sut.AddILogger.Should().BeTrue("The default value is 'true'.");
        }

        [TestMethod]
        public void AddILoggerOfT_ReturnsExpectedDefault()
        {
            var sut = new DefaultStartConfiguration();

            sut.AddILoggerOfT.Should().BeFalse("The default value is 'false'.");
        }

    }
}