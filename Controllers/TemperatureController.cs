using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemperatureMonitor.Api.Models;
using TemperatureMonitor.Api.Patterns.Structural.Facade;
using TemperatureMonitor.Api.Patterns.Concurrency;

namespace TemperatureMonitor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly ITemperatureMonitoringFacade _facade;
        private readonly SensorDataQueue _queue;

        public TemperatureController(ITemperatureMonitoringFacade facade, SensorDataQueue queue)
        {
            _facade = facade;
            _queue = queue;
        }

        [HttpGet("/sensor")]
        public ActionResult<DashboardResponse> GetAllSensors()
        {
            var dummyResponse = new DashboardResponse
            {
                Items = new List<SensorDto>
                {
                    new SensorDto
                    {
                        Id = 1,
                        SerialNumber = "PICO-001",
                        Name = "Mijn Raspberry Pi Pico",
                        LastMeasurementTimestamp = DateTime.UtcNow,
                        LastMeasurements = new List<MeasurementDto>
                        {
                            new MeasurementDto { Type = "Temperature", Value = 21.5, Unit = "Celsius" },
                            new MeasurementDto { Type = "Humidity", Value = 45.0, Unit = "Percent" },
                            new MeasurementDto { Type = "Battery", Value = 100, Unit = "Percent" }
                        },
                        Aggregations = new AggregationsDto
                        {
                            Temperature = new AggregationDetailDto { MaxToday = 25.0, MinToday = 18.0 },
                            Humidity = new AggregationDetailDto { MaxToday = 60.0, MinToday = 40.0 }
                        }
                    }
                }
            };

            return Ok(dummyResponse);
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessRawData([FromBody] string rawData)
        {
            if (string.IsNullOrWhiteSpace(rawData))
            {
                return BadRequest("Geen data meegestuurd.");
            }

            await _queue.EnqueueRawDataAsync(rawData);

            return Accepted("Data is in de wachtrij geplaatst voor live verwerking!");
        }
    }
}