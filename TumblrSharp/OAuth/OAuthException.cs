using System;

namespace DontPanic.TumblrSharp.OAuth
{
	/// <summary>
	/// Represents an error that occour during a OAuth call.
	/// </summary>
	public class OAuthException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OAuthException"/> class.
		/// </summary>
		public OAuthException()
			: base()
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="OAuthException"/> class.
		/// </summary>
		/// <param name="message">
		/// The error message.
		/// </param>
		/// <param name="innerException">
		/// An optional inner exception.
		/// </param>
		public OAuthException(string message, Exception innerException = null)
			: base(message)
		{ }
	}
}
