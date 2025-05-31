﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DontPanic.TumblrSharp.OAuth;

namespace DontPanic.TumblrSharp
{
	/// <summary>
	/// Encapsulates the Tumblr API.
	/// </summary>
	public class TumblrClientBase : IDisposable
	{
		private readonly object disposeLock = new object();
		private bool disposed;

		private readonly HttpClient httpClient;

		private readonly string _consumerKey;
		private readonly string _consumerSecret;

		private readonly IHmacSha1HashProvider _hashProvider;

		/// <summary>
		/// Initializes a new instance of the <see cref="TumblrClientBase"/> class.
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
		/// <param name="oAuthToken">
		/// An optional access token for the API. If no access token is provided, only the methods
		/// that do not require OAuth can be invoked successfully.
		/// </param>
		/// <remarks>
		///  You can get a consumer key and a consumer secret by registering an application with Tumblr:<br/>
		/// <br/>
		/// http://www.tumblr.com/oauth/apps
		/// <br/><br/>platform: .NetStandard 1.1+
		/// </remarks>
		public TumblrClientBase(IHmacSha1HashProvider hashProvider, string consumerKey, string consumerSecret, Token oAuthToken = null)
		{
			if (consumerKey == null)
				throw new ArgumentNullException(nameof(consumerKey));

			if (consumerKey.Length == 0)
				throw new ArgumentException("Consumer key cannot be empty.", nameof(consumerKey));

			if (consumerSecret == null)
				throw new ArgumentNullException(nameof(consumerSecret));

			if (consumerSecret.Length == 0)
				throw new ArgumentException("Consumer secret cannot be empty.", nameof(consumerSecret));

			if (oAuthToken != null)
			{
				if (oAuthToken.IsValid == false)
					throw new ArgumentException("oAuthToken is not valid", nameof(oAuthToken));
			}

			this.oAuthToken = oAuthToken;

			_consumerKey = consumerKey;
			_consumerSecret = consumerSecret;

			_hashProvider = hashProvider;

			this.httpClient = new HttpClient();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TumblrClientBase"/> class.
		/// </summary>
		/// <param name="httpClientFactory"> <see cref="IHttpClientFactory">HttpClientFactory</see> to create internal HttpClient</param>
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
		/// <param name="oAuthToken">
		/// An optional access token for the API. If no access token is provided, only the methods
		/// that do not require OAuth can be invoked successfully.
		/// </param>
		/// <remarks>
		///  You can get a consumer key and a consumer secret by registering an application with Tumblr:<br/>
		/// http://www.tumblr.com/oauth/apps 
		/// <br/><br/>platform: .NetStandard 2.0+, .Net Core 2.2+
		/// </remarks>
		public TumblrClientBase(IHttpClientFactory httpClientFactory, IHmacSha1HashProvider hashProvider, string consumerKey, string consumerSecret, Token oAuthToken = null)
		{
			if (httpClientFactory == null)
				throw new ArgumentNullException(nameof(httpClientFactory));

			if (consumerKey == null)
				throw new ArgumentNullException(nameof(consumerKey));

			if (consumerKey.Length == 0)
				throw new ArgumentException("Consumer key cannot be empty.", nameof(consumerKey));

			if (consumerSecret == null)
				throw new ArgumentNullException(nameof(consumerSecret));

			if (consumerSecret.Length == 0)
				throw new ArgumentException("Consumer secret cannot be empty.", nameof(consumerSecret));

			if (oAuthToken != null)
			{
				if (oAuthToken.IsValid == false)
					throw new ArgumentException("oAuthToken is not valid", nameof(oAuthToken));
			}

			this.oAuthToken = oAuthToken;

			_consumerKey = consumerKey;
			_consumerSecret = consumerSecret;

			_hashProvider = hashProvider;

			this.httpClient = httpClientFactory.CreateClient();
		}

		#region Public Properties

		private readonly Token oAuthToken;
		/// <summary>
		/// Gets the OAuth <see cref="Token"/> used when the object was created.
		/// </summary>
		public Token OAuthToken { get { return oAuthToken; } }

		#endregion

		#region Public Methods

		/// <summary>
		/// Asynchronously invokes a method on the Tumblr API, and performs a projection on the
		/// response before returning the result.
		/// </summary>
		/// <typeparam name="TResponse">
		/// The type of the response received from the API. This must be a type that can be deserialized
		/// from the response JSON.
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The actual type that is the result of the method.
		/// </typeparam>
		/// <param name="method">
		/// The <see cref="ApiMethod"/> to invoke.
		/// </param>
		/// <param name="projection">
		/// The projection function that transforms <typeparamref name="TResponse"/> into <typeparamref name="TResult"/>.
		/// </param>
		/// <param name="cancellationToken">
		/// A <see cref="CancellationToken"/> that can be used to cancel the operation.
		/// </param>
		/// <param name="converters">
		/// An optional list of JSON converters that will be used while deserializing the response from the Tumblr API.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry a <typeparamref name="TResult"/> instance. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="method"/> is <b>null</b>.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="projection"/> is <b>null</b>.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		public async Task<TResult> CallApiMethodAsync<TResponse, TResult>(ApiMethod method, Func<TResponse, TResult> projection, CancellationToken cancellationToken, IEnumerable<JsonConverter> converters = null)
			where TResult : class
			where TResponse : class
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (method == null)
				throw new ArgumentNullException(nameof(method));

			if (projection == null)
				throw new ArgumentNullException(nameof(projection));

			var response = await CallApiMethodAsync<TResponse>(method, cancellationToken, converters).ConfigureAwait(false);
			return projection(response);
		}

		/// <summary>
		/// Asynchronously invokes a method on the Tumblr API without expecting a response.
		/// </summary>
		/// <param name="method">
		/// The <see cref="ApiMethod"/> to invoke.
		/// </param>
		/// <param name="cancellationToken">
		/// A <see cref="CancellationToken"/> that can be used to cancel the operation.
		/// </param>
		/// <returns>
		///  A <see cref="Task"/> that can be used to track the operation. If the task fails, <see cref="Task.Exception"/> 
		///  will carry a <see cref="TumblrException"/> representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="method"/> is <b>null</b>.
		/// </exception>
		public Task CallApiMethodNoResultAsync(ApiMethod method, CancellationToken cancellationToken)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (method == null)
				throw new ArgumentNullException(nameof(method));

			return CallApiMethodAsync<object>(method, cancellationToken);
		}

		/// <summary>
		/// Asynchronously invokes a method on the Tumblr API.
		/// </summary>
		/// <typeparam name="TResult">
		/// The type of the response received from the API. This must be a type that can be deserialized
		/// from the response JSON.
		/// </typeparam>
		/// <param name="method">
		/// The <see cref="ApiMethod"/> to invoke.
		/// </param>
		/// <param name="cancellationToken">
		/// A <see cref="CancellationToken"/> that can be used to cancel the operation.
		/// </param>
		/// <param name="converters">
		/// An optional list of JSON converters that will be used while deserializing the response from the Tumblr API.
		/// </param>
		/// <returns>
		/// A <see cref="Task{TResult}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{TResult}.Result"/> will
		/// carry a <typeparamref name="TResult"/> instance. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="method"/> is <b>null</b>.
		/// </exception>
		public async Task<TResult> CallApiMethodAsync<TResult>(ApiMethod method, CancellationToken cancellationToken, IEnumerable<JsonConverter> converters = null)
			where TResult : class
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (method == null)
				throw new ArgumentNullException(nameof(method));

			//build the api call URL
			StringBuilder apiRequestUrl = new StringBuilder(method.Url);
			if (method.HttpMethod == HttpMethod.Get && method.Parameters.Count > 0)
			{
				//we are in a HTTP GET: add the request parameters to the query string
				apiRequestUrl.Append("?");
				apiRequestUrl.Append(method.Parameters.ToFormUrlEncoded());
			}

			using (var request = new HttpRequestMessage(method.HttpMethod, apiRequestUrl.ToString()))
			{
				if (method.OAuthToken != null)
					method.Parameters.Add("oauth_token", method.OAuthToken.Key);

				if (method.Parameters.Any(c => c is BinaryMethodParameter))
				{
					//if there is binary content we submit a multipart form request
					var content = new MultipartFormDataContent();
					foreach (var p in method.Parameters)
						content.Add(p.AsHttpContent());

					request.Content = content;
				}
				else
				{
					//otherwise just a form url encoded request
					var content = new FormUrlEncodedContent(method.Parameters.Select(c => new KeyValuePair<string, string>(c.Name, ((StringMethodParameter)c).Value)));
					request.Content = content;
				}

				await request.PreparationForTumblrClient(_hashProvider, _consumerKey, _consumerSecret, oAuthToken);

				using (var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false))
				{
					var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
					using (var reader = new JsonTextReader(new StreamReader(stream)))
					{
						var serializer = CreateSerializer(converters);
						if (response.IsSuccessStatusCode)
						{
							return serializer.Deserialize<TumblrRawResponse<TResult>>(reader).Response;
						}
						else
						{
							TumblrError[] errorResponse;

#if (NETSTANDARD1_3 || NETSTANDARD2_0 || NETCOREAPP2_2)
							errorResponse = Array.Empty<TumblrError>();
#else
							errorResponse = new TumblrError[0];
#endif

							switch (response.StatusCode)
							{
								case System.Net.HttpStatusCode.Unauthorized :
									{
										break;
									}
								default:
									{
										if (response.Content.Headers.ContentType.MediaType == "application/json")
										{
											errorResponse = serializer.Deserialize<TumblrErrorResponse>(reader).Errors;
										}
										break;
									}
							}

							throw new TumblrException(response.StatusCode, response.ReasonPhrase, errorResponse?.ToList());
						}
					}
				}
			}
		}

#endregion

		#region Private Methods
		
		/// <summary>
		/// Create serialize
		/// </summary>
		/// <param name="converters"><see cref="IEnumerable{T}" /> from <see cref="JsonConverter"/></param>
		/// <returns><see cref="JsonSerializer"/></returns>
		private JsonSerializer CreateSerializer(IEnumerable<JsonConverter> converters)
		{
			JsonSerializer serializer = new JsonSerializer();
			if (converters != null)
			{
				foreach (JsonConverter converter in converters)
					serializer.Converters.Add(converter);
			}

			return serializer;
		}

		#endregion

		#region IDisposable Implementation

		/// <summary>
		/// Disposes of the object and the internal HttpClient instance.
		/// </summary>
#pragma warning disable CA1063 // Implement IDisposable Correctly
		public void Dispose()
#pragma warning restore CA1063 // Implement IDisposable Correctly
		{
			if (!disposed)
			{
				lock (disposeLock)
				{
					if (!disposed)
					{
						disposed = true;

						httpClient.Dispose();
						
						Dispose(true);
						
						GC.SuppressFinalize(this);
					}
				}
			}
		}

		/// <summary>
		/// Subclasses can override this method to provide custom
		/// disposing logic.
		/// </summary>
		/// <param name="disposing">
		/// <b>true</b> if managed resources have to be disposed; otherwise <b>false</b>.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{ }

		#endregion
	}
}
