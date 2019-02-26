using System;

namespace Azure.TableStorage
{
    internal sealed class TableStorageUri
    {
        private readonly Uri _primmaryUri;

        internal TableStorageUri(string accountName, string endPointSuffix)
        {
            _primmaryUri = new Uri("https://" + accountName + ".table." + endPointSuffix + "/");
        }

        internal Uri BuildRequestUri(TableUri uri) => new Uri(_primmaryUri + uri.Uri + uri.Filter);
    }
}
