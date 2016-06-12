using System;
using FluentAssertions;
using Xunit;

namespace TemperatureSensor.Business.Tests
{
    public sealed class DependencyInjectionTests
    {
        [Fact]
        public void Create()
        {
            Action create = () => DependencyInjection.Create();
            create.ShouldNotThrow();
        }

        [Fact]
        public void GetService_GpioController()
        {
            var container = DependencyInjection.Create();
            var controller = container.GetService<IGpioController>();
            controller.Should().NotBeNull();
        }
    }
    
}