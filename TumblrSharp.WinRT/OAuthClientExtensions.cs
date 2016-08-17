using DontPanic.TumblrSharp.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace DontPanic.TumblrSharp
{
	/// <summary>
	/// Extends <see cref="OAuthClient"/> so to perform the full OAuth handshake with a single method call.
	/// </summary>
	public static class OAuthClientExtensions
	{
		/// <summary>
		/// Asynchronously performs the OAuth flow as a single method call and returns the access token.
		/// </summary>
		/// <param name="client">
		/// The <see cref="OAuthClient"/> being extended.
		/// </param>
		/// <returns>
		/// The access token.
		/// </returns>
		/// <exception cref="OAuthException">
		/// An exception occurred during the method call.
		/// </exception>
		public static async Task<Token> PerformOAuthAsync(this OAuthClient client)
		{
			Uri callbackUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();

			var requestToken = await client.GetRequestTokenAsync(callbackUri.ToString());

			WebAuthenticationResult webAuthResult = await WebAuthenticationBroker.AuthenticateAsync(
									WebAuthenticationOptions.None,
									client.GetAuthorizeUrl(requestToken),
									callbackUri);

			if (webAuthResult.ResponseStatus == WebAuthenticationStatus.Success)
			{
				return await client.GetAccessTokenAsync(requestToken, webAuthResult.ResponseData);
			}
			else if (webAuthResult.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
			{
				throw new OAuthException(String.Format("HTTP Error returned by AuthenticateAsync(): {0}", webAuthResult.ResponseErrorDetail.ToString()));
			}
			else
			{
				throw new OAuthException(String.Format("Error returned by AuthenticateAsync(): {0}", webAuthResult.ResponseStatus.ToString()));
			}
		}
	}
}
