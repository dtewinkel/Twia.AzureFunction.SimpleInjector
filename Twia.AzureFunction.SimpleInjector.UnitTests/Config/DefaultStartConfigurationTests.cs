using FluentAssertions;
using NUnit.Framework;

namespace Twia.AzureFunction.SimpleInjector.UnitTests.Config
{
    [TestFixture]
    public class DefaultStartConfigurationTests
    {
        [Test]
        public void AddILogger_ReturnsExpectedDefault()
        {
            var sut = new DefaultStartConfiguration();

            sut.AddILogger.Should().BeTrue("The default value is 'true'.");
        }

        [Test]
        public void AddILoggerOfT_ReturnsExpectedDefault()
        {
            var sut = new DefaultStartConfiguration();

            sut.AddILoggerOfT.Should().BeFalse("The default value is 'false'.");
        }
    }
}