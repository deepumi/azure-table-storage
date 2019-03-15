using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azure.TableStorage.Test
{
    internal static class MessageRepository
    {
        private static readonly TableClient _client = TableStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=tapappstorageproto;AccountKey=+YLvUABaBvM1aLaJr0rOjTUjTlgrKEBBJmTl3+ofISrM91vWsjnAoESz95R/U//CN2UrVfcFxBK+2jGQc7YncQ==;EndpointSuffix=core.windows.net").CreateTableClient();

        internal async Task GetAll()
        {
            TablePaginationToken token;

        }
    }
}
