using System;

namespace TemperatureMonitor.Api.Patterns.Behavioral.Strategy
{
    public class TemperatureConverter
    {
        private ITemperatureConversionStrategy _strategy;
        public TemperatureConverter(ITemperatureConversionStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(ITemperatureConversionStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public double ProcessRawData(string rawData)
        {
            if (_strategy == null)
            {
                throw new InvalidOperationException("Er is geen conversiestrategie ingesteld.");
            }

            return _strategy.ConvertToCelsius(rawData);
        }
    }
}