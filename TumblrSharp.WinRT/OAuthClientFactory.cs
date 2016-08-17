using DontPanic.TumblrSharp.OAuth;

namespace DontPanic.TumblrSharp
{
    /// <summary>
    /// Factory for <see cref="OAuthClient"/> instances.
    /// </summary>
    public class OAuthClientFactory : IOAuthClientFactory
    {
        /// <summary>
        /// Creates a new <see cref="OAuthClient"/> instance.
        /// </summary>
        /// <param name="consumerKey">
        /// The consumer key.
        /// </param>
        /// <param name="consumerSecret">
        /// The consumer secret.
        /// </param>
        /// <returns>
        /// A <see cref="OAuthClient"/> instance.
        /// </returns>
        public OAuthClient Create(string consumerKey, string consumerSecret)
        {
            return new OAuthClient(new HmacSha1HashProvider(), consumerKey, consumerSecret);
        }
    }
}
