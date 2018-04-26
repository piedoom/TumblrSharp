using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Contains the blogs that a user is following.
	/// </summary>
    public class Following
    {
		/// <summary>
		/// The number of blogs the user is following.
		/// </summary>
        [JsonProperty(PropertyName = "total_blogs")]
        public int Count { get; set; }

		/// <summary>
		/// An array of <see cref="BlogBase"/> instances, representing information
		/// about each followed blog.
		/// </summary>
        [JsonProperty(PropertyName = "blogs")]
        public BlogBase[] Result { get; set; }
    }
}
