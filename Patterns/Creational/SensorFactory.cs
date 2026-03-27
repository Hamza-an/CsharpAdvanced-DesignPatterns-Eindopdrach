using System;
using TemperatureMonitor.Api.Models;

namespace TemperatureMonitor.Api.Patterns.Creational
{
    public static class SensorFactory
    {
        public static ISensor CreateSensor(string sensorType)
        {
            if (!SensorTypeManager.Instance.IsValidSensorType(sensorType))
            {
                throw new ArgumentException($"Onbekend of niet-ondersteund sensortype: {sensorType}");
            }

            return sensorType.ToUpper() switch
            {
                "DHT11" => new Dht11Sensor(),
                "DS18B20" => new Ds18b20Sensor(),
                "PICO_INTERNAL" => new PicoInternalSensor(),

                _ => throw new NotImplementedException($"Factory mist de implementatie voor {sensorType}")
            };
        }
    }
}