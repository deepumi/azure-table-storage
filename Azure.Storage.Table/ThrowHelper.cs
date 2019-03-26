using System.Net;

namespace Azure.Storage.Table
{
    internal static class ThrowHelper
    {
        internal static TableExceptionMessage Throw(string message)
        {
            throw new TableExceptionMessage(message);
        }

        internal static TableExceptionMessage Throw(string message, HttpStatusCode code)
        {
            throw new TableExceptionMessage(message, code);
        }
    }
}