using DontPanic.TumblrSharp.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DontPanic.TumblrSharp
{
	/// <summary>
	/// Extensionsclass for HttpRequestMessage
	/// </summary>
	public static class ExtensionHttpRequestMessage
	{
		/// <summary>
		/// set the authoriaztionheader for TumblrClient
		/// </summary>
		/// <param name="request"></param>
		/// <param name="hashProvider"></param>
		/// <param name="consumerKey"></param>
		/// <param name="consumerSecret"></param>
		/// <param name="oAuthToken"></param>
		/// <returns></returns>
		public static async Task PreparationForTumblrClient(this HttpRequestMessage request, IHmacSha1HashProvider hashProvider, string consumerKey, string consumerSecret, Token oAuthToken)
		{
			MethodParameterSet requestParameters = null;

			if (request.Content is FormUrlEncodedContent)
			{
				var formUrlEncoded = request.Content as FormUrlEncodedContent;

				string content = await formUrlEncoded.ReadAsStringAsync().ConfigureAwait(false);

				requestParameters = new MethodParameterSet(content);
			}
			else if (request.Content is MultipartFormDataContent multiPartContent)
			{
				requestParameters = new MethodParameterSet();

				foreach (var c in multiPartContent)
				{
					if (c is StringContent stringContent)
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
				string signatureHash = hashProvider.ComputeHash(consumerSecret, oAuthToken?.Secret, signatureBaseString);

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
		}
	}
}
