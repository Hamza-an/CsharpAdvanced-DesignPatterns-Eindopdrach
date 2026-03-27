using Microsoft.AspNetCore.SignalR;
using TemperatureMonitor.Api.Hubs;
using TemperatureMonitor.Api.Models;
using TemperatureMonitor.Api.Patterns.Concurrency;
using TemperatureMonitor.Api.Patterns.Structural.Facade;
using TemperatureMonitor.Api.Patterns.Creational.Singleton;

namespace TemperatureMonitor.Api.Services
{
    public class SensorDataProcessorService : BackgroundService
    {
        private readonly SensorDataQueue _queue;
        private readonly IHubContext<SensorHub> _hubContext;
        private readonly IServiceScopeFactory _scopeFactory;

        public SensorDataProcessorService(
            SensorDataQueue queue,
            IHubContext<SensorHub> hubContext,
            IServiceScopeFactory scopeFactory)
        {
            _queue = queue;
            _hubContext = hubContext;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string rawData = await _queue.DequeueRawDataAsync(stoppingToken);

                if (!string.IsNullOrWhiteSpace(rawData))
                {
                    using var scope = _scopeFactory.CreateScope();
                    var facade = scope.ServiceProvider.GetRequiredService<ITemperatureMonitoringFacade>();

                    var processedData = facade.ProcessRawString(rawData);

                    if (processedData.IsValid)
                    {
                        var config = SensorConfigManager.Instance;

                        var frontendData = new SensorDto
                        {
                            Id = 1,
                            SerialNumber = "PICO-LIVE",
                            Name = "Live Sensor Data",
                            LastMeasurementTimestamp = DateTime.UtcNow,
                            LastMeasurements = new List<MeasurementDto>
                            {
                                new MeasurementDto { Type = "Temperature", Value = processedData.TemperatureCelsius, Unit = "Celsius" },
                                new MeasurementDto { Type = "Humidity", Value = 50.0, Unit = "Percent" },
                                new MeasurementDto { Type = "Battery", Value = double.TryParse(processedData.BatteryStatus, out double bat) ? bat : 100, Unit = "Percent" }
                            },
                            Aggregations = new AggregationsDto
                            {
                                Temperature = new AggregationDetailDto { MaxToday = config.MaxTemperatureThreshold, MinToday = config.MinTemperatureThreshold },
                                Humidity = new AggregationDetailDto { MaxToday = 100.0, MinToday = 0.0 }
                            }
                        };

                        await _hubContext.Clients.All.SendAsync("ReceiveSensorData", frontendData, stoppingToken);
                    }
                }
            }
        }
    }
}