using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Contains information about a blog.
	/// </summary>
    public class BlogInfo : BlogBase
    {
		/// <summary>
		/// The total number of posts to this blog.
		/// </summary>
        [JsonProperty(PropertyName = "posts")]
        public long PostsCount { get; set; }

		/// <summary>
		/// The blog's description.
		/// </summary>
		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }
        
		/// <summary>
		/// Indicates whether the blog allows questions.
		/// </summary>
        [JsonProperty(PropertyName = "ask")]
        public bool IsAskEnabled { get; set; }

		/// <summary>
		/// Indicates whether the blog allows anonymous questions. Returned only if 
		/// <see cref="IsAskEnabled"/> is <b>true</b>.
		/// </summary>
        [JsonProperty(PropertyName = "ask_anon")]
        public bool IsAnonAskEnabled { get; set; }

		/// <summary>
		/// Number of likes for this user. Returned only if this is 
		/// the user's primary blog and sharing of likes is enabled.
		/// </summary>
        [JsonProperty(PropertyName = "likes")]
        public long LikesCount { get; set; }

		/// <summary>
		/// Whether the blog is marked as NSFW or not.
		/// </summary>
		[JsonProperty(PropertyName = "is_nsfw")]
		public bool IsNsfw { get; set; }
	}
}
