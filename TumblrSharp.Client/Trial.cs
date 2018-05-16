using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
    public class Trial
    {
        [JsonProperty(PropertyName = "blog")]
        public TrialBlog Blog { get; set; }

        [JsonProperty(PropertyName ="post")]
        public TrialPost Post { get; set; }

        [JsonProperty(PropertyName = "content_raw")]
        public string ContentRaw { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
    }
}