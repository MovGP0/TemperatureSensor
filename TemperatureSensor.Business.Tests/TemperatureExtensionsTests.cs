using FluentAssertions;
using TemperatureSensor.Contracts;
using Xunit;

namespace TemperatureSensor.Business.Tests
{
    public sealed class TemperatureExtensionsTests
    {
        [Theory]
        [InlineData(0f, 0)]
        [InlineData(0.5f, 1)]
        [InlineData(-0.5f, -1)]
        [InlineData(50f, 100)]
        [InlineData(-50f, -100)]
        public void TemperatureToMeasurement(float value, short expected)
        {
            var temperature = Temperature.FromDegreesCelsius(value);
            Measurement measurement = temperature.ToMeasurement();
            measurement.Value.ShouldBeEquivalentTo(expected);
        }
    }
}
