using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Represents a text post.
	/// </summary>
    public class TextPost : BasePost
    {
		/// <summary>
		/// The optional title of the post.
		/// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

		/// <summary>
		/// The full post body.
		/// </summary>
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }
    }
}
