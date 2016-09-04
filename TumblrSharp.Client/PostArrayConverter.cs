using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Converts post objects to the proper post type.
	/// </summary>
    public class PostArrayConverter : JsonConverter
    {
		/// <exclude/>
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(BasePost[]));
        }

		/// <exclude/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            List<BasePost> list = new List<BasePost>();
            reader.Read();
            do
            {
                if (reader.TokenType == JsonToken.EndArray)
                    break;

                JObject jo = JObject.Load(reader);
                switch (jo["type"].ToString())
                {
                    case "text":
                        list.Add(jo.ToObject<TextPost>());
                        break;

                    case "quote":
                        list.Add(jo.ToObject<QuotePost>());
                        break;

                    case "photo":
                        list.Add(jo.ToObject<PhotoPost>());
                        break;

                    case "link":
                        list.Add(jo.ToObject<LinkPost>());
                        break;
                    
                    case "answer":
                        list.Add(jo.ToObject<AnswerPost>());
                        break;
                    
                    case "audio":
						list.Add(jo.ToObject<AudioPost>());
						break;

                    case "chat":
						list.Add(jo.ToObject<ChatPost>());
						break;

                    case "video":
						list.Add(jo.ToObject<VideoPost>());
                        break;
                }
            }
            while (reader.Read() && reader.TokenType != JsonToken.EndArray);

            return list.ToArray();
        }

		/// <exclude/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
