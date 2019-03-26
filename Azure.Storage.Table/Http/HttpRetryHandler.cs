using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Storage.Table.Http
{
    internal sealed class HttpRetryHandler : HttpClientHandler
    {
        internal HttpRetryHandler() => AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            HttpResponseMessage response;

            var attempt = 1;

            do
            {
                response = await base.SendAsync(request, token).ConfigureAwait(false);

                if (response != null && (response.IsSuccessStatusCode || !IsTransientStatus(response.StatusCode))) break;

                if (attempt < 3) await Task.Delay(1000 * attempt, token);

                attempt += 1;

            } while (attempt < 4);

            return response;
        }

        private static bool IsTransientStatus(HttpStatusCode s) => s == HttpStatusCode.RequestTimeout ||
                                                                   s == HttpStatusCode.ServiceUnavailable ||
                                                                   s == HttpStatusCode.GatewayTimeout ||
                                                                   s == HttpStatusCode.BadGateway ||
                                                                   s == HttpStatusCode.InternalServerError;

    }
}