namespace TemperatureMonitor.Api.Patterns.Behavioral.Strategy
{
    public interface ITemperatureConversionStrategy
    {
        double ConvertToCelsius(string rawData);
    }
}