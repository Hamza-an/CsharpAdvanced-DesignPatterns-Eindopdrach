using System;
using TemperatureMonitor.Api.Patterns.Creational;
using TemperatureMonitor.Api.Patterns.Behavioral.Strategy;

namespace TemperatureMonitor.Api.Patterns.Behavioral.Observer
{
    public class TemperatureDataProcessor : IDataObserver
    {
        public void Update(SensorDataPayload data)
        {
            Console.WriteLine($"[Processor] Data binnengekregen voor sensor: {data.SensorType}");

            try
            {
                var sensor = SensorFactory.CreateSensor(data.SensorType);

                ITemperatureConversionStrategy strategy = data.SensorType.ToUpper() switch
                {
                    "DHT11" => new Dht11ConversionStrategy(),
                    "DS18B20" => new Ds18b20ConversionStrategy(),
                    "PICO_INTERNAL" => new PicoInternalConversionStrategy(),
                    _ => throw new NotImplementedException()
                };

                var converter = new TemperatureConverter(strategy);
                double finalTemperature = converter.ProcessRawData(data.RawValue);

                Console.WriteLine($"[Processor] Succes! De temperatuur van {sensor.Name} is {finalTemperature}°C.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Processor Fout] Kan data niet verwerken: {ex.Message}");
            }
        }
    }
}