using Windows.Devices.Gpio;

namespace TemperatureSensor.Business
{
    public sealed class GpioControllerWrapper : IGpioController
    {
        private GpioController Controller { get; }

        public GpioControllerWrapper(GpioController controller)
        {
            Controller = controller;
        }

        public GpioControllerWrapper() : this(GpioController.GetDefault())
        {
        }

        public IGpioPin OpenPin(int pinNumber)
        {
            return new GpioPinWrapper(Controller.OpenPin(pinNumber));
        }

        public IGpioPin OpenPin(int pinNumber, GpioSharingMode sharingMode)
        {
            return new GpioPinWrapper(Controller.OpenPin(pinNumber, sharingMode));
        }

        public bool TryOpenPin(int pinNumber, GpioSharingMode sharingMode, out GpioPin pin, out GpioOpenStatus openStatus)
        {
            return Controller.TryOpenPin(pinNumber, sharingMode, out pin, out openStatus);
        }

        public int PinCount => Controller.PinCount;
    }
}
