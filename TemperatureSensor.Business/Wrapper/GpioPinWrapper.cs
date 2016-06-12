using System;
using System.Reactive.Linq;
using Windows.Devices.Gpio;
using Windows.Foundation;

namespace TemperatureSensor.Business
{
    public sealed class GpioPinWrapper : IGpioPin
    {
        private GpioPin Pin { get; }

        public GpioPinWrapper(GpioPin pin)
        {
            Pin = pin;
            ValueChanged = Observable.FromEvent<TypedEventHandler<GpioPin, GpioPinValueChangedEventArgs>, GpioPinValueChangedEventArgs>(eh => Pin.ValueChanged += eh, eh => Pin.ValueChanged -= eh);
        }

        public void Dispose()
        {
            Pin.Dispose();
        }

        public bool IsDriveModeSupported(GpioPinDriveMode driveMode)
        {
            return Pin.IsDriveModeSupported(driveMode);
        }

        public GpioPinDriveMode GetDriveMode()
        {
            return Pin.GetDriveMode();
        }

        public void SetDriveMode(GpioPinDriveMode value)
        {
            Pin.SetDriveMode(value);
        }

        public void Write(GpioPinValue value)
        {
            Pin.Write(value);
        }

        public GpioPinValue Read()
        {
            return Pin.Read();
        }

        public TimeSpan DebounceTimeout {
            get { return Pin.DebounceTimeout; }
            set { Pin.DebounceTimeout = value; }
        }

        public int PinNumber => Pin.PinNumber;
        public GpioSharingMode SharingMode => Pin.SharingMode;

        public IObservable<object> ValueChanged { get; }
    }
}