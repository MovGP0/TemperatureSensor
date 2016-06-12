using TemperatureSensor.Contracts;

namespace TemperatureSensor.Business
{
    public static class TemperatureExtensions
    {
        public static Measurement ToMeasurement(this Temperature temperature)
        {
            var value = (short)(temperature.DegreesCelsius*2f);
            return new Measurement(value);
        }        
    }
}