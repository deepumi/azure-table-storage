using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Azure.TableStorage.PerformanceTest.MicrosoftAzureStorage
{
    public sealed class MessageEntityMs : Microsoft.WindowsAzure.Storage.Table.ITableEntity
    {
        public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            
        }

        public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            return null;
        }

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public string ETag { get; set; }
    }
}
