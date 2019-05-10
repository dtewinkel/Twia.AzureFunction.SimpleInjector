using FluentAssertions;
using NUnit.Framework;

namespace Twia.AzureFunction.SimpleInjector.UnitTests
{
    [TestFixture]
    public class FullLoggingStartConfigurationTests
    {
        [Test]
        public void AddILogger_ReturnsExpectedDefault()
        {
            var sut = new FullLoggingStartConfiguration();

            sut.AddILogger.Should().BeTrue("The expected value is 'true'.");
        }

        [Test]
        public void AddILoggerOfT_ReturnsExpectedDefault()
        {
            var sut = new FullLoggingStartConfiguration();

            sut.AddILoggerOfT.Should().BeTrue("The expected value is 'true'.");
        }
    }
}