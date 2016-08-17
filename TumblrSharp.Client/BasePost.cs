using Newtonsoft.Json;
using System;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Base class for post types.
	/// </summary>
    public class BasePost
    {
		/// <summary>
		/// The short name used to uniquely identify a blog.
		/// </summary>
        [JsonProperty(PropertyName = "blog_name")]
        public string BlogName { get; set; }

		/// <summary>
		///The post identifier.
		/// </summary>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

		/// <summary>
		/// The post type.
		/// </summary>
        [JsonConverter(typeof(EnumConverter))]
		[JsonProperty(PropertyName = "type")]
        public PostType Type { get; set; }

		/// <summary>
		/// The url of the post.
		/// </summary>
        [JsonProperty(PropertyName = "post_url")]
        public string Url { get; set; }

		/// <summary>
		/// The date and time of the post (in local time).
		/// </summary>
		[JsonConverter(typeof(TimestampConverter))]
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }

		/// <summary>
		/// The number of notes (likes and reblogs) for the post.
		/// </summary>
        [JsonProperty(PropertyName = "note_count")]
        public long NotesCount { get; set; }

		/// <summary>
		/// The <see cref="PostFormat"/>.
		/// </summary>
		[JsonConverter(typeof(EnumConverter))]
        [JsonProperty(PropertyName = "format")]
        public PostFormat Format { get; set; }

		/// <summary>
		/// The key to use to reblog the post.
		/// </summary>
        [JsonProperty(PropertyName = "reblog_key")]
        public string ReblogKey { get; set; }

		/// <summary>
		/// The tags applied to the post.
		/// </summary>
        [JsonProperty(PropertyName = "tags")]
        public string[] Tags { get; set; }

		/// <summary>
		/// The url to the blog that is the source of the post, if any.
		/// </summary>
        [JsonProperty(PropertyName = "source_url")]
        public string SourceUrl { get; set; }

		/// <summary>
		/// The title of the blog which is the source of the post, if any.
		/// </summary>
        [JsonProperty(PropertyName = "source_title")]
        public string SourceTitle { get; set; }

		/// <summary>
		/// The number of posts in the response (can be used for pagination).
		/// </summary>
        [JsonProperty(PropertyName = "total_posts")]
        public int NumberOfPostsInResponse { get; set; }

		/// <summary>
		/// The <see cref="PostCreationState"/>.
		/// </summary>
		[JsonConverter(typeof(EnumConverter))]
		[JsonProperty(PropertyName = "state")]
		public PostCreationState State { get; set; }

		/// <summary>
		/// Indicates if the current user has already liked the post or not.
		/// </summary>
		[JsonProperty(PropertyName = "liked")]
		public string Liked { get; set; }

		/// <summary>
		/// Indicates whether the post was created via mobile/email publishing.
		/// </summary>
		[JsonProperty(PropertyName = "mobile")]
		public string IsMobile { get; set; }

		/// <summary>
		/// Indicates whether the post was created via the Tumblr bookmarklet.
		/// </summary>
		[JsonProperty(PropertyName = "bookmarklet")]
		public string IsBookmarklet { get; set; }

		/// <summary>
		/// The id of a post being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_from_id")]
		public long RebloggedFromId { get; set; }

		/// <summary>
		/// Name of a blog being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_from_url")]
		public string RebloggedFromUrl { get; set; }

		/// <summary>
		/// Title of a post being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_from_name")]
		public string RebloggedFromName { get; set; }

		/// <summary>
		/// URL of a post being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_from_title")]
		public string RebloggedFromTitle { get; set; }

		/// <summary>
		/// The id of an original post being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_root_id")]
		public long RebloggedRootId { get; set; }

		/// <summary>
		/// Root name of a post being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_root_url")]
		public string RebloggedRootUrl { get; set; }

		/// <summary>
		/// Title of the original post being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_root_name")]
		public string RebloggedRootName { get; set; }

		/// <summary>
		/// URL of the original post being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_root_title")]
		public string RebloggedRootTitle { get; set; }
	}
}
