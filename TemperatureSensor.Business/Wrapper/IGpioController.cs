using Windows.Devices.Gpio;

namespace TemperatureSensor.Business
{
    public interface IGpioController
    {
        IGpioPin OpenPin(int pinNumber);
        IGpioPin OpenPin(int pinNumber, GpioSharingMode sharingMode);
        bool TryOpenPin(int pinNumber, GpioSharingMode sharingMode, out GpioPin pin, out GpioOpenStatus openStatus);
        int PinCount { get; }
    }
}