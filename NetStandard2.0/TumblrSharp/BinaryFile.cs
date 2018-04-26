using System;
using System.IO;
using System.Linq;

namespace DontPanic.TumblrSharp
{
	/// <summary>
	/// Represents a binary file (photo, video or audio).
	/// </summary>
	public class BinaryFile
	{
		private readonly byte[] data;
		private readonly string fileName;
		private readonly string mimeType;

		/// <summary>
		/// Initializes a new instance of the <see cref="BinaryFile"/> class.
		/// </summary>
		/// <param name="data">
		/// The binary file content.
		/// </param>
		/// <param name="fileName">
		/// The file name.
		/// </param>
		/// <param name="mimeType">
		/// The file's mime type.
		/// </param>
		public BinaryFile(byte[] data, string fileName = null, string mimeType = null)
		{
			if (data == null)
				throw new ArgumentNullException("data");

			this.data = data;
			this.fileName = fileName ?? "file";
			this.mimeType = mimeType ?? GetMimeType(data);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BinaryFile"/> class.
		/// </summary>
		/// <param name="stream">
		/// The binary file content.
		/// </param>
		/// <param name="fileName">
		/// The file name.
		/// </param>
		/// <param name="mimeType">
		/// The file's mime type.
		/// </param>
		public BinaryFile(Stream stream, string fileName = null, string mimeType = null)
		{
			if (stream == null)
				throw new ArgumentNullException("stream");

			if (!stream.CanRead || !stream.CanSeek)
				throw new ArgumentException("The stream must be readable and seekable.", "stream");

			using (stream)
			{
				data = new byte[stream.Length];
				stream.Read(data, 0, data.Length);
			}

			this.fileName = fileName ?? "file";
			this.mimeType = mimeType ?? GetMimeType(data);
		}

		/// <summary>
		/// Gets the binary file content.
		/// </summary>
		public byte[] Data { get { return data; } }

		/// <summary>
		/// Gets the file name.
		/// </summary>
		public string FileName { get { return fileName; } }

		/// <summary>
		/// Gets the file's mime type.
		/// </summary>
		public string MimeType { get { return mimeType; } }

		private static string GetMimeType(byte[] data)
		{
			if (data == null)
				return null;

			// see http://www.mikekunz.com/image_file_header.html
			var bmp = new byte[] { 66, 77 };
			var gif = new byte[] { 71, 73, 70 };
			var png = new byte[] { 137, 80, 78, 71 };
			var tiff = new byte[] { 73, 73, 42 };
			var tiff2 = new byte[] { 77, 77, 42 };
			var jpeg = new byte[] { 255, 216, 255, 224 };
			var jpeg2 = new byte[] { 255, 216, 255, 225 };

			var buffer = new byte[4];
			for (int i = 0; i < 4; i++)
				buffer[i] = data[i];

			if (bmp.SequenceEqual(buffer.Take(bmp.Length)))
				return "image/bmp";

			if (gif.SequenceEqual(buffer.Take(gif.Length)))
				return "image/gif";

			if (png.SequenceEqual(buffer.Take(png.Length)))
				return "image/png";

			if (tiff.SequenceEqual(buffer.Take(tiff.Length)))
				return "image/tiff";

			if (tiff2.SequenceEqual(buffer.Take(tiff2.Length)))
				return "image/tiff";

			if (jpeg.SequenceEqual(buffer.Take(jpeg.Length)))
				return "image/jpeg";

			if (jpeg2.SequenceEqual(buffer.Take(jpeg2.Length)))
				return "image/jpeg";

			return "application/octet-stream";
		}
	}
}
