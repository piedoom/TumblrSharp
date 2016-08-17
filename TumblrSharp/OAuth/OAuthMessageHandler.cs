using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DontPanic.TumblrSharp.OAuth
{
	internal class OAuthMessageHandler : DelegatingHandler
	{
        private readonly IHmacSha1HashProvider hashProvider;
		private readonly string consumerKey;
		private readonly string consumerSecret;
		private readonly Token oAuthToken;

		public OAuthMessageHandler(IHmacSha1HashProvider hashProvider, string consumerKey, string consumerSecret, Token oAuthToken)
            : this(new HttpClientHandler(), hashProvider, consumerKey, consumerSecret, oAuthToken)
		{ }

		public OAuthMessageHandler(HttpMessageHandler innerHandler, IHmacSha1HashProvider hashProvider, string consumerKey, string consumerSecret, Token oAuthToken)
			: base(innerHandler)
		{
            if (hashProvider == null)
                throw new ArgumentNullException("hashProvider");

			if (consumerKey == null)
				throw new ArgumentNullException("consumerKey");

			if (consumerKey.Length == 0)
				throw new ArgumentException("Consumer key cannot be empty.", "consumerKey");

			if (consumerSecret == null)
				throw new ArgumentNullException("consumerSecret");

			if (consumerSecret.Length == 0)
				throw new ArgumentException("Consumer secret cannot be empty.", "consumerSecret");

			if(oAuthToken != null && !oAuthToken.IsValid)
				throw new ArgumentException("OAuth token is not valid.", "oAuthToken");

            if (consumerKey == null)
                throw new ArgumentNullException("consumerKey");

            this.hashProvider = hashProvider;
			this.consumerKey = consumerKey;
			this.consumerSecret = consumerSecret;
			this.oAuthToken = oAuthToken;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			MethodParameterSet requestParameters = null;
			if (request.Content is FormUrlEncodedContent)
			{
				var formUrlEncoded = request.Content as FormUrlEncodedContent;
				string content = await formUrlEncoded.ReadAsStringAsync().ConfigureAwait(false);

				requestParameters = new MethodParameterSet(content);
			}
			else if (request.Content is MultipartFormDataContent)
			{
				requestParameters = new MethodParameterSet();

				var multiPart = request.Content as MultipartFormDataContent;
				foreach (var c in ((MultipartFormDataContent)request.Content))
				{
					var stringContent = c as StringContent;
					if (stringContent != null)
						requestParameters.Add(c.Headers.ContentDisposition.Name, await c.ReadAsStringAsync().ConfigureAwait(false));
				}
			}

			//if we have an api_key parameter we can skip the oauth
			if (requestParameters.FirstOrDefault(c => c.Name == "api_key") == null)
			{
				var authorizationHeaderParameters = new MethodParameterSet(requestParameters);

				if (oAuthToken != null) authorizationHeaderParameters.Add("oauth_token", oAuthToken.Key);
				authorizationHeaderParameters.Add("oauth_consumer_key", consumerKey);
				authorizationHeaderParameters.Add("oauth_nonce", Guid.NewGuid().ToString());
				authorizationHeaderParameters.Add("oauth_timestamp", DateTimeHelper.ToTimestamp(DateTime.UtcNow).ToString());
				authorizationHeaderParameters.Add("oauth_signature_method", "HMAC-SHA1");
				authorizationHeaderParameters.Add("oauth_version", "1.0");

				string urlParameters = authorizationHeaderParameters.ToFormUrlEncoded();

				var requestUriNoQueryString = request.RequestUri.OriginalString;
				if (!String.IsNullOrEmpty(request.RequestUri.Query))
					requestUriNoQueryString = request.RequestUri.OriginalString.Replace(request.RequestUri.Query, String.Empty);

				string signatureBaseString = String.Format("{0}&{1}&{2}", request.Method.ToString(), UrlEncoder.Encode(requestUriNoQueryString), UrlEncoder.Encode(urlParameters));
				string signatureHash = hashProvider.ComputeHash(consumerSecret, (oAuthToken != null) ? oAuthToken.Secret : null, signatureBaseString);

				authorizationHeaderParameters.Add("oauth_signature", signatureHash);

				foreach (IMethodParameter p in requestParameters)
				{
					//remove non-oauth parameters from the authorization header
					if (!p.Name.StartsWith("oauth"))
						authorizationHeaderParameters.Remove(p);
				}

				request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", authorizationHeaderParameters.ToAuthorizationHeader());
			}

			if (request.Method == HttpMethod.Get)
				request.Content = null; //we don't have to send a body with get requests

			return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
		}
	}
}
