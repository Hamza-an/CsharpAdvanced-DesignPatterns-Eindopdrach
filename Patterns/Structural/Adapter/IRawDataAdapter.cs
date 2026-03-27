using System.Collections.Generic;

namespace TemperatureMonitor.Api.Patterns.Structural.Adapter
{
    public interface IRawDataAdapter
    {
        Dictionary<string, string> Adapt(string rawPicoData);
    }
}