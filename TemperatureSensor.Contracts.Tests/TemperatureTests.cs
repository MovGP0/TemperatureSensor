using System;
using FluentAssertions;
using Xunit;

namespace TemperatureSensor.Contracts.Tests
{
    public sealed class TemperatureTests
    {
        public sealed class FromKelvin
        {
            [Fact]
            public void NewAbsoluteZeroTemperature()
            {
                Action newTemp = () => Temperature.FromKelvins(0.0f);
                newTemp.ShouldNotThrow();
            }
        }

        public sealed class FromDegreesCelsius
        {
            [Theory]
            [InlineData(0.0f)]
            [InlineData(0.1f)]
            [InlineData(-0.1f)]
            [InlineData(1.416786e32f)]
            [InlineData(-273.15f)]
            public void DegreesCelsiusPropertyShouldMatchGivenTemperature(float value)
            {
                var temperature = Temperature.FromDegreesCelsius(value);
                temperature.DegreesCelsius.ShouldBeEquivalentTo(value);
            }

            [Fact]
            public void ShouldThrowOnNegativeTemperatures()
            {
                Action newTemp = () => Temperature.FromDegreesCelsius(-273.16f);
                newTemp.ShouldThrow<ArgumentOutOfRangeException>();
            }

            [Fact]
            public void ShouldThrowOnImpossiblyHotTemperatures()
            {
                Action newTemp = () => Temperature.FromDegreesCelsius(float.MaxValue);
                newTemp.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        public sealed class MinValue
        {
            public void ShouldBeEqualToAbsoluteZero()
            {
                const float absoluteZeroInDegreesCelsius = -273.15f;
                var temperature = Temperature.MinValue;
                temperature.DegreesCelsius.ShouldBeEquivalentTo(absoluteZeroInDegreesCelsius);
            }
        }
    }
}
