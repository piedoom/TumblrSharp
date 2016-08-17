using DontPanic.TumblrSharp;
using System;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

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

			var sha1Provider = MacAlgorithmProvider.OpenAlgorithm(Windows.Security.Cryptography.Core.MacAlgorithmNames.HmacSha1);

			IBuffer keyMaterial = CryptographicBuffer.ConvertStringToBinary(
				String.Format("{0}&{1}", consumerSecret, oauthSecret),
				BinaryStringEncoding.Utf8);

			var key = sha1Provider.CreateKey(keyMaterial);
			IBuffer signatureBaseStringHash = CryptographicEngine.Sign(key, CryptographicBuffer.ConvertStringToBinary(signatureBaseString, BinaryStringEncoding.Utf8));
			return CryptographicBuffer.EncodeToBase64String(signatureBaseStringHash);
        }
    }
}
