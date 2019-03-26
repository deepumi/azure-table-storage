using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace Azure.Storage.Table
{
    internal sealed class TableResponse
    {
        internal Stream ResponseStream { get; }

        internal HttpStatusCode StatusCode { get; }

        internal HttpHeaders Headers { get; }

        internal TableResponse(Stream stream, HttpStatusCode code, HttpHeaders header) : this(code)
        {
            ResponseStream = stream;

            Headers = header;
        }

        internal TableResponse(HttpStatusCode code) => StatusCode = code;
    }
}