using System.Collections.Generic;

namespace TemperatureMonitor.Api.Patterns.Behavioral.Observer
{
    public class SensorDataPayload
    {
        public string SensorType { get; set; }
        public string RawValue { get; set; }
    }

    public interface IDataObserver
    {
        void Update(SensorDataPayload data);
    }

    public interface IDataSubject
    {
        void Attach(IDataObserver observer);
        void Detach(IDataObserver observer);
        void NotifyObservers(SensorDataPayload data);
    }
}