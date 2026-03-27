using System;
using System.Globalization;

namespace TemperatureMonitor.Api.Patterns.Behavioral.Strategy
{
    public class Dht11ConversionStrategy : ITemperatureConversionStrategy
    {
        public double ConvertToCelsius(string rawData)
        {
            if (double.TryParse(rawData, CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }
            throw new ArgumentException("Ongeldige data ontvangen van DHT11.");
        }
    }

    public class Ds18b20ConversionStrategy : ITemperatureConversionStrategy
    {
        public double ConvertToCelsius(string rawData)
        {
            if (double.TryParse(rawData, CultureInfo.InvariantCulture, out double result))
            {
                return result / 100.0;
            }
            throw new ArgumentException("Ongeldige data ontvangen van DS18B20.");
        }
    }

    public class PicoInternalConversionStrategy : ITemperatureConversionStrategy
    {
        public double ConvertToCelsius(string rawVoltage)
        {
            if (double.TryParse(rawVoltage, CultureInfo.InvariantCulture, out double voltage))
            {
                double temperature = 27.0 - (voltage - 0.706) / 0.001721;
                return Math.Round(temperature, 2);
            }
            throw new ArgumentException("Ongeldige data ontvangen van Pico.");
        }
    }
}