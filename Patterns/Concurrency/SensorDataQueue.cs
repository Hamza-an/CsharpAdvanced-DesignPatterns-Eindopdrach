using System.Threading.Channels;
using System.Threading.Tasks;

namespace TemperatureMonitor.Api.Patterns.Concurrency
{
    public class SensorDataQueue
    {
        private readonly Channel<string> _queue;

        public SensorDataQueue()
        {
            _queue = Channel.CreateUnbounded<string>();
        }

        public async ValueTask EnqueueRawDataAsync(string rawData)
        {
            await _queue.Writer.WriteAsync(rawData);
        }

        public async ValueTask<string> DequeueRawDataAsync(CancellationToken cancellationToken = default)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}