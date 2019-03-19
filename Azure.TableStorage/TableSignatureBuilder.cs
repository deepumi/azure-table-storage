using System;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace Azure.TableStorage
{
    internal static class TableSignatureBuilder
    {
        internal static AuthenticationHeaderValue Create(byte[] key, string accountName, string message)
        {
            using (HashAlgorithm hmac = new HMACSHA256(key))
            {
                return new AuthenticationHeaderValue("SharedKeyLite", accountName + ":" + Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(message))));
            }
        }
    }
}