using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Azure.Storage.Table.Test
{
    [TestClass]
    public class TableQueryOperationTest
    {
        internal static readonly TableClient Client = TableStorageAccount.Parse("").CreateTableClient();

        private static readonly Guid _testPartitionKey = Guid.NewGuid();

        [TestMethod]
        public async Task InsertAsync_Test()
        {
            var entity = new MessageEntity { PartitionKey = _testPartitionKey.ToString(), RowKey = "demo", Message = "Integration test" };

            var tableResult = await Client.InsertAsync<MessageEntity>(entity);

            Assert.IsTrue(tableResult.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task GetAsync_Test()
        {
            var entity = new MessageEntity { PartitionKey = _testPartitionKey.ToString(), RowKey = "demo" };

            var tableResult = await Client.GetAsync<MessageEntity>(entity);

            Assert.IsTrue(tableResult.IsSuccessStatusCode && tableResult.Result != null);
        }

        [TestMethod]
        public async Task GetAsync_With_Select_Property_Test()
        {
            var entity = new MessageEntity { PartitionKey = _testPartitionKey.ToString(), RowKey = "demo" };

            const string selectProperties = "Message"; //use comma for multiple properties to return

            var tableResult = await Client.GetAsync<MessageEntity>(entity, selectProperties);

            var entityResult = tableResult.Result;

            Assert.IsTrue(tableResult.IsSuccessStatusCode && entityResult != null && entityResult.RowKey == null &&
                          entityResult.PartitionKey == null && !string.IsNullOrEmpty(entityResult.Message));
        }

        [TestMethod]
        public async Task GetAsync_Pagination_With_First_10_Entities_Test()
        {
            var token = default(TablePaginationToken);

            var entity = new MessageEntity();

            var entities = new List<MessageEntity>();

            var options = new TableQueryOptions { SelectProperties = "Message", Top = 10 }; //optional

            do
            {
                var result = await Client.QueryAsync<MessageEntity>(entity, token, options);

                entities.AddRange(result.Results);

                token = result.TablePaginationToken;

                if (result.Results.Count <= 10) break;

            } while (token != null);

            Assert.IsTrue(entities.Count <= 10);
        }

        [TestMethod]
        public async Task GetAsync_Pagination_All_Test()
        {
            var token = default(TablePaginationToken);

            var entity = new MessageEntity();

            var entities = new List<MessageEntity>();

            var options = new TableQueryOptions { SelectProperties = "Message", Top = 10 }; //optional

            do
            {
                var result = await Client.QueryAsync<MessageEntity>(entity, token, options);

                entities.AddRange(result.Results);

                token = result.TablePaginationToken;

            } while (token != null);

            Assert.IsTrue(entities.Count > 0);
        }

        [TestMethod]
        public async Task DeleteAsync_Test()
        {
            var entity = new MessageEntity { PartitionKey = Guid.NewGuid().ToString(), RowKey = "demo", Message = "Delete test" };

            await Client.InsertAsync<MessageEntity>(entity); //insert first

            var tableResult = await Client.DeleteAsync<MessageEntity>(entity);

            Assert.IsTrue(tableResult.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task UpdateAsync_Test()
        {
            var entity = new MessageEntity { PartitionKey = _testPartitionKey.ToString(), RowKey = "demo", Message = "Update integeration test" };

            var tableResult = await Client.UpdateAsync<MessageEntity>(entity);

            Assert.IsTrue(tableResult.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task FilterCondition_Single_Property_Test()
        {
            var token = default(TablePaginationToken);
            var entity = new UserEntity();
            var entities = new List<MessageEntity>();
            var options = new TableQueryOptions
            {
                Filters = new[]
                {
                    new FilterCondition
                    {
                        Conditions = new[]
                        {
                            new CompareCondition("Email", CompareOperator.Equal, "Ben@contoso.com"),
                        }
                    }
                }
            };

            do
            {
                var result = await Client.QueryAsync<MessageEntity>(entity, token, options);

                entities.AddRange(result.Results);

                token = result.TablePaginationToken;

            } while (token != null);

            Assert.IsTrue(entities.Count > 0);
        }

        [TestMethod]
        public async Task FilterCondition_Two_Property_Test()
        {
            var token = default(TablePaginationToken);
            var entity = new UserEntity();
            var entities = new List<MessageEntity>();
            var options = new TableQueryOptions
            {
                Filters = new[]
                {
                    new FilterCondition(FilterOperator.And)
                    {
                        Conditions = new[]
                        {
                            new CompareCondition("PartitionKey", CompareOperator.Equal, "Smith"),
                            new CompareCondition("Email", CompareOperator.Equal, "Ben@contoso.com"),
                        }
                    }
                }
            };

            do
            {
                var result = await Client.QueryAsync<MessageEntity>(entity, token, options);

                entities.AddRange(result.Results);

                token = result.TablePaginationToken;

            } while (token != null);

            Assert.IsTrue(entities.Count > 0);
        }

        [TestMethod]
        public async Task FilterCondition_Complex_Test()
        {
            var token = default(TablePaginationToken);
            var entity = new UserEntity();
            var entities = new List<MessageEntity>();
            var options = new TableQueryOptions
            {
                Filters = new[]
                {
                    new FilterCondition(FilterOperator.And)
                    {
                        Conditions = new[]
                        {
                            new CompareCondition("PartitionKey", CompareOperator.Equal, "Smith"),
                            new CompareCondition("Email", CompareOperator.Equal, "Ben@contoso.com"),
                        }
                    },

                    new FilterCondition(FilterOperator.Or),

                    new FilterCondition
                    {
                        Conditions = new[]
                        {
                            new CompareCondition("phone", CompareOperator.Equal, "425-555-0102"),
                        }
                    }
                }
            };

            do
            {
                var result = await Client.QueryAsync<MessageEntity>(entity, token, options);

                entities.AddRange(result.Results);

                token = result.TablePaginationToken;

            } while (token != null);

            Assert.IsTrue(entities.Count > 0);
        }
    }
}