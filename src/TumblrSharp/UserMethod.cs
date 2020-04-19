using System;
using System.Net.Http;
using DontPanic.TumblrSharp.OAuth;

namespace DontPanic.TumblrSharp
{
	/// <summary>
	/// Represents a user API method.
	/// </summary>
	public class UserMethod : ApiMethod
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserMethod"/> class.
		/// </summary>
		/// <param name="methodName">
		/// The name of the method to call. The method url will be automatically built.
		/// </param>
		/// <param name="oAuthToken">
		/// The OAuth <see cref="Token"/> to use for the call. Can be <b>null</b> if the 
		/// method does not require OAuth.
		/// </param>
		/// <param name="httpMethod">
		/// The required <see cref="HttpMethod"/> for the Tumblr API call. Only <see cref="HttpMethod.Get"/> and
		/// <see cref="HttpMethod.Post"/> are supported.
		/// </param>
		/// <param name="parameters">
		/// The parameters for the Tumblr API call. Can be <b>null</b> if the method does not require parameters.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="methodName"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		///	<paramref name="methodName"/> is empty.
		/// </exception>
		public UserMethod(
			string methodName,
			Token oAuthToken,
			HttpMethod httpMethod,
			MethodParameterSet parameters = null)
			: base(String.Format("https://api.tumblr.com/v2/user/{0}", methodName), oAuthToken, httpMethod, parameters)
		{
			if (methodName == null)
				throw new ArgumentNullException("methodName");

			if (methodName.Length == 0)
				throw new ArgumentException("Method name cannot be empty.", "methodName");
		}
	}
}
