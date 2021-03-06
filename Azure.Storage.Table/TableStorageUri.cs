﻿using System;

namespace Azure.Storage.Table
{
    internal sealed class TableStorageUri
    {
        private readonly Uri _primmaryUri;

        internal TableStorageUri(string accountName, string endPointSuffix)
        {
            _primmaryUri = new Uri("https://" + accountName + ".table." + endPointSuffix + "/");
        }

        internal Uri BuildRequestUri(TableUri uri)
        {
            if (uri.Filter == null) return new Uri(_primmaryUri + uri.Url);

            return new Uri(_primmaryUri + uri.Url + uri.Filter);
        }
    }
}