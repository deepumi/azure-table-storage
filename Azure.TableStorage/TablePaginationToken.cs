﻿using System.Net.Http.Headers;
using Azure.TableStorage.Extensions;

namespace Azure.TableStorage
{
    public sealed class TablePaginationToken
    {
        public string NextPartitionKey { get; }

        public string NextRowKey { get; }

        public TablePaginationToken(HttpHeaders headers)
        {
            NextPartitionKey = headers.GetValues(TableConstants.NextPartitionKey).First();
            NextRowKey = headers.GetValues(TableConstants.NextRowKey).First();
        }
    }
}
