using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DontPanic.TumblrSharp
{
	internal class TumblrErrorsConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(TumblrErrors));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartArray)
			{
				reader.Read();
				if (reader.TokenType == JsonToken.EndArray)
					return new TumblrErrors() { Errors = new String[0] };

				throw new InvalidOperationException("Unexpected Token.");
			}
			else if (reader.TokenType == JsonToken.StartObject)
			{
				reader.Read();
				if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "errors")
				{
					reader.Read();
					if (reader.TokenType == JsonToken.StartArray)
					{
						List<string> errors = new List<string>();
						while (reader.Read() && reader.TokenType != JsonToken.EndArray)
						{
							if (reader.ValueType == typeof(string))
								errors.Add((string)reader.Value);
						}

						return new TumblrErrors() { Errors = errors.ToArray() };
					}
				}

				throw new InvalidOperationException("Unexpected Token.");
			}
			else
			{
				throw new InvalidOperationException("Unexpected Token.");
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
