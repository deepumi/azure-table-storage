using System;

namespace Azure.TableStorage
{
    public sealed class TableStorageAccount
    {
        private readonly TableCredentials _tableCredentials;

        private readonly TableStorageUri _tableStorageUri;

        internal TableStorageAccount(TableCredentials credentials, TableStorageUri tableStorageUri)
        {
            _tableCredentials = credentials;

            _tableStorageUri = tableStorageUri;
        }

        public static TableStorageAccount Parse(string connectionString)
        {
            if(string.IsNullOrEmpty(connectionString)) ThrowHelper.Throw("Connection string is missing");

            var entry = ParseConnectionString(connectionString);

            return new TableStorageAccount(new TableCredentials(entry), new TableStorageUri(entry.AccountName, entry.EndpointSuffix));
        }

        public TableClient CreateTableClient() => new TableClient(_tableCredentials, _tableStorageUri);

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