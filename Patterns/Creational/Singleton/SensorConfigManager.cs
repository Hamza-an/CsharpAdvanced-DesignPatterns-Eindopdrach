namespace TemperatureMonitor.Api.Patterns.Creational.Singleton
{
    public sealed class SensorConfigManager
    {
        private static SensorConfigManager _instance;

        private static readonly object _lock = new object();

        public double MaxTemperatureThreshold { get; set; }
        public double MinTemperatureThreshold { get; set; }

        private SensorConfigManager()
        {
            MaxTemperatureThreshold = 35.0;
            MinTemperatureThreshold = 15.0;
        }

        public static SensorConfigManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SensorConfigManager();
                    }
                    return _instance;
                }
            }
        }
    }
}