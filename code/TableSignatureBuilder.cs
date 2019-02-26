using System;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace Azure.TableStorage
{
    internal static class TableSignatureBuilder
    {
        internal static AuthenticationHeaderValue Create(TableCredentials credentials, string message)
        {
            using (HashAlgorithm hmac = new HMACSHA256(credentials.HmacKey))
            {
                return new AuthenticationHeaderValue("SharedKeyLite", credentials.AccountName + ":" + Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(message))));
            }
        }
    }
}
