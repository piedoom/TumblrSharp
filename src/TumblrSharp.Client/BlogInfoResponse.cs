using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
    internal class BlogInfoResponse
    {
        [JsonProperty(PropertyName = "blog")]
        public BlogInfo Blog { get; set; }
    }
}
