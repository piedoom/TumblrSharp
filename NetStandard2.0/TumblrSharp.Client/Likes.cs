using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Contains the user's likes.
	/// </summary>
	public class Likes 
    {
		/// <summary>
		/// Total number of liked posts.
		/// </summary>
		[JsonProperty(PropertyName = "liked_count")]
		public long Count { get; set; }

		/// <summary>
		/// An array of <see cref="BasePost"/> instances, representing
		/// the liked posts.
		/// </summary>
		[JsonConverter(typeof(PostArrayConverter))]
		[JsonProperty(PropertyName = "liked_posts")]
        public BasePost[] Result { get; set; }
    }
}
