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
            return objectType == typeof(List<BaseNote>);
        }

        /// <exclude/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            List<BaseNote> list = new List<BaseNote>();

            if (reader.TokenType == JsonToken.StartArray)
            {
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
            }

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

            List<BaseNote> notes = value as List<BaseNote>;

            if (notes.Count > 0)
            {
                foreach (BaseNote note in notes)
                {
                    if (note is PostAttributionNote)
                    {
                        PostAttributionNote spezificNote = (PostAttributionNote)note;
                        JObject jo = JObject.FromObject(spezificNote);
                        jo.WriteTo(writer);
                    }
                    else
                    {
                        JObject jo = JObject.FromObject(note);
                        jo.WriteTo(writer);
                    }
                }                
            }

            writer.WriteEndArray();
        }
    }
}
