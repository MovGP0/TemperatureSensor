using System.Diagnostics;
using Windows.Devices.Gpio;

namespace TemperatureSensor.Business
{
    //TODO: this is for demonstration only. delete it when not needed anymore.
    public sealed class Foo
    {
        private IGpioController Controller { get; }

        public Foo()
        {
            Controller = null;
        }

        public void Bar()
        {
            var pin = Controller.OpenPin(14);
            var value = pin.Read();
            Debug.Write(value == GpioPinValue.High ? "High" : "Low");
        }
    }
}