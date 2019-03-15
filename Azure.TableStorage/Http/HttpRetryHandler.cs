using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.TableStorage.Http
{
    internal sealed class HttpRetryHandler : HttpClientHandler
    {
        internal HttpRetryHandler() => AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            var response = default(HttpResponseMessage);

            var policy = new RetryPolicy();

            while (policy.Next())
            {
                response = await base.SendAsync(request, token).ConfigureAwait(false);

                if (response != null && (response.IsSuccessStatusCode || !IsTransientStatus(response.StatusCode))) break;

                await policy.Delay(token);
            }

            return response;
        }

        private static bool IsTransientStatus(HttpStatusCode s) => s == HttpStatusCode.RequestTimeout ||
                                                                   s == HttpStatusCode.ServiceUnavailable ||
                                                                   s == HttpStatusCode.GatewayTimeout ||
                                                                   s == HttpStatusCode.BadGateway ||
                                                                   s == HttpStatusCode.InternalServerError;

    }
}
