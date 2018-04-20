using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DontPanic.TumblrSharp
{
	/// <summary>
	/// Represents an error that occour during a Tumblr API call.
	/// </summary>
	public class TumblrException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TumblrException"/> class.
		/// </summary>
		/// <param name="statusCode">
		/// The <see cref="HttpStatusCode"/> of the error.
		/// </param>
		/// <param name="message">
		/// The error message.
		/// </param>
		/// <param name="errors">
		/// An optional list of extra errors.
		/// </param>
		/// <param name="innerException">
		/// An optional inner exception.
		/// </param>
		public TumblrException(HttpStatusCode statusCode, string message = null, IEnumerable<string> errors = null, Exception innerException = null)
			: base(message, innerException)
		{
			StatusCode = statusCode;
			Errors = (errors == null)
			 ? new System.Collections.ObjectModel.ReadOnlyCollection<string>(new List<string>())
			 : new System.Collections.ObjectModel.ReadOnlyCollection<string>(errors.ToList());
		}

		/// <summary>
		/// Gets the <see cref="HttpStatusCode"/> of the error.
		/// </summary>
		public HttpStatusCode StatusCode { get; private set; }

		/// <summary>
		/// Gets the extra error messages returned from the server (if any).
		/// </summary>
		public IReadOnlyCollection<string> Errors { get; private set; }
	}
}
