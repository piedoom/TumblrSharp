using System;
using System.Collections.Generic;
using System.Linq;

namespace DontPanic.TumblrSharp
{
	/// <summary>
	/// A sorted set of <see cref="IMethodParameter"/> instances.
	/// </summary>
	public class MethodParameterSet : SortedSet<IMethodParameter>
	{
		private class MethodParameterComparer : IComparer<IMethodParameter>
		{
			public int Compare(IMethodParameter x, IMethodParameter y)
			{
				return x.Name.CompareTo(y.Name);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MethodParameterSet"/> class.
		/// </summary>
		public MethodParameterSet()
			: base(new MethodParameterComparer())
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="MethodParameterSet"/> class.
		/// </summary>
		/// <param name="collection">
		/// An enumerable list of <see cref="IMethodParameter"/> instances to insert in the set.
		/// </param>
		public MethodParameterSet(IEnumerable<IMethodParameter> collection)
			: base(collection, new MethodParameterComparer())
		{ }

		internal MethodParameterSet(string formUrlEncodedValue)
			: base(new MethodParameterComparer())
		{
			if (formUrlEncodedValue == null)
				throw new ArgumentNullException("formUrlEncodedValue");

			if (formUrlEncodedValue.Contains("&") || formUrlEncodedValue.Contains("="))
			{
				var pairs = formUrlEncodedValue.Split('&');
				foreach (var pair in pairs)
				{
					var nameValue = pair.Split('=');
					if (nameValue.Length < 2)
						continue;

					this.Add(nameValue[0], System.Net.WebUtility.UrlDecode(nameValue[1]));
				}
			}
		}

		/// <summary>
		/// Adds a new parameter to the set.
		/// </summary>
		/// <param name="name">
		/// The parameter name.
		/// </param>
		/// <param name="value">
		/// The parameter value.
		/// </param>
		/// <param name="defaultValue">
		/// The parameter's default value.
		/// </param>
		/// <remarks>
		/// Tumblr API methods define default values for most of the method's parameters. A parameter
		/// whose value equals to the default value can be omitted from the request; by specifying the 
		/// <paramref name="defaultValue"/> the parameter won't be added to the set if its <paramref name="value"/>
		/// equals to the default value.
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="name"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="name"/> name is empty or a whitespace string.
		/// </exception>
		public void Add(string name, long value, long? defaultValue = null)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			if (name.Trim().Length == 0)
				throw new ArgumentException("Parameter name cannot be empty or whitespace.", "name");

			if(!defaultValue.HasValue || defaultValue.Value != value)
				Add(new StringMethodParameter(name, value));
		}

		/// <summary>
		/// Adds a new parameter to the set.
		/// </summary>
		/// <param name="name">
		/// The parameter name.
		/// </param>
		/// <param name="value">
		/// The parameter value.
		/// </param>
		/// <param name="defaultValue">
		/// The parameter's default value.
		/// </param>
		/// <remarks>
		/// Tumblr API methods define default values for most of the method's parameters. A parameter
		/// whose value equals to the default value can be omitted from the request; by specifying the 
		/// <paramref name="defaultValue"/> the parameter won't be added to the set if its <paramref name="value"/>
		/// equals to the default value.
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="name"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="name"/> name is empty or a whitespace string.
		/// </exception>
		public void Add(string name, double value, double? defaultValue = null)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			if (name.Trim().Length == 0)
				throw new ArgumentException("Parameter name cannot be empty or whitespace.", "name");

			if (!defaultValue.HasValue || defaultValue.Value != value)
				Add(new StringMethodParameter(name, value));
		}

		/// <summary>
		/// Adds a new parameter to the set.
		/// </summary>
		/// <param name="name">
		/// The parameter name.
		/// </param>
		/// <param name="value">
		/// The parameter value.
		/// </param>
		/// <param name="defaultValue">
		/// The parameter's default value.
		/// </param>
		/// <remarks>
		/// Tumblr API methods define default values for most of the method's parameters. A parameter
		/// whose value equals to the default value can be omitted from the request; by specifying the 
		/// <paramref name="defaultValue"/> the parameter won't be added to the set if its <paramref name="value"/>
		/// equals to the default value.
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="name"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="name"/> name is empty or a whitespace string.
		/// </exception>
		public void Add(string name, int value, int? defaultValue = null)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			if (name.Trim().Length == 0)
				throw new ArgumentException("Parameter name cannot be empty or whitespace.", "name");

			if (!defaultValue.HasValue || defaultValue.Value != value)
				Add(new StringMethodParameter(name, value));
		}

		/// <summary>
		/// Adds a new parameter to the set.
		/// </summary>
		/// <param name="name">
		/// The parameter name.
		/// </param>
		/// <param name="value">
		/// The parameter value.
		/// </param>
		/// <param name="defaultValue">
		/// The parameter's default value.
		/// </param>
		/// <remarks>
		/// Tumblr API methods define default values for most of the method's parameters. A parameter
		/// whose value equals to the default value can be omitted from the request; by specifying the 
		/// <paramref name="defaultValue"/> the parameter won't be added to the set if its <paramref name="value"/>
		/// equals to the default value.
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="name"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="name"/> name is empty or a whitespace string.
		/// </exception>
		public void Add(string name, bool value, bool? defaultValue = null)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			if (name.Trim().Length == 0)
				throw new ArgumentException("Parameter name cannot be empty or whitespace.", "name");

			if (!defaultValue.HasValue || defaultValue.Value != value)
				Add(new StringMethodParameter(name, value));
		}

		/// <summary>
		/// Adds a new parameter to the set.
		/// </summary>
		/// <param name="name">
		/// The parameter name.
		/// </param>
		/// <param name="value">
		/// The parameter value.
		/// </param>
		/// <param name="defaultValue">
		/// The parameter's default value.
		/// </param>
		/// <remarks>
		/// Tumblr API methods define default values for most of the method's parameters. A parameter
		/// whose value equals to the default value can be omitted from the request; by specifying the 
		/// <paramref name="defaultValue"/> the parameter won't be added to the set if its <paramref name="value"/>
		/// equals to the default value.
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="name"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="name"/> name is empty or a whitespace string.
		/// </exception>
		public void Add(string name, string value, string defaultValue = null)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			if (name.Trim().Length == 0)
				throw new ArgumentException("Parameter name cannot be empty or whitespace.", "name");

			if (defaultValue != value)
				Add(new StringMethodParameter(name, value));
		}

		/// <summary>
		/// Adds a new parameter to the set.
		/// </summary>
		/// <param name="name">
		/// The parameter name.
		/// </param>
		/// <param name="value">
		/// The parameter value.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="name"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="name"/> name is empty or a whitespace string.
		/// </exception>
		public void Add(string name, byte[] value)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			if (name.Trim().Length == 0)
				throw new ArgumentException("Parameter name cannot be empty or whitespace.", "name");

			if(value != null)
				Add(new BinaryMethodParameter(name, value));
		}

		internal string ToFormUrlEncoded()
		{
			return String.Join("&", this.Where(c => c is StringMethodParameter).Select(c => String.Format("{0}={1}", c.Name, Encode(c))));
		}

		internal string ToAuthorizationHeader()
		{
			return String.Join(",", this.Select(c => String.Format("{0}=\"{1}\"", c.Name, Encode(c))));
		}

		private string Encode(IMethodParameter p)
		{
			return UrlEncoder.Encode(((StringMethodParameter)p).Value);
		}
	}
}
