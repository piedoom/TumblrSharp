using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Converts a timestamp to a <see cref="System.DateTime"/>.
	/// </summary>
	public class TimestampConverter : JsonConverter
	{
		/// <exclude/>
		public override bool CanConvert(Type objectType)
		{
			return objectType.Equals(typeof(Int64)) ||
				objectType.Equals(typeof(Int32)) ||
				objectType.Equals(typeof(Double));
		}

		/// <exclude/>
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var dateTime = (DateTime)value;
			writer.WriteValue(DateTimeHelper.ToTimestamp(dateTime));
		}

		/// <exclude/>
		public override object ReadJson(JsonReader reader, Type	objectType, object existingValue, JsonSerializer serializer)
		{
			var timestamp = Convert.ToDouble(reader.Value);
			return DateTimeHelper.FromTimestamp(timestamp);
		}
	}
}
