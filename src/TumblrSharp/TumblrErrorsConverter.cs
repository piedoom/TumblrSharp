using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DontPanic.TumblrSharp
{
    /// <summary>
    /// convert TumblrError
    /// </summary>
    public class TumblrErrorsConverter : JsonConverter
	{
        /// <exclude/>
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(TumblrError));
        }

        /// <exclude/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            List<TumblrError> result = new List<TumblrError>();

            if (reader.TokenType == JsonToken.StartArray)
            {
                reader.Read();
                if (reader.TokenType == JsonToken.EndArray)
                    return new TumblrError();

                do
                {
                    JObject jo = JObject.Load(reader);
                    result.Add(jo.ToObject<TumblrError>());
                }
                while (reader.Read() && reader.TokenType != JsonToken.EndArray);
            }

            return result.ToArray();
        }

        /// <exclude/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            List<TumblrError> listToWrite = value as List<TumblrError>;

            writer.WriteStartArray();

            foreach (var element in listToWrite)
            {
                JObject jo = JObject.FromObject(element);
                jo.WriteTo(writer);
            }

            writer.WriteEndArray();
        }
    }
}
