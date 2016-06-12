using System;
using FluentAssertions;
using Xunit;

namespace TemperatureSensor.Contracts.Tests
{
    public sealed class MeasurementTests
    {
        [Fact]
        public void Constructor()
        {
            Action newTemp = () => new Measurement((short) 0);
            newTemp.ShouldNotThrow();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(short.MaxValue)]
        [InlineData(short.MinValue)]
        public void Value(short value)
        {
            var measurement = new Measurement(value);
            measurement.Value.ShouldBeEquivalentTo(value);
        }
    }
}