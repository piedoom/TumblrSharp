using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Contains information about a user's account.
	/// </summary>
    public class UserInfo
    {
		/// <summary>
		/// The number of blogs the user is following
		/// </summary>
        [JsonProperty(PropertyName = "following")]
        public long FollowingCount { get; set; }

		/// <summary>
		/// The user's default <see cref="PostFormat"/>.
		/// </summary>
        [JsonConverter(typeof(EnumConverter))]
		[JsonProperty(PropertyName = "default_post_format")]
		public PostFormat DefaultPostFormat { get; set; }

		/// <summary>
		/// The user's tumblr short name.
		/// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

		/// <summary>
		/// The total count of the user's likes
		/// </summary>
        [JsonProperty(PropertyName = "likes")]
        public long LikesCount { get; set; }

		/// <summary>
		/// An array of <see cref="UserBlogInfo"/> instances, containing information
		/// about the user's blogs.
		/// </summary>
        [JsonProperty(PropertyName = "blogs")]
        public UserBlogInfo[] Blogs { get; set; }
    }
}
