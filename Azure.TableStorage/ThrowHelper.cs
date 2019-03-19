using System.Net;

namespace Azure.TableStorage
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