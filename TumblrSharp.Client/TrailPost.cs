using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
    /// <summary>
    /// post object from <see cref="Trail" />
    /// </summary>
    public class TrailPost
    {
        /// <summary>
        /// id from post
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
    }
}