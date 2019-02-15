using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DontPanic.TumblrSharp.Client
{
    /// <summary>
    /// convert trail
    /// </summary>
    public class TrailConverter : JsonConverter
    {
        /// <exclude/>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<Trail>);
        }

        /// <exclude/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            List<Trail> list = new List<Trail>();

            reader.Read();

            do
            {
                if (reader.TokenType == JsonToken.EndArray || reader.TokenType == JsonToken.EndObject)
                    break;

                JObject jo = JObject.Load(reader);

                list.Add(jo.ToObject<Trail>());
                
            }
            while (reader.Read() && reader.TokenType != JsonToken.EndArray);

            return list;
        }

        /// <exclude/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartArray();

            if (value == null)
            {   
                writer.WriteEndArray();
                return;
            }

            List<Trail> trails = value as List<Trail>;

            if (trails.Count > 0)
            {
                foreach (Trail trail in trails)
                {
                    JObject jo = JObject.FromObject(trail);
                    jo.WriteTo(writer);
                }                
            }

            writer.WriteEndArray();
        }
    }
}