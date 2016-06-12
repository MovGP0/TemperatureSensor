using System.Threading.Tasks;

namespace TemperatureSensor.Maxim
{
    public interface ISerialDeviceFactory
    {
        Task<ISerialDevice> FromIdAsync(string deviceId);
    }
}
