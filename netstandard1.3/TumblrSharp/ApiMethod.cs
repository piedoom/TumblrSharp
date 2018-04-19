using System;
using System.Net.Http;
using DontPanic.TumblrSharp.OAuth;

namespace DontPanic.TumblrSharp
{
	/// <summary>
	/// Encapsulates all the data required to make a Tumblr API call.
	/// </summary>
	public class ApiMethod
	{
		private readonly string methodUrl;
		private readonly HttpMethod httpMethod;
		private readonly Token oAuthToken;
		private readonly MethodParameterSet parameters;

		/// <summary>
		/// Initializes a new instance of the <see cref="ApiMethod"/> class.
		/// </summary>
		/// <param name="methodUrl">
		/// The url of the method to call.
		/// </param>
		/// <param name="oAuthToken">
		/// The OAuth <see cref="Token"/> to use for the call. Can be <b>null</b> if the 
		/// method does not require OAuth.
		/// </param>
		/// <param name="httpMethod">
		/// The required <see cref="HttpMethod"/> for the Tumblr API call. Only GET and
		/// POST are supported.
		/// </param>
		/// <param name="parameters">
		/// The parameters for the Tumblr API call. Can be <b>null</b> if the method does not require parameters.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="methodUrl"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="methodUrl"/> is empty.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="httpMethod"/> is not Get or Post.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		public ApiMethod(
			string methodUrl,
			Token oAuthToken,
			HttpMethod httpMethod, 
			MethodParameterSet parameters = null)
		{
			if (methodUrl == null)
				throw new ArgumentNullException("methodUrl");

			if (methodUrl.Length == 0)
				throw new ArgumentException("Method URL cannot be empty.", "methodUrl");

			if (httpMethod != HttpMethod.Get && httpMethod != HttpMethod.Post)
				throw new ArgumentException("The http method must be either GET or POST.", "httpMethod");

			this.methodUrl = methodUrl;
			this.httpMethod = httpMethod;
			this.oAuthToken = oAuthToken;
			this.parameters = parameters ?? new MethodParameterSet();
		}

		/// <summary>
		/// Gets the url of the Tumblr API method to call.
		/// </summary>
		public string Url { get { return methodUrl; } }

		/// <summary>
		/// Gets the OAuth <see cref="Token"/> to use for the call.
		/// </summary>
		public Token OAuthToken { get { return oAuthToken; } }

		/// <summary>
		/// Gets the required <see cref="HttpMethod"/> for the Tumblr API call.
		/// </summary>
		public HttpMethod HttpMethod { get { return httpMethod; } }

		/// <summary>
		/// Gets the parameters for the Tumblr API call.
		/// </summary>
		public MethodParameterSet Parameters { get { return parameters; } }
	}
}
