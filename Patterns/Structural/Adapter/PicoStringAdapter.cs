using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TemperatureMonitor.Api.Patterns.Structural.Adapter
{
    public class PicoStringAdapter : IRawDataAdapter
    {
        public Dictionary<string, string> Adapt(string rawPicoData)
        {
            var parsedData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(rawPicoData))
                return parsedData;

            rawPicoData = rawPicoData.Replace("\"", "").Trim();

            string pattern = @"(serialnumber|serial|batterylevel|batlevel|batmax|batmin|bat|temp|hum|state|manufac|manu|type|error|v3|v2|v):";

            string[] parts = Regex.Split(rawPicoData, pattern, RegexOptions.IgnoreCase);

            for (int i = 1; i < parts.Length - 1; i += 2)
            {
                string key = parts[i].Trim().ToLower();
                string value = parts[i + 1].Trim();

                parsedData[key] = value;
            }

            return parsedData;
        }
    }
}