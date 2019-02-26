using System;
using System.Collections.Generic;

namespace Azure.TableStorage
{
    internal sealed class TableCredentials
    {
        internal byte[] HmacKey { get; }

        internal string AccountName { get; }

        internal string AccountKey { get; }

        internal TableCredentials(IDictionary<string, string> settings)
        {
            AccountName = settings[Constants.AccountName];
            AccountKey = settings[Constants.AccountKey];
            HmacKey = Convert.FromBase64String(settings[Constants.AccountKey]);
        }
    }
}
