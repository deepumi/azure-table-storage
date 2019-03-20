# azure-table-storage
SDK for azure table operations

## Performance benchmark for 100 Get operations!


|      Method | Iteration |     Mean |    Error |   StdDev | Rank | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------ |---------- |---------:|---------:|---------:|-----:|------------:|------------:|------------:|--------------------:|
|    GetAsync |       100 | 536.1 ms | 10.64 ms | 10.92 ms |    1 |           - |           - |           - |             6.63 KB |
| MS_GetAsync |       100 | 592.9 ms | 11.77 ms | 14.89 ms |    2 |           - |           - |           - |             17.3 KB |

### Azure.TableStorage SDK call
```csharp
private static TableClient _client = TableStorageAccount.Parse(TableConnection.ConnectionString)
                                     .CreateTableClient();
public async Task GetAsync()
{
    var entity = new MessageEntity { PartitionKey = "006c7e09-261b-4081-a021-db8032bcc01b", RowKey = "demo" };
    
    var tableResult = await _client.GetAsync<MessageEntity>(entity);
    
    Assert.IsTrue(tableResult.IsSuccessStatusCode && tableResult.Result != null);
}
```

### Windows Azure SDK Call

```csharp
 private static CloudTableClient _tableClient =  CloudStorageAccount.Parse(TableConnection.ConnectionString)
                                                 .CreateCloudTableClient();
  internal static async Task MS_GetAsync()
  {
      var entity = new MessageEntityMs { PartitionKey = "006c7e09-261b-4081-a021-db8032bcc01b", RowKey = "demo" };

      var table = _tableClient.GetTableReference("DemoTable");

      var retrieveOperation = TableOperation.Retrieve<MessageEntityMs>(entity.PartitionKey, entity.RowKey);

      var retrievedResult = await table.ExecuteAsync(retrieveOperation);

      var result = retrievedResult?.Result as MessageEntityMs;
      
      Assert.IsTrue(result != null);
  }
```
