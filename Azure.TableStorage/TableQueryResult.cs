using System.Collections.Generic;

namespace Azure.TableStorage
{
    public sealed class TableQueryResult<T>
    {
        public List<T> Results { get; }

        public TablePaginationToken TablePaginationToken { get; }
         
        public TableQueryResult(List<T> result, TablePaginationToken paginationToken)
        {
            Results = result;

            TablePaginationToken = paginationToken;
        }
    }
}