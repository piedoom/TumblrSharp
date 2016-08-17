using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Represents a quote post.
	/// </summary>
    public class QuotePost : BasePost
    {
		/// <summary>
		/// The text of the quote.
		/// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

		/// <summary>
		/// Full HTML for the source of the quote.
		/// </summary>
        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }
    }
}
