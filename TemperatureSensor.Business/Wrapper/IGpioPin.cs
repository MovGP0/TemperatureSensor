using System;
using Windows.Devices.Gpio;

namespace TemperatureSensor.Business
{
    public interface IGpioPin : IDisposable
    {
        bool IsDriveModeSupported(GpioPinDriveMode driveMode);
        GpioPinDriveMode GetDriveMode();
        void SetDriveMode(GpioPinDriveMode value);
        void Write(GpioPinValue value);
        GpioPinValue Read();
        TimeSpan DebounceTimeout { get; set; }
        int PinNumber { get; }
        GpioSharingMode SharingMode { get; }
    }
}