using DontPanic.TumblrSharp.OAuth;

namespace DontPanic.TumblrSharp
{
    /// <summary>
    /// Generic factory for <see cref="TumblrClientBase"/> instances.
    /// </summary>
    public interface ITumblrClientFactory
    {
        /// <summary>
        /// Creates a new Tumblr client instance of type <typeparamref name="TClient"/>.
        /// </summary>
        /// <typeparam name="TClient">
        /// The type of client to create (must derive from <see cref="TumblrClientBase"/>).
        /// </typeparam>
        /// <param name="consumerKey">
        /// The consumer key.
        /// </param>
        /// <param name="consumerSecret">
        /// The consumer secret.
        /// </param>
        /// <param name="oAuthToken">
        /// An optional access token for the API. If no access token is provided, only the methods
        /// that do not require OAuth can be invoked successfully.
        /// </param>
        /// <returns>
        /// A new Tumblr client instance of type <typeparamref name="TClient"/>.
        /// </returns>
        TClient Create<TClient>(string consumerKey, string consumerSecret, Token oAuthToken = null) where TClient : TumblrClientBase;
    }
}
