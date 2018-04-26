using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Contains information about a user's blog.
	/// </summary>
    public class UserBlogInfo : BlogBase
    {
		/// <summary>
		/// Whether the automatic tweet of posts is enabled or not.
		/// </summary>
		[JsonConverter(typeof(BoolConverter))]
        [JsonProperty(PropertyName = "tweet")]
        public bool IsTweetEnabled { get; set; }

		/// <summary>
		/// Whether the automatic posting to Facebook of posts is enabled or not.
		/// </summary>
		[JsonConverter(typeof(BoolConverter))]
		[JsonProperty(PropertyName = "facebook")]
		public bool IsFacebookPostEnabled { get; set; }

		/// <summary>
		/// The type of blog (public or private).
		/// </summary>
		[JsonConverter(typeof(EnumConverter))]
		[JsonProperty(PropertyName = "type")]
		public BlogType BlogType { get; set; }

		/// <summary>
		/// Whether if the user likes are public or not.
		/// </summary>
		[JsonProperty(PropertyName = "share_likes")]
		public bool IsSharingLikes { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty(PropertyName = "messages")]
		public int MessagesCount { get; set; }

		/// <summary>
		/// The number of posts in <see cref="PostCreationState.Queue"/> state.
		/// </summary>
		[JsonProperty(PropertyName = "queue")]
		public int QueueCount { get; set; }

		/// <summary>
		/// The number of posts in <see cref="PostCreationState.Draft"/> state.
		/// </summary>
		[JsonProperty(PropertyName = "drafts")]
		public int DraftsCount { get; set; }

		/// <summary>
		/// Indicates if this is the user's primary blog.
		/// </summary>
        [JsonProperty(PropertyName = "primary")]
        public bool IsPrimary { get; set; }

		/// <summary>
		/// Total count of followers for this blog.
		/// </summary>
        [JsonProperty(PropertyName = "followers")]
        public long FollowersCount { get; set; }
    }
}
