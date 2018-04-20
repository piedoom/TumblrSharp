using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Generic converter for enumerations.
	/// </summary>
	public class EnumConverter : JsonConverter
	{
		/// <exclude/>
		public override bool CanConvert(Type objectType)
		{
			return objectType.GetTypeInfo().IsEnum;
		}

		/// <exclude/>
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteValue(value.ToString());
		}

		/// <exclude/>
		public override object ReadJson(JsonReader reader, Type	objectType, object existingValue, JsonSerializer serializer)
		{
			return Enum.Parse(objectType, reader.Value.ToString(), true);
		}
	}
}
