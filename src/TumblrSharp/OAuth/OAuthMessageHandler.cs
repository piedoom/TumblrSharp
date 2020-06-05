using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DontPanic.TumblrSharp.OAuth
{
	/// <summary>
	/// MessageHandler for the HttpClient
	/// </summary>
	public class OAuthMessageHandler : DelegatingHandler
	{
		private readonly IHmacSha1HashProvider hashProvider;
		private readonly string consumerKey;
		private readonly string consumerSecret;
		private readonly Token oAuthToken;

		/// <summary>
		/// Create OAuthMessageHandler
		/// </summary>
		/// <param name="hashProvider"></param>
		/// <param name="consumerKey"></param>
		/// <param name="consumerSecret"></param>
		/// <param name="oAuthToken"></param>
		public OAuthMessageHandler(IHmacSha1HashProvider hashProvider, string consumerKey, string consumerSecret, Token oAuthToken)
			: this(new HttpClientHandler(), hashProvider, consumerKey, consumerSecret, oAuthToken)
		{ }

		/// <summary>
		/// Create OAuthMessageHandle
		/// </summary>
		/// <param name="innerHandler"></param>
		/// <param name="hashProvider"></param>
		/// <param name="consumerKey"></param>
		/// <param name="consumerSecret"></param>
		/// <param name="oAuthToken"></param>
		public OAuthMessageHandler(HttpMessageHandler innerHandler, IHmacSha1HashProvider hashProvider, string consumerKey, string consumerSecret, Token oAuthToken)
			: base(innerHandler)
		{
			if (hashProvider == null)
				throw new ArgumentNullException(nameof(hashProvider));

			if (consumerKey == null)
				throw new ArgumentNullException(nameof(consumerKey));

			if (consumerKey.Length == 0)
				throw new ArgumentException("Consumer key cannot be empty.", nameof(consumerKey));

			if (consumerSecret == null)
				throw new ArgumentNullException(nameof(consumerSecret));

			if (consumerSecret.Length == 0)
				throw new ArgumentException("Consumer secret cannot be empty.", nameof(consumerSecret));

			if (oAuthToken != null && !oAuthToken.IsValid)
				throw new ArgumentException("OAuth token is not valid.", nameof(oAuthToken));

			if (consumerKey == null)
				throw new ArgumentNullException(nameof(consumerKey));

			this.hashProvider = hashProvider;
			this.consumerKey = consumerKey;
			this.consumerSecret = consumerSecret;
			this.oAuthToken = oAuthToken;
		}

		/// <summary>
		/// Send methode 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			await request.PreparationForTumblrClient(hashProvider, consumerKey, consumerSecret, oAuthToken).ConfigureAwait(false);

			return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
		}
	}
}
