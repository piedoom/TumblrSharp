using System;

namespace DontPanic.TumblrSharp.OAuth
{
	/// <summary>
	/// Represents a token for OAuth.
	/// </summary>
	public class Token : IEquatable<Token>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Token"/> class.
		/// </summary>
		/// <param name="key">
		/// The token's key.
		/// </param>
		/// <param name="secret">
		/// The token's secret.
		/// </param>
		public Token(string key, string secret)
		{
			Key = key;
			Secret = secret;
		}

		/// <summary>
		/// Gets the token's key.
		/// </summary>
		public string Key { get; private set; }

		/// <summary>
		/// Gets the token's secret.
		/// </summary>
		public string Secret { get; private set; }

		/// <summary>
		/// <b>true</b> is the token is valid; otherwise <b>false</b>.
		/// </summary>
		/// <remarks>
		/// A token is valid if both <see cref="Key"/> and <see cref="Secret"/> are not null or empty.
		/// </remarks>
		public bool IsValid { get { return !String.IsNullOrEmpty(Key) && !String.IsNullOrEmpty(Secret); } }

		/// <exclude/>
		public bool Equals(Token other)
		{
			if (other == null)
				return false;

			return this.Key == other.Key && this.Secret == other.Secret;
		}

		/// <exclude/>
		public override int GetHashCode()
		{
			return ((Key != null) ? Key.GetHashCode() : 0) ^ ((Secret != null) ? Secret.GetHashCode() : 0);
		}

		/// <exclude/>
		public override bool Equals(object obj)
		{
			return Equals(obj as Token);
		}

		/// <exclude/>
		public override string ToString()
		{
			return String.Format("Key={0}; Secret={1}", Key, Secret);
		}
	}
}
