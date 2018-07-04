using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
    /// <summary>
    /// reblog
    /// </summary>
    public class Reblog
    {
        /// <summary>
        /// Comment
        /// </summary>
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Tree Html
        /// </summary>
        [JsonProperty(PropertyName = "tree_html")]
        public string TreeHtml { get; set; }
    }
}