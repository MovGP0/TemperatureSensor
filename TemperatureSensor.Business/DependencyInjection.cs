using System;
using SimpleInjector;

namespace TemperatureSensor.Business
{
    public static class DependencyInjection
    {
        public static IServiceProvider Create()
        {
            var container = new Container();
            container.Register<IGpioController>(() => new GpioControllerWrapper());
            return container;
        }
    }
}