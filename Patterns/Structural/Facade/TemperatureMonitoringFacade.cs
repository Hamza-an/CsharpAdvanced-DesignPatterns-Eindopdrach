using System;
using System.Collections.Generic;
using TemperatureMonitor.Api.Models;
using TemperatureMonitor.Api.Patterns.Structural.Adapter;
using TemperatureMonitor.Api.Patterns.Behavioral.Strategy;

namespace TemperatureMonitor.Api.Patterns.Structural.Facade
{
    public interface ITemperatureMonitoringFacade
    {
        ProcessedSensorData ProcessRawString(string rawData);
    }

    public class TemperatureMonitoringFacade : ITemperatureMonitoringFacade
    {
        private readonly IRawDataAdapter _adapter;

        public TemperatureMonitoringFacade(IRawDataAdapter adapter)
        {
            _adapter = adapter;
        }

        public ProcessedSensorData ProcessRawString(string rawData)
        {
            var result = new ProcessedSensorData { IsValid = false };

            try
            {
                Dictionary<string, string> parsedData = _adapter.Adapt(rawData);

                if (parsedData.Count == 0)
                {
                    result.ErrorMessage = "Geen data gevonden in de input.";
                    return result;
                }

                parsedData.TryGetValue("type", out string sensorType);
                parsedData.TryGetValue("manu", out string manu);
                if (string.IsNullOrEmpty(manu)) parsedData.TryGetValue("manufac", out manu); 
                parsedData.TryGetValue("temp", out string rawTemp);
                parsedData.TryGetValue("state", out string state);
                parsedData.TryGetValue("bat", out string battery);

                result.SensorType = sensorType ?? "Unknown";
                result.Manufacturer = manu ?? "Unknown";
                result.State = state ?? "Unknown";
                result.BatteryStatus = battery ?? "Unknown";

                if (!string.IsNullOrEmpty(rawTemp))
                {
                    ITemperatureConversionStrategy strategy = new Ds18b20ConversionStrategy();
                    var converter = new TemperatureConverter(strategy);

                    result.TemperatureCelsius = converter.ProcessRawData(rawTemp);
                    result.IsValid = true;
                }
                else
                {
                    result.ErrorMessage = "Geen temperatuurwaarde gevonden in de data.";
                }

                return result;
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ErrorMessage = $"Fout bij verwerken: {ex.Message}";
                return result;
            }
        }
    }
}