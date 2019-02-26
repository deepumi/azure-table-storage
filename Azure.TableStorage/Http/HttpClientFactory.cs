using System.Net.Http;
using System.Threading;

namespace Azure.TableStorage.Http
{
    internal sealed class HttpClientFactory /*: IHttpClientFactory*/
    {
        private static readonly HttpClient _client = new HttpClient(new RetryHandler(), false);

        static HttpClientFactory()
        {
            //set base URL
            _client.DefaultRequestHeaders.ExpectContinue = false;
            _client.DefaultRequestHeaders.Add("x-ms-version", Constants.MsVersion);
            _client.DefaultRequestHeaders.Add("Accept-Charset", Constants.Utf8);
            _client.DefaultRequestHeaders.Add("Accept", Constants.Accept);
            _client.Timeout = Timeout.InfiniteTimeSpan;

            //per URL
            //_client.DefaultRequestHeaders.ConnectionClose = false;
            // ServicePointManager.FindServicePoint(new Uri("")).ConnectionLeaseTimeout = 60 * 1000;
        }

        internal HttpClient Client => _client;
    }
}
