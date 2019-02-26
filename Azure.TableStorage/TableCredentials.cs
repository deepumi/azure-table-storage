﻿using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Azure.TableStorage
{
    internal sealed class TableCredentials
    {
        private readonly string _accountName;

        private readonly byte[] _key;

        internal TableCredentials(Dictionary<string, string> settings)
        {
            _accountName = settings[Constants.AccountName];
            _key = Convert.FromBase64String(settings[Constants.AccountKey]);
        }

        internal AuthenticationHeaderValue AuthorizationHeader(string timeString, string tableUri)
        {
            return TableSignatureBuilder.Create(_key, _accountName, timeString + "\n" + "/" + _accountName + "/" + tableUri);
        }
    }
}
