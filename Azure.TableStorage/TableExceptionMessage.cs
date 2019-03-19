using System;
using System.Net;

namespace Azure.TableStorage
{
    public sealed class TableExceptionMessage : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public TableExceptionMessage(string message) : base(message) { }
     
        public TableExceptionMessage(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}