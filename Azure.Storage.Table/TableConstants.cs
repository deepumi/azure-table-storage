using System;

namespace Azure.Storage.Table
{
    internal static class TableConstants
    {
        internal const string Accept = "application/json;odata=nometadata";

        internal const string Utf8 = "UTF-8";

        internal const string MsVersion = "2018-03-28";

        internal const string NextPartitionKey = "x-ms-continuation-NextPartitionKey";

        internal const string NextRowKey = "x-ms-continuation-NextRowKey";

        internal const string ReturnNoContent = "return-no-content";

        internal static readonly TimeSpan DefaultRequestTimeout = TimeSpan.FromSeconds(30);
    }
}