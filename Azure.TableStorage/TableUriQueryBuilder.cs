using System;
using System.Globalization;

namespace Azure.TableStorage
{
    internal static class TableUriQueryBuilder
    {
        private const string Quote = "?";

        internal static string Build(TablePaginationToken token, TableQueryOptions options)
        {
            if (token == null && options == null) return null;

            var result = Quote;

            if (token != null)
            {
                result += "NextPartitionKey=" + Uri.EscapeDataString(token.NextPartitionKey) + "&NextRowKey=" + Uri.EscapeDataString(token.NextRowKey);
            }

            if (options == null) return result;

            if (options.Top > 0)
            {
                result = Result(result);
                result += "$top=" + options.Top.ToString(CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrEmpty(options.SelectProperties))
            {
                result = Result(result);
                result += "$select=" + options.SelectProperties;
            }

            if (!string.IsNullOrEmpty(options.Filter))
            {
                result = Result(result);
                result +="$filter=" + options.Filter;
            }

            string Result(string s)
            {
                if (s.IndexOf('?') == -1)
                    s += Quote;
                else
                    s += "&";
                return s;
            }

            return result;
        }
    }
}