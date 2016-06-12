using TemperatureSensor.Contracts;

namespace TemperatureSensor.Business
{
    public static class MeasurementExtensions
    {
        public static Temperature ToTemperature(this Measurement measurement)
        {
            return Temperature.FromDegreesCelsius(measurement.Value / 2f);
        }
    }
}