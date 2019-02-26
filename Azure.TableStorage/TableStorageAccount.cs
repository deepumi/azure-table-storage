namespace Azure.TableStorage
{
    using System;
    using Http;

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
            var entry = ParseConnectionString(connectionString);

            return new TableStorageAccount(new TableCredentials(entry), new TableStorageUri(entry.AccountName, entry.EndpointSuffix), new HttpClientFactory());
        }

        internal TableClient CreateTableClient() => new TableClient(_tableCredentials, _tableStorageUri, _http);

        private static TableConnectionEntry ParseConnectionString(string connectionString)
        {
            var split = connectionString.Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            var entry = new TableConnectionEntry();

            for (var i = 0; i < split.Length; i++)
            {
                var nvp = split[i].Split(new char[1] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);

                if (nvp.Length == 0) continue;

                switch (nvp[0])
                {
                    case nameof(entry.AccountKey):
                        entry.AccountKey = nvp[1];
                        break;

                    case nameof(entry.AccountName):
                        entry.AccountName = nvp[1];
                        break;

                    case nameof(entry.EndpointSuffix):
                        entry.EndpointSuffix = nvp[1];
                        break;
                }
            }

            return entry;
        }
    }
}
