using System.Net.Http;
using System.Threading;

namespace Azure.TableStorage.Http
{
    internal static class HttpClientFactory
    {
        internal static readonly HttpClient Client =
            new HttpClient(new HttpRetryHandler(), false) { Timeout = Timeout.InfiniteTimeSpan };

        static HttpClientFactory()
        {
            Client.DefaultRequestHeaders.ExpectContinue = false;
            Client.DefaultRequestHeaders.Add("x-ms-version", TableConstants.MsVersion);
            Client.DefaultRequestHeaders.Add("Accept-Charset", TableConstants.Utf8);
            Client.DefaultRequestHeaders.Add("Accept", TableConstants.Accept);
        }
    }
}