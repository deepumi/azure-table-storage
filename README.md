# azure-table-storage
SDK for azure table operations

## Performance benchmark for 100 Get operations!


|      Method | Iteration |     Mean |    Error |   StdDev | Rank | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------ |---------- |---------:|---------:|---------:|-----:|------------:|------------:|------------:|--------------------:|
|    GetAsync |       100 | 536.1 ms | 10.64 ms | 10.92 ms |    1 |           - |           - |           - |             6.63 KB |
| MS_GetAsync |       100 | 592.9 ms | 11.77 ms | 14.89 ms |    2 |           - |           - |           - |             17.3 KB |

### Azure.Storage.Table SDK
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

### Microsoft.WindowsAzure.Storage.Table SDK

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

## Performance benchmark for 100 Insert operations!

|         Method | Iteration |     Mean |    Error |   StdDev |   Median | Rank | Gen 0/1k Op | Allocated Memory/Op |
|--------------- |---------- |---------:|---------:|---------:|---------:|-----:|------------:|--------------------:|
|    InsertAsync |       100 | 761.9 ms | 15.99 ms | 46.65 ms | 748.2 ms |    1 |           - |             8.91 KB |
| MS_InsertAsync |       100 | 764.5 ms | 22.51 ms | 66.00 ms | 746.3 ms |    1 |   2000.0000 |            96.79 KB |


### Azure.Storage.Table SDK
```csharp
public async Task InsertAsync()
{
    var entity = new MessageEntity {PartitionKey = Guid.NewGuid().ToString(),RowKey = "demo",Message = "InsertAsync"};
    await _client.InsertAsync<MessageEntity>(entity);
}
```

### Microsoft.WindowsAzure.Storage.Table SDK

```csharp
public static async Task MS_InsertAsync()
{
    var entity = new MessageEntityMs { PartitionKey = Guid.NewGuid().ToString(), RowKey = "demo", Message = "MS_InsertAsync" };

    var table = _tableClient.GetTableReference("DemoTable");

    var insertOperation = TableOperation.Insert(entity);

    await table.ExecuteAsync(insertOperation);
}
```
