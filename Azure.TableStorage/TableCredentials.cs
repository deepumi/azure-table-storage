using System;
using System.Net.Http.Headers;

namespace Azure.TableStorage
{
    internal sealed class TableCredentials
    {
        private readonly string _accountName;

        private readonly byte[] _key;

        internal TableCredentials(TableConnectionEntry entry)
        {
            _accountName = entry.AccountName;
            _key = Convert.FromBase64String(entry.AccountKey);
        }

        internal AuthenticationHeaderValue AuthorizationHeader(string timeString, string tableUri)
        {
            return TableSignatureBuilder.Create(_key, _accountName, timeString + "\n" + "/" + _accountName + "/" + tableUri);
        }
    }
}