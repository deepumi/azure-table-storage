using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Azure.TableStorage
{
    internal static class TableUriQueryBuilder
    {
        internal static string Build(TablePaginationToken token, TableQueryOptions options)
        {
            if (token == null && options == null) return null;

            var items = new List<string>(5);

            if (token != null)
            {
                items.Add("NextPartitionKey=" + Uri.EscapeDataString(token.NextPartitionKey));
                items.Add("NextRowKey=" + Uri.EscapeDataString(token.NextRowKey));
            }

            if (options != null)
            {
                if (options.Top > 0) items.Add("$top=" + options.Top.ToString(CultureInfo.InvariantCulture));

                if (!string.IsNullOrEmpty(options.SelectProperties)) items.Add("$select=" + options.SelectProperties);

                if (!string.IsNullOrEmpty(options.Filter)) items.Add("$filter=" + options.Filter);
            }

            var count = items.Count;

            if (count == 0) return null;

            var builder = new StringBuilder("?");

            for (var i = 0; i < count; i++)
            {
                builder.Append(items[i]);

                if (i != count - 1) builder.Append("&");
            }

            return builder.ToString();
        }
    }
}
