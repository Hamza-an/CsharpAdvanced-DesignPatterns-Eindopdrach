namespace TemperatureMonitor.Api.Models
{
    public class ProcessedSensorData
    {
        public string SensorType { get; set; }
        public string Manufacturer { get; set; }
        public double TemperatureCelsius { get; set; }
        public string BatteryStatus { get; set; }
        public string State { get; set; }
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }
}