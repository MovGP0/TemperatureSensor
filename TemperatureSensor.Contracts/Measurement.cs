namespace TemperatureSensor.Contracts
{
    public struct Measurement
    {
        public short Value { get; }

        public Measurement(short value)
        {
            Value = value;
        }
    }
}