using System.Threading;
using System.Threading.Tasks;

namespace Azure.TableStorage.Http
{
    internal sealed class RetryPolicy
    {
        private const int MaxAttempt = 4;

        private int _attempt = 1;

        private int _sleepTime;

        internal bool Next()
        {
            _sleepTime = 1000 * _attempt; //exponential backoff!  1) 1000ms 2) 2000 ms 3) no need to sleep before exiting!

            return _attempt < MaxAttempt;
        }

        internal bool IsMaxAttempt => _attempt >= MaxAttempt || !ConitnueDelay();

        internal async Task Delay(CancellationToken token)
        {
            if (ConitnueDelay()) await Task.Delay(_sleepTime, token);

            _attempt++;
        }

        private bool ConitnueDelay() => _attempt < MaxAttempt - 1;
    }
}
