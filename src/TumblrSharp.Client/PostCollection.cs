using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
    internal class PostCollection
    {
        [JsonConverter(typeof(PostArrayConverter))]
        [JsonProperty(PropertyName = "posts")]
        public BasePost[] Posts { get; set; }
    }
}
