using System.Net.Http;

namespace Azure.TableStorage.Http
{
    internal sealed class HttpClientFactory /*: IHttpClientFactory*/
    {
        private static readonly HttpClient _client = new HttpClient(new RetryHandler(), false);

        static HttpClientFactory()
        {
            _client.DefaultRequestHeaders.ExpectContinue = false;
            _client.DefaultRequestHeaders.Add("x-ms-version", Constants.MsVersion);
            _client.DefaultRequestHeaders.Add("Accept-Charset", Constants.Utf8);
            _client.DefaultRequestHeaders.Add("Accept", Constants.Accept);
        }

        public HttpClient Client => _client;
    }
}
