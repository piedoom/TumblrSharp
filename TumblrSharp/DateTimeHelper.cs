using System;

namespace DontPanic.TumblrSharp
{
	/// <summary>
	/// Converts from Unix-type timestamp to <see cref="DateTime"/> and vice-versa.
	/// </summary>
	public static class DateTimeHelper
	{
		/// <summary>
		/// Converts from a timestamp to a <see cref="DateTime"/>. The result is in local time.
		/// </summary>
		/// <param name="timestamp">
		/// The timestamp.
		/// </param>
		/// <returns>
		/// The equivalent <see cref="DateTime"/> in local time.
		/// </returns>
		public static DateTime FromTimestamp(long timestamp)
		{
#if (NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP2_1)
			DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(timestamp);
			return dto.DateTime.ToLocalTime();
#else
			return FromTimestamp((double)timestamp);
#endif
		}

		/// <summary>
		/// Converts from a timestamp to a <see cref="DateTime"/>. The result is in local time.
		/// </summary>
		/// <param name="timestamp">
		/// The timestamp.
		/// </param>
		/// <returns>
		/// The equivalent <see cref="DateTime"/> in local time.
		/// </returns>
		public static DateTime FromTimestamp(Double timestamp)
		{
#if (NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP2_1)
			DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds((long)timestamp);
			return dto.DateTime.ToLocalTime();
#else
			DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			return origin.AddSeconds(timestamp).ToLocalTime();
#endif
		}

		/// <summary>
		/// Converts from a <see cref="DateTime"/> to a timestamp.
		/// </summary>
		/// <param name="date">
		/// The <see cref="DateTime"/>.
		/// </param>
		/// <returns>
		/// The timestamp.
		/// </returns>
		public static long ToTimestamp(DateTime date)
		{
#if (NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP2_1)
			DateTimeOffset dto = new DateTimeOffset(date);
			return dto.ToUnixTimeSeconds();
#else
			DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			TimeSpan diff = date.ToUniversalTime() - origin;
			return (long)Math.Floor(diff.TotalSeconds);
#endif
		}
	}
}
