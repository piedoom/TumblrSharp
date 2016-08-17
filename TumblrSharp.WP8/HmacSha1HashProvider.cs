using DontPanic.TumblrSharp;
using System;
using System.Text;

namespace DontPanic.TumblrSharp
{
    /// <summary>
    /// Provides an implementation of <see cref="IHmacSha1HashProvider"/> for signing OAuth requests.
    /// </summary>
    public class HmacSha1HashProvider : IHmacSha1HashProvider
    {
        /// <summary>
        /// Gets a HMAC-SHA1 hash for an OAuth request.
        /// </summary>
        /// <param name="consumerSecret">
        /// The consumer secret.
        /// </param>
        /// <param name="oauthSecret">
        /// The OAuth secret.
        /// </param>
        /// <param name="signatureBaseString">
        /// The signature base string for which to compute the hash.
        /// </param>
        /// <returns>
        /// A HMAC-SHA1 hash of <paramref name="signatureBaseString"/>.
        /// </returns>
        public string ComputeHash(string consumerSecret, string oauthSecret, string signatureBaseString)
        {
            if (signatureBaseString == null)
                return null;

            byte[] key = Encoding.UTF8.GetBytes(String.Format("{0}&{1}", consumerSecret, oauthSecret));
            using (System.Security.Cryptography.HMACSHA1 sha1 = new System.Security.Cryptography.HMACSHA1(key))
            {
                byte[] signatureBaseStringHash = sha1.ComputeHash(Encoding.UTF8.GetBytes(signatureBaseString));
                return Convert.ToBase64String(signatureBaseStringHash);
            }
        }
    }
}
