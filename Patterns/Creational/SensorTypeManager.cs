using System;
using System.Collections.Generic;

namespace TemperatureMonitor.Api.Patterns.Creational
{
    public sealed class SensorTypeManager
    {
        private static readonly Lazy<SensorTypeManager> _instance =
            new Lazy<SensorTypeManager>(() => new SensorTypeManager());

        public static SensorTypeManager Instance => _instance.Value;

        private readonly List<string> _supportedSensorTypes;

        private SensorTypeManager()
        {
            _supportedSensorTypes = new List<string>
            {
                "DHT11",
                "DS18B20",
                "PICO_INTERNAL"
            };
        }
        public bool IsValidSensorType(string sensorType)
        {
            if (string.IsNullOrWhiteSpace(sensorType)) return false;
            return _supportedSensorTypes.Contains(sensorType.ToUpper());
        }

        public IReadOnlyList<string> GetSupportedTypes()
        {
            return _supportedSensorTypes.AsReadOnly();
        }
    }
}