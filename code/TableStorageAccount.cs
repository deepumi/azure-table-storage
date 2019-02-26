using System;
using System.Collections.Generic;
using Azure.TableStorage.Http;

namespace Azure.TableStorage
{
    internal sealed class TableStorageAccount
    {
        private readonly TableCredentials _tableCredentials;

        private readonly TableStorageUri _tableStorageUri;

        private readonly HttpClientFactory _http;

        internal TableStorageAccount(TableCredentials credentials, TableStorageUri tableStorageUri, HttpClientFactory http)
        {
            _tableCredentials = credentials;

            _tableStorageUri = tableStorageUri;

            _http = http;
        }

        internal static TableStorageAccount Parse(string connectionString)
        {
            var settings = ParseInternal(connectionString);

            return new TableStorageAccount(new TableCredentials(settings), new TableStorageUri(settings[Constants.AccountName], settings[Constants.EndpointSuffix]), new HttpClientFactory());
        }

        internal TableClient CreateTableClient() => new TableClient(_tableCredentials, _tableStorageUri, _http);

        private static Dictionary<string, string> ParseInternal(string connectionString)
        {
            var split = connectionString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            var dictionary = new Dictionary<string, string>(split.Length, StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < split.Length; i++)
            {
                var nvp = split[i].Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);

                if (nvp.Length > 1 && !dictionary.ContainsKey(nvp[0])) dictionary.Add(nvp[0], nvp[1]);
            }

            return dictionary;
        }
    }
}
