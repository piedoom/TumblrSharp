using Newtonsoft.Json;
using System;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Contains information about a blog's followers.
	/// </summary>
    public class Followers
    {
		/// <summary>
		/// The number of users currently following the blog.
		/// </summary>
        [JsonProperty(PropertyName = "total_users")]
        public long Count { get; set; }

		/// <summary>
		/// An array of <see cref="BlogBase"/> instances, representing information
		/// about each user following the blog.
		/// </summary>
        [JsonProperty(PropertyName = "users")]
        public BlogBase[] Result { get; set; }

		/// <summary>
		/// The user's name on tumblr.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string UserName { get; set; }

		/// <summary>
		/// The URL of the user's primary blog.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string PrimaryBlogUrl { get; set; }

		/// <summary>
		/// The date and time when the blog was last updated (in local time).
		/// </summary>
		[JsonConverter(typeof(TimestampConverter))]
		[JsonProperty(PropertyName = "updated")]
		public DateTime LastUpdated { get; set; }
    }
}
