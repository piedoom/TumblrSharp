using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
    /// <summary>
    /// post object from <see cref="Trial" />
    /// </summary>
    public class TrialPost
    {
        /// <summary>
        /// id from post
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
    }
}