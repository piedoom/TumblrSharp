using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DontPanic.TumblrSharp.OAuth
{
	/// <summary>
	/// A client for OAuth. 
	/// </summary>
	/// <remarks>
	/// This class can be used to authorize the application to access the user's account. 
	/// If the user authorizes the app, an access token will be generated that can then be 
	/// used to make OAuth method calls.
	/// </remarks>
	public class OAuthClient
	{
		#region Response Parsers

		private static System.Text.RegularExpressions.Regex xauthTokensRegEx = new System.Text.RegularExpressions.Regex(
						@"oauth_token=(?<token>[^&]*)&oauth_token_secret=(?<secret>.*)");

		private static System.Text.RegularExpressions.Regex oauthTempTokensRegEx = new System.Text.RegularExpressions.Regex(
						@"oauth_token=(?<token>[^&]*)&oauth_token_secret=(?<secret>[^&]*)&oauth_callback_confirmed=(?<confirmed>.*)");

		private static System.Text.RegularExpressions.Regex oauthVerifierRegEx = new System.Text.RegularExpressions.Regex(
						@"oauth_token=(?<token>[^&]*)&oauth_verifier=(?<verifier>.*)");

		#endregion 

		private const string accessTokenUrl = "https://www.tumblr.com/oauth/access_token";
		private const string requestTokenUrl = "https://www.tumblr.com/oauth/request_token";
		private const string authorizeTokenUrl = "https://www.tumblr.com/oauth/authorize";

        private readonly IHmacSha1HashProvider hashProvider;
		private readonly string consumerKey;
		private readonly string consumerSecret;

		/// <summary>
		/// Initializes a new instance of the <see cref="OAuth"/> client class.
		/// </summary>
        /// <param name="hashProvider">
        /// A <see cref="IHmacSha1HashProvider"/> implementation used to generate a
        /// HMAC-SHA1 hash for OAuth purposes.
        /// </param>
		/// <param name="consumerKey">
		/// The consumer key.
		/// </param>
		/// <param name="consumerSecret">
		/// The consumer secret.
		/// </param>
		/// <remarks>
		/// You can get a consumer key and a consumer secret by registering an application with Tumblr:<br/>
		/// <br/>
		/// http://www.tumblr.com/oauth/apps
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		/// <list type="bullet">
        /// <item>
        ///		<description>
        ///			<paramref name="hashProvider"/> is <b>null</b>.
        ///		</description>
        ///	</item>
		/// <item>
		///		<description>
		///			<paramref name="consumerKey"/> is <b>null</b>.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="consumerSecret"/> is <b>null</b>.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="consumerKey"/> is empty.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="consumerSecret"/> is empty.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		public OAuthClient(IHmacSha1HashProvider hashProvider, string consumerKey, string consumerSecret)
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

            this.hashProvider = hashProvider;
			this.consumerKey = consumerKey;
			this.consumerSecret = consumerSecret;
		}

		/// <summary>
		/// Asynchronously performs XAuth.
		/// </summary>
		/// <param name="userName">
		/// The user name.
		/// </param>
		/// <param name="password">
		/// The user password.
		/// </param>
		/// <returns>
		/// The access <see cref="Token"/>.
		/// </returns>
		/// <remarks>
		/// XAuth is mainly used in mobile applications, where the device does not (or can not) have a 
		/// callback url. It uses the user name and password to get the access token from the server.
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="userName"/> is <b>null</b>.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="password"/> is <b>null</b>.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="userName"/> is empty.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="password"/> is empty.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		/// <exception cref="OAuthException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			Could not determine oauth_token and oauth_token_secret from server response.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			An exception occurred during the method call.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		public async Task<Token> PerformXAuthAsync(string userName, string password)
		{
			if (userName == null)
				throw new ArgumentNullException("userName");

			if (userName.Length == 0)
				throw new ArgumentException("User name cannot be empty.", "userName");

			if (password == null)
				throw new ArgumentNullException("password");

			if (password.Length == 0)
				throw new ArgumentException("Password cannot be empty.", "password");

			var requestParameters = new Dictionary<string, string>() 
			{
				 { "x_auth_mode", "client_auth" },
				 { "x_auth_password", password },
				 { "x_auth_username", userName }
			};

			using (var client = new HttpClient(new OAuthMessageHandler(hashProvider, consumerKey, consumerSecret, null)))
			{
				var request = new HttpRequestMessage(HttpMethod.Post, accessTokenUrl);
				request.Content = new FormUrlEncodedContent(requestParameters);

				var response = await client.SendAsync(request).ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{
					string tokenString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

					System.Text.RegularExpressions.Match m = xauthTokensRegEx.Match(tokenString);
					if (m.Success)
					{
						return new Token(m.Groups["token"].Value, m.Groups["secret"].Value);
					}
					else
					{
						throw new OAuthException("Could not determine oauth_token and oauth_token_secret from server response.");
					}
				}
				else
				{
					throw new OAuthException(String.Format("PerformXAuthAsync failed. Status Code: {0}, Message: {1}", (int)response.StatusCode, response.ReasonPhrase));
				}
			}
		}

		/// <summary>
		/// Asynchronously gets a request token.
		/// </summary>
		/// <param name="callbackUrl">
		/// The server redirects Users to this URL after they authorize access to their private data.
		/// </param>
		/// <returns>
		/// The request token.
		/// </returns>
		/// <remarks>
		/// The Consumer obtains an unauthorized Request Token by asking the Service Provider to issue a Token. 
		/// The Request Token’s sole purpose is to receive User approval and can only be used to obtain an Access Token.
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="callbackUrl"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="callbackUrl"/> is empty.
		/// </exception>
		/// <exception cref="OAuthException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			Could not determine oauth_token and oauth_token_secret from server response.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			An exception occurred during the method call.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		public async Task<Token> GetRequestTokenAsync(string callbackUrl)
		{
			if (callbackUrl == null)
				throw new ArgumentNullException("callbackUrl");

			if (callbackUrl.Length == 0)
				throw new ArgumentException("Callback URL cannot be empty.", "callbackUrl");

			var requestParameters = new Dictionary<string, string>() 
			{
				 { "oauth_callback", callbackUrl }
			};

            using (var client = new HttpClient(new OAuthMessageHandler(hashProvider, consumerKey, consumerSecret, null)))
			{
				var request = new HttpRequestMessage(HttpMethod.Post, requestTokenUrl);
				request.Content = new FormUrlEncodedContent(requestParameters);

				var response = await client.SendAsync(request).ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{
					string tokenString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

					System.Text.RegularExpressions.Match m = oauthTempTokensRegEx.Match(tokenString);
					if (m.Success && String.Compare(m.Groups["confirmed"].Value, Boolean.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new Token(m.Groups["token"].Value, m.Groups["secret"].Value);
					}
					else
					{
						throw new OAuthException("Could not determine oauth_token and oauth_token_secret from server response.");
					}
				}
				else
				{
					throw new OAuthException(String.Format("GetRequestTokenAsync failed. Status Code: {0}, Message: {1}", response.StatusCode, response.ReasonPhrase));
				}
			}
		}

		/// <summary>
		/// Builds the url that is required to connect to the server, where the server will authenticate the user
		/// and ask for authorization.
		/// </summary>
		/// <param name="requestToken">
		/// The request token obtained during the call to <see cref="GetRequestTokenAsync"/>.
		/// </param>
		/// <returns>
		/// The <see cref="Uri"/> where to direct the user to obtain authorization.
		/// </returns>
		/// <remarks>
		/// After the User authenticates with the Service Provider and grants permission for Consumer access, the Consumer will be 
		/// notified that the Request Token has been authorized and ready to be exchanged for an Access Token. The Service Provider 
		/// will construct an HTTP GET request URL, and redirects the User’s web browser to that URL with the following parameters: 
		/// <b>oauth_token</b> which is the request token and <b>oauth_verifier</b> which is the verification code tied to the request token.
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="requestToken"/> is <b>null</b>.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="requestToken"/>.Key is <b>null</b>.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="requestToken"/>.Key is empty.
		/// </exception>
		public Uri GetAuthorizeUrl(Token requestToken)
		{
			if (requestToken == null)
				throw new ArgumentNullException("requestToken");

			if (requestToken.Key == null)
				throw new ArgumentNullException("requestToken.Key");

			if (requestToken.Key.Length == 0)
				throw new ArgumentException("Request token key cannot be empty.", "requestToken.Key");

			return new Uri(String.Format("{0}?oauth_token={1}", authorizeTokenUrl, requestToken.Key), UriKind.Absolute);
		}

		/// <summary>
		/// Gets the authorized access token that can be used to make OAuth calls.
		/// </summary>
		/// <param name="requestToken">
		/// The request token sent from the server to the <b>callback url</b>.
		/// </param>
		/// <param name="verifierUrl">
		/// The verifier url returned from the server.
		/// </param>
		/// <returns>
		/// The access token.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="requestToken"/> is <b>null</b>.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="verifierUrl"/> is <b>null</b>.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="verifierUrl"/> is empty.
		/// </exception>
		/// <exception cref="OAuthException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			Could not determine oauth_token and oauth_token_secret from server response.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			An exception occurred during the method call.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		public async Task<Token> GetAccessTokenAsync(Token requestToken, string verifierUrl)
		{
			if (requestToken == null)
				throw new ArgumentNullException("requestToken");

			if (verifierUrl == null)
				throw new ArgumentNullException("verifierUrl");

			if (verifierUrl.Length == 0)
				throw new ArgumentException("Verifier URL cannot be empty.", "verifierUrl");

			Uri uri = new Uri(verifierUrl, UriKind.RelativeOrAbsolute);
			string verifierString = (uri.IsAbsoluteUri) ? uri.Query : verifierUrl;

			System.Text.RegularExpressions.Match m = oauthVerifierRegEx.Match(verifierString);
			if (m.Success)
			{
				string token = m.Groups["token"].Value;
				string verifier = m.Groups["verifier"].Value;

				MethodParameterSet authorizationHeaderParameters = new MethodParameterSet();
				authorizationHeaderParameters.Add("oauth_token", token);
				authorizationHeaderParameters.Add("oauth_verifier", verifier);

				var requestParameters = new Dictionary<string, string>() 
				{
					{ "oauth_token", token },
					{ "oauth_verifier", verifier },
				};

                using (var client = new HttpClient(new OAuthMessageHandler(hashProvider, consumerKey, consumerSecret, requestToken)))
				{
					var request = new HttpRequestMessage(HttpMethod.Post, accessTokenUrl);
					request.Content = new FormUrlEncodedContent(requestParameters);

					using (var response = await client.SendAsync(request).ConfigureAwait(false))
					{
						if (response.IsSuccessStatusCode)
						{
							string tokenString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

							System.Text.RegularExpressions.Match m1 = xauthTokensRegEx.Match(tokenString);
							if (m1.Success)
							{
								return new Token(m1.Groups["token"].Value, m1.Groups["secret"].Value);
							}
							else
							{
								throw new OAuthException("Could not determine oauth_token and oauth_token_secret from server response.");
							}
						}
						else
						{
							throw new OAuthException(String.Format("GetAccessTokenAsync failed. Status Code: {0}, Message: {1}", response.StatusCode, response.ReasonPhrase));
						}
					}
				}
			}
			else
			{
				throw new OAuthException("Could not parse response to callback URL");
			}
		}
	}
}
