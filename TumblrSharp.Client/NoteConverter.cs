using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DontPanic.TumblrSharp.Client
{
    /// <summary>
    /// convert note
    /// </summary>
    public class NoteConverter : JsonConverter
    {
        /// <exclude/>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(BaseNote[]);
        }

        /// <exclude/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            List<BaseNote> list = new List<BaseNote>();
            reader.Read();
            do
            {
                if (reader.TokenType == JsonToken.EndArray)
                    break;

                JObject jo = JObject.Load(reader);
                switch (jo["type"].ToString())
                {
                    case "post_attribution":
                        list.Add(jo.ToObject<PostAttributionNote>());
                        break;

                    default:
                        list.Add(jo.ToObject<BaseNote>());
                        break;
                }
            }
            while (reader.Read() && reader.TokenType != JsonToken.EndArray);

            return list;
        }

        /// <exclude/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
