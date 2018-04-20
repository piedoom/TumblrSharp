using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Represents a chat post.
	/// </summary>
    public class ChatPost : BasePost
    {
		/// <summary>
		/// The optional title of the post.
		/// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

		/// <summary>
		/// The full chat body.
		/// </summary>
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

		/// <summary>
		/// An array of <see cref="ChatFragment"/> instances, representing the chat.
		/// </summary>
        [JsonProperty(PropertyName = "dialogue")]
        public ChatFragment[] Dialogue { get; set; }
    }

	/// <summary>
	/// Represents a fragment of a <see cref="ChatPost"/>.
	/// </summary>
    public class ChatFragment
    {
		/// <summary>
		/// The label of the speaker.
		/// </summary>
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

		/// <summary>
		/// The name of the speaker.
		/// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

		/// <summary>
		/// The text.
		/// </summary>
        [JsonProperty(PropertyName = "phrase")]
        public string Phrase { get; set; }
    }
}
