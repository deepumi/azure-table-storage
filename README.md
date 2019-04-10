# Azure.Storage.Table
A lightweight memory efficient SDK for Azure Storage Table operations. Following table operations are currently supported!

|  API | Description
|---|---|
|GetAsync<T>(ITableEntity entity)  | Get entity based on Partition key and Row key. Note: The method returns all the properties in the table which include TimeStamp, Partition key & Row key and other custom properties.|
|GetAsync<T>(ITableEntity entity, string selectProperties)  | Get entity based on Partition key and Row key with selected properties as response entity. **Note**: Multiple properties can be concated using ","|
|QueryAsync<T>(ITableEntity entity, TablePaginationToken token, TableQueryOptions options)   | Query multiple entities based on filter condition. Per offical docs maximum result per request is 1000 entities. However, through pagination support you can query more records.  |
|InsertAsync<T>(ITableEntity entity) | Insert new entity in to the table. **Note:** Use this method if table has all column properties data type as string.|
|InsertEdmTypeAsync<T>(ITableEntity entity) | Insert new entity in to the table **Note:** Use this method if table has combination of  properties data type like string and Guid or DateTime or byte[] or Double or Long.|
|UpdateAsync<T>(ITableEntity entity) | Update existing entity based on Partition key and Row key.|
|DeleteAsync<T>(ITableEntity entity) | Delete existing entity based on Partition key and Row key.|

## No Json Dependencies
Azure.Storage.Table does not support any default json serializers. However, consumer app can specify its own json serializers implemention through public interfaces. 

## Nuget
Install [D4.Azure.Storage.Table](https://www.nuget.org/packages/D4.Azure.Storage.Table/) via [Nuget](https://www.nuget.org/packages/D4.Azure.Storage.Table/)

PM> Install-Package Azure.Storage.Table

## All entites must implement ITableEntity interface. 
```csharp
public interface ITableEntity
{
    string PartitionKey { get; set; }

    string RowKey { get; set; }

    HttpContent Serialize(object entity);

    TableResult<TResult> DeSerialize<TResult>(Stream stream, HttpStatusCode statusCode) where TResult : class; // GET 

    TableQueryResult<TResult> DeSerialize<TResult>(Stream stream, TablePaginationToken paginationToken) where TResult : class; // Collection

    string TableName { get; }
}
```
Since you have multipe entites, consumer app could have a abstract class which then implements the ITableEntity interface and resuse the serializers and De-serializers logic. Sample [TableEntity.cs](https://github.com/deepumi/azure-table-storage/blob/master/Azure.Storage.Table.Test/TableEntity.cs) class can grab from here. https://github.com/deepumi/azure-table-storage/blob/master/Azure.Storage.Table.Test/TableEntity.cs.

 
## Sample

```csharp
public sealed class MessageEntity : TableEntity
{
    public string Message { get; set; }

    public MessageEntity() : base("DemoTable") { } //Pass the table name in base constructor.
}
``` 

## Insert entity operation.

### Setup table client connection.
```csharp
internal static readonly TableClient Client = TableStorageAccount.Parse("").CreateTableClient(); //Pass storage connection string.
```
### Call Insert operation.
```csharp
var entity = new MessageEntity { PartitionKey = Guid.NewGuid().ToString(), RowKey = "demo", Message = "Integration test" };

var tableResult = await Client.InsertAsync<MessageEntity>(entity);

var isSuccess = tableResult.IsSuccessStatusCode; //Return true if entity inserted successfully!
```

### Call Get operation.
```csharp
var entity = new MessageEntity { PartitionKey = "", RowKey = "demo" };

var tableResult = await Client.GetAsync<MessageEntity>(entity);

var resukt = tableResult?.Result; //
```

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
