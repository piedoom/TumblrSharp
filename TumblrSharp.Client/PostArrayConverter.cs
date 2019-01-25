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
            if (value == null)
            {
                writer.WriteStartArray();
                writer.WriteEndArray();

                return;
            }

            List<BasePost> basePosts = (List<BasePost>)value;

            if (basePosts.Count > 0)
            {
                writer.WriteStartArray();

                foreach (var basePost in basePosts)
                {
                    if (basePost is TextPost)
                    {
                        TextPost sp = basePost as TextPost;

                        JObject jo = JObject.FromObject(sp);
                        jo.WriteTo(writer);
                    }
                    else
                    {
                        if (basePost is QuotePost)
                        {
                            QuotePost sp = basePost as QuotePost;

                            JObject jo = JObject.FromObject(sp);
                            jo.WriteTo(writer);
                        }
                        else
                        {
                            if (basePost is PhotoPost)
                            {
                                PhotoPost sp = basePost as PhotoPost;

                                JObject jo = JObject.FromObject(sp);
                                jo.WriteTo(writer);
                            }
                            else
                            {
                                if (basePost is LinkPost)
                                {
                                    LinkPost sp = basePost as LinkPost;

                                    JObject jo = JObject.FromObject(sp);
                                    jo.WriteTo(writer);
                                }
                                else
                                {
                                    if (basePost is AnswerPost)
                                    {
                                        AnswerPost sp = basePost as AnswerPost;

                                        JObject jo = JObject.FromObject(sp);
                                        jo.WriteTo(writer);
                                    }
                                    else
                                    {
                                        if (basePost is AudioPost)
                                        {
                                            AudioPost sp = basePost as AudioPost;

                                            JObject jo = JObject.FromObject(sp);
                                            jo.WriteTo(writer);
                                        }
                                        else
                                        {
                                            if (basePost is ChatPost)
                                            {
                                                ChatPost sp = basePost as ChatPost;

                                                JObject jo = JObject.FromObject(sp);
                                                jo.WriteTo(writer);
                                            }
                                            else
                                            {
                                                if (basePost is VideoPost)
                                                {
                                                    VideoPost sp = basePost as VideoPost;

                                                    JObject jo = JObject.FromObject(sp);
                                                    jo.WriteTo(writer);
                                                }
                                                else
                                                {
                                                    AnswerPost sp = basePost as AnswerPost;

                                                    JObject jo = JObject.FromObject(sp);
                                                    jo.WriteTo(writer);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                writer.WriteEndArray();
            }
        }
    }
}
