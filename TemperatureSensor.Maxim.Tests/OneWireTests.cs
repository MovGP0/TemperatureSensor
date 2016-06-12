using Windows.Storage.Streams;
using FluentAssertions;
using HyperMock.Universal;
using Xunit;

namespace TemperatureSensor.Maxim.Tests
{
    public sealed class OneWireTests
    {
        public sealed class GetNewSerialPortAsync
        {
            [Fact]
            public void ShouldNotReturnNull()
            {
                var serialDeviceFactory = Mock.Create<ISerialDeviceFactory>();
                var writer = Mock.Create<IDataWriter>();
                var reader = Mock.Create<IDataReader>();

                var oneWire = new OneWire(serialDeviceFactory, _ => writer, _ => reader);
                var result = oneWire.GetNewSerialPortAsync("foo", BaudRate.High);

                result.Should().NotBeNull();
            }
        }
    }
}
