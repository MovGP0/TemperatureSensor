using System;

namespace TemperatureSensor.Contracts
{
    /// <summary>
    /// Thermodynamic Temperature
    /// </summary>
    /// <remarks>
    /// Note that in the metric system, differences of thermodynamic temperatures 
    /// are given in Kelvins and not in Degrees Celsius.
    /// </remarks>
    public struct Temperature
    {
        private const float AbsoluteZeroInKelvins = 0.0f;
        private const float PlanckTemperatureInKelvins = 1.416786e32f;
        private const float AbsoluteZeroToDegreesCelsiusOffset = 273.15f;

        /// <summary>
        /// Temperature given in Degrees Celsius
        /// </summary>
        public float DegreesCelsius => Kelvins - AbsoluteZeroToDegreesCelsiusOffset;

        /// <summary>
        /// Temperature given in Kelvins
        /// </summary>
        public float Kelvins { get; }
        
        private Temperature(float kelvins)
        {
            if(kelvins < AbsoluteZeroInKelvins)
                throw new ArgumentOutOfRangeException(nameof(kelvins), "Must be greater or equal absolute zero.");

            if(kelvins > PlanckTemperatureInKelvins)
                throw new ArgumentOutOfRangeException(nameof(kelvins), "Must be smaller than the planck temperature.");

            Kelvins = kelvins;
        }

        public static Temperature FromDegreesCelsius(float degreesCelsius)
        {
            var kelvins = degreesCelsius + AbsoluteZeroToDegreesCelsiusOffset;
            return FromKelvins(kelvins);
        }

        public static Temperature FromKelvins(float kelvins)
        {
            return new Temperature(kelvins);
        }

        /// <summary>
        /// Minimum Thermodynamic Temperature (absolute zero)
        /// </summary>
        public static Temperature MinValue => new Temperature(AbsoluteZeroInKelvins);

        /// <summary>
        /// Maximum Thermodynamic Temperature (Planck-Temperature; absolute hot)
        /// </summary>
        public static Temperature MaxValue => new Temperature(PlanckTemperatureInKelvins);
    }
}
