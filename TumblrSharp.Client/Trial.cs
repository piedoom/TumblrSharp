using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
    /// <summary>
    /// Trial
    /// </summary>
    public class Trial
    {
        /// <summary>
        /// the blog
        /// </summary>
        [JsonProperty(PropertyName = "blog")]
        public TrialBlog Blog { get; set; }

        /// <summary>
        /// the post that trail
        /// </summary>
        [JsonProperty(PropertyName ="post")]
        public TrialPost Post { get; set; }

        /// <summary>
        /// ContentRaw
        /// </summary>
        [JsonProperty(PropertyName = "content_raw")]
        public string ContentRaw { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
    }
}