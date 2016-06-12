using FluentAssertions;
using TemperatureSensor.Contracts;
using Xunit;

namespace TemperatureSensor.Business.Tests
{
    public sealed class MeasurementExtensionsTests
    {
        [Theory]
        [InlineData(0f, 0)]
        [InlineData(0.5f, 1)]
        [InlineData(-0.5f, -1)]
        [InlineData(50f, 100)]
        [InlineData(-50f, -100)]
        public void MeasurementToTemperature(float expected, short value)
        {
            var measurement = new Measurement(value);
            Temperature temperature = measurement.ToTemperature();
            temperature.DegreesCelsius.ShouldBeEquivalentTo(expected);
        }
    }
}