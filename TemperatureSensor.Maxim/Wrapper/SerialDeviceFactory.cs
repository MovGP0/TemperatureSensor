using System;
using System.Threading.Tasks;
using Windows.Devices.SerialCommunication;

namespace TemperatureSensor.Maxim
{
    public sealed class SerialDeviceFactory : ISerialDeviceFactory
    {
        public async Task<ISerialDevice> FromIdAsync(string deviceId)
        {
            var serialDevice = await SerialDevice.FromIdAsync(deviceId);
            return new SerialDeviceWrapper(serialDevice);
        }
    }
}
