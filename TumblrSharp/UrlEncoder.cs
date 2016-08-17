using System;
using System.Collections.Generic;

namespace DontPanic.TumblrSharp
{
	internal static class UrlEncoder
	{
		private const string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~.-_";

		public static string Encode(string value)
		{
			if (value == null)
				return String.Empty;

			var bytes = System.Text.Encoding.UTF8.GetBytes(value);
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			foreach (byte b in bytes)
			{
				char c = (char)b;
				if (unreservedChars.IndexOf(c) >= 0)
					sb.Append(c);
				else
					sb.Append(String.Format("%{0:X2}", b));
			}
			
			return sb.ToString();
		}

		public static string Decode(string value)
		{
			if (value == null)
				return String.Empty;

			char[] chars = value.ToCharArray();

			List<byte> buffer = new List<byte>(chars.Length);
			for (int i = 0; i < chars.Length; i++)
			{
				if (value[i] == '%')
				{
					byte decodedChar = (byte)Convert.ToInt32(new string(chars, i + 1, 2), 16);
					buffer.Add(decodedChar);

					i += 2;
				}
				else
				{
					buffer.Add((byte)value[i]);
				}
			}

			return System.Text.Encoding.UTF8.GetString(buffer.ToArray(), 0, buffer.Count);
		}
	}
}
