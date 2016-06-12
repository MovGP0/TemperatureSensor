using System;

namespace TemperatureSensor.Business
{
    public static class ServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider container)
        {
            return (T) container.GetService(typeof (T));
        }
    }
}