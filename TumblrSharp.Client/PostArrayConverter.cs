using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

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

                    case "submission":
                        list.Add(jo.ToObject<AnswerPost>());
                        break;
						
					default:
						list.Add(jo.ToObject<AnswerPost>());
						break;
                }
            }
            while (reader.Read() && reader.TokenType != JsonToken.EndArray);

            return list.ToArray();
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

            BasePost[] basePosts = (BasePost[])value;

            if (basePosts.Count() > 0)
            {
                foreach (var basePost in basePosts)
                {
                    JObject jo;

                    switch (basePost)
                    {
                        case TextPost sp:
                            jo = JObject.FromObject(sp);
                            break;

                        case QuotePost sp:
                            jo = JObject.FromObject(sp);
                            break;

                        case PhotoPost sp:
                            jo = JObject.FromObject(sp);
                            break;

                        case LinkPost sp:
                            jo = JObject.FromObject(sp);
                            break;

                        case AnswerPost sp:
                            jo = JObject.FromObject(sp);
                            break;

                        case AudioPost sp:
                            jo = JObject.FromObject(sp);
                            break;

                        case ChatPost sp:
                            jo = JObject.FromObject(sp);
                            break;

                        case VideoPost sp:
                            jo = JObject.FromObject(sp);
                            break;
                            
                        default:
                            jo = JObject.FromObject(basePost);
                            break;
                    }

                    jo.WriteTo(writer);
                }                
            }

            writer.WriteEndArray();
        }
    }
}
