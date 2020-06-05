
namespace DontPanic.TumblrSharp.OAuth
{
    /// <summary>
    /// Factory for <see cref="OAuthClient"/> instances.
    /// </summary>
    public interface IOAuthClientFactory
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
        OAuthClient Create(string consumerKey, string consumerSecret);
    }
}
