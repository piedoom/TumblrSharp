using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DontPanic.TumblrSharp.Client
{
    /// <summary>
    /// Converter for trail theme
    /// </summary>
    public class TrailThemeConverter : JsonConverter
    {
        /// <exclude/>
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(TrailTheme))
            {
                return true;
            }

            return false;
        }

        /// <exclude/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            TrailTheme result = null;

            if (reader.TokenType == JsonToken.StartArray)
            {
                reader.Read();

                if (reader.TokenType == JsonToken.EndArray)
                {
                    return result;
                }

                return result;
            }

            JToken jt = JToken.Load(reader);

            result = jt.ToObject<TrailTheme>();

            return result;
        }

        /// <exclude/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            TrailTheme trailTheme = (TrailTheme)value;

            JObject jo = JObject.FromObject(trailTheme);
            jo.WriteTo(writer);

        }
    }
}
