using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using System;
using System.Net.Http;

#if (NETCOREAPP2_2)
using Microsoft.Extensions.DependencyInjection;
#endif

namespace DontPanic.TumblrSharp
{
    /// <summary>
    /// Factory for <see cref="TumblrClientBase"/> instances.
    /// </summary>
    public class TumblrClientFactory : ITumblrClientFactory
    {

#if (NETCOREAPP2_2)
        internal static string TumblrSharpClientName
        {
            get
            {
                return "tumblrSharpHtppClient";
            }
        }
        
        public static void ConfigureService(IServiceCollection services, string consumerKey, string consumerSecret, Token oAuthToken = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (consumerKey == null)
                throw new ArgumentNullException(nameof(consumerKey));

            if (consumerKey.Length == 0)
                throw new ArgumentException("Consumer key cannot be empty.", nameof(consumerKey));

            if (consumerSecret == null)
                throw new ArgumentNullException("consumerSecret");

            if (consumerSecret.Length == 0)
                throw new ArgumentException("Consumer secret cannot be empty.", nameof(consumerSecret));

            var service = services.AddHttpClient(TumblrSharpClientName);
            
            service.ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new OAuthMessageHandler(new HmacSha1HashProvider(), consumerKey, consumerSecret, oAuthToken);
            }
            );
        }


        public TClient Create<TClient>(IHttpClientFactory httpClientFactory, string consumerKey, string consumerSecret, Token oAuthToken = null) where TClient : TumblrClientBase
        {
            if (typeof(TClient) == typeof(TumblrClient))
            {
                return new TumblrClient(httpClientFactory, TumblrSharpClientName, consumerKey, consumerSecret, oAuthToken) as TClient;
            }

            throw new ArgumentException(String.Format("The provided type '{0}'cannot be created by this factory.", typeof(TClient).FullName));
        }
#endif

        /// <summary>
        /// Creates a new Tumblr client instance of type <typeparamref name="TClient"/>.
        /// </summary>
        /// <remarks>
        /// This factory only supports <see cref="TumblrClientBase"/> or <see cref="TumblrClient"/> as values
        /// for <typeparamref name="TClient"/>.
        /// </remarks>
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
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TClient"/> is not <see cref="TumblrClientBase"/> or <see cref="TumblrClient"/>.
        /// </exception>
        public TClient Create<TClient>(string consumerKey, string consumerSecret, Token oAuthToken = null) where TClient : TumblrClientBase
        {
            if (typeof(TClient) == typeof(TumblrClientBase))
            {
                return new TumblrClientBase(new HmacSha1HashProvider(), consumerKey, consumerSecret, oAuthToken) as TClient;
            }
            else if (typeof(TClient) == typeof(TumblrClient))
            {
                return new TumblrClient(new HmacSha1HashProvider(), consumerKey, consumerSecret, oAuthToken) as TClient;
            }

            throw new ArgumentException(String.Format("The provided type '{0}'cannot be created by this factory.", typeof(TClient).FullName));
        }
    }
}
