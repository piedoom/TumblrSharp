using System;
using System.Linq;
using System.Net.Http;

namespace DontPanic.TumblrSharp
{
	internal class BinaryMethodParameter : IMethodParameter
	{
		private readonly string name;
		private readonly byte[] value;
		private readonly string fileName;
		private readonly string mimeType;

		public BinaryMethodParameter(string name, byte[] value, string fileName = null, string mimeType = null)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			if (name.Trim().Length == 0)
				throw new ArgumentException("Parameter name cannot be empty or whitespace.", "name");

			this.name = name;
			this.value = value;
			this.fileName = fileName ?? "file";
			this.mimeType = mimeType;
		}

		public string Name { get { return name; } }

		public string FileName { get { return fileName; } }

		public HttpContent AsHttpContent()
		{
			var content = new ByteArrayContent(value);

			content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
			content.Headers.ContentDisposition.Name = this.Name;
			if (!String.IsNullOrEmpty(this.FileName))
				content.Headers.ContentDisposition.FileName = this.FileName;

			if (!String.IsNullOrEmpty(mimeType))
				content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mimeType);

			return content;
		}

		public bool Equals(IMethodParameter other)
		{
			var p = other as BinaryMethodParameter;
			if (p == null)
				return false;

			return (this.Name == other.Name && this.value.SequenceEqual(p.value));
		}
		
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() ^ this.value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as BinaryMethodParameter);
		}

		public override string ToString()
		{
			return String.Format("{0}={{binary}}", Name);
		}
	}
}
