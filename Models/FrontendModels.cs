using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TemperatureMonitor.Api.Models
{
    public class DashboardResponse
    {
        [JsonPropertyName("items")]
        public List<SensorDto> Items { get; set; } = new List<SensorDto>();
    }

    public class SensorDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("serial_number")]
        public string SerialNumber { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("last_measurements")]
        public List<MeasurementDto> LastMeasurements { get; set; } = new List<MeasurementDto>();

        [JsonPropertyName("aggregations")]
        public AggregationsDto Aggregations { get; set; } = new AggregationsDto();

        [JsonPropertyName("last_measurement_timestamp")]
        public DateTime LastMeasurementTimestamp { get; set; }
    }

    public class MeasurementDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }

    public class AggregationsDto
    {
        [JsonPropertyName("temperature")]
        public AggregationDetailDto Temperature { get; set; } = new AggregationDetailDto { Unit = "Celsius" };

        [JsonPropertyName("humidity")]
        public AggregationDetailDto Humidity { get; set; } = new AggregationDetailDto { Unit = "Percent" };
    }

    public class AggregationDetailDto
    {
        [JsonPropertyName("max_today")]
        public double MaxToday { get; set; }

        [JsonPropertyName("min_today")]
        public double MinToday { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }
}