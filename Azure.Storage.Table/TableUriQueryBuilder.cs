using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Azure.Storage.Table
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

            if (options.Filters?.Length> 0)
            {
                result = Result(result);
                result += "$filter=" + BuildFilterCondition(options.Filters);  
            }

            string Result(string s)
            {
                if (s.IndexOf('?') == -1)
                    s += Quote;
                else if (s == "?")
                    return s;
                else
                    s += "&";
                return s;
            }

            return result;
        }

        private static string BuildFilterCondition(IReadOnlyList<FilterCondition> filters)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < filters.Count; i++)
            {
                if (filters[i].Conditions == null)
                {
                    sb.Append(filters[i].FilterOperator.Operator);
                    continue;
                }

                if (filters.Count > 1) sb.Append("(");

                for (var j = 0; j < filters[i].Conditions.Length; j++)
                {
                    sb.Append(filters[i].Conditions[j].FilterString);

                    if (j != filters[i].Conditions.Length - 1)
                    {
                        sb.Append(filters[i].FilterOperator.Operator);
                    }
                }

                if (filters.Count > 1) sb.Append(")");
            }

            return sb.ToString();
        }
    }
}