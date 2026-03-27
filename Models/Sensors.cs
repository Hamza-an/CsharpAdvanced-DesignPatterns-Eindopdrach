namespace TemperatureMonitor.Api.Models
{
    public interface ISensor
    {
        string Name { get; }
        string Description { get; }
    }

    public class Dht11Sensor : ISensor
    {
        public string Name => "DHT11";
        public string Description => "Digitale temperatuur- en luchtvochtigheidssensor.";
    }

    public class Ds18b20Sensor : ISensor
    {
        public string Name => "DS18B20";
        public string Description => "Waterdichte 1-Wire temperatuursensor.";
    }

    public class PicoInternalSensor : ISensor
    {
        public string Name => "PICO_INTERNAL";
        public string Description => "De ingebouwde temperatuursensor van de Raspberry Pi Pico.";
    }
}