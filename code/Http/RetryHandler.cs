using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.TableStorage.Http
{
    internal sealed class RetryHandler : HttpClientHandler
    {
        internal RetryHandler() => AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            var retry =  new RetryPolicy();

            while (retry.Next())
            {
                try
                {
                    var response = await base.SendAsync(request, token).ConfigureAwait(false);

                    if (response != null && response.IsSuccessStatusCode) return response;

                    await retry.Delay(token);
                }
                catch
                {
                    if (retry.IsMaxAttempt) throw;

                    await retry.Delay(token);
                }
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}
