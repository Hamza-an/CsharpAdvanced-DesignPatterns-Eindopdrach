using System;
using System.Collections.Generic;

namespace TemperatureMonitor.Api.Patterns.Behavioral.Observer
{
    public class SerialPortReceiver : IDataSubject
    {
        private readonly List<IDataObserver> _observers = new List<IDataObserver>();

        public void Attach(IDataObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Detach(IDataObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers(SensorDataPayload data)
        {
            foreach (var observer in _observers)
            {
                observer.Update(data);
            }
        }

        public void ReceiveDataFromPico(string sensorType, string rawValue)
        {
            Console.WriteLine($"[Hardware] Ruwe data ontvangen: {sensorType} - {rawValue}");

            var payload = new SensorDataPayload
            {
                SensorType = sensorType,
                RawValue = rawValue
            };

            NotifyObservers(payload);
        }
    }
}