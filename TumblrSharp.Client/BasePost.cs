using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Base class for post types.
	/// </summary>
    public class BasePost
    {
        /// <summary>
        /// The post type.
        /// </summary>
        [JsonConverter(typeof(EnumStringConverter))]
        [JsonProperty(PropertyName = "type")]
        public PostType Type { get; set; }

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
		/// The url of the post.
		/// </summary>
        [JsonProperty(PropertyName = "post_url")]
        public string Url { get; set; }

        /// <summary>
        /// slug
        /// </summary>
        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; set; }

		/// <summary>
		/// The date and time of the post (in local time).
		/// </summary>
		[JsonConverter(typeof(TimestampConverter))]
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The <see cref="PostCreationState"/>.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "state")]
        public PostCreationState State { get; set; }

        /// <summary>
        /// The <see cref="PostFormat"/>.
        /// </summary>
        [JsonConverter(typeof(EnumStringConverter))]
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
        /// Shorturl
        /// </summary>
        [JsonProperty(PropertyName = "short_url")]
        public string ShortUrl { get; set; }

        /// <summary>
        /// Summary
        /// </summary>
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }

        /// <summary>
        /// The number of notes (likes and reblogs) for the post.
        /// </summary>
        [JsonProperty(PropertyName = "note_count")]
        public long NotesCount { get; set; }

	    /// <summary>
	    /// The notes (likes and reblogs) for the post
	    /// </summary>
		[JsonProperty(PropertyName = "notes")]
        [JsonConverter(typeof(NoteConverter))]
		public List<BaseNote> Notes { get; set; }

		/// <summary>
		/// The url to the blog that is the source of the post, if any.
		/// </summary>
        [JsonProperty(PropertyName = "source_url", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceUrl { get; set; }

		/// <summary>
		/// The title of the blog which is the source of the post, if any.
		/// </summary>
        [JsonProperty(PropertyName = "source_title", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceTitle { get; set; }

		/// <summary>
		/// The number of posts in the response (can be used for pagination).
		/// </summary>
        [JsonProperty(PropertyName = "total_posts")]
        public int NumberOfPostsInResponse { get; set; }

		/// <summary>
		/// Indicates if the current user has already liked the post or not.
		/// </summary>
		[JsonProperty(PropertyName = "liked", NullValueHandling = NullValueHandling.Ignore)]
		public string Liked { get; set; }

		/// <summary>
		/// Indicates whether the post was created via mobile/email publishing.
		/// </summary>
		[JsonProperty(PropertyName = "mobile", NullValueHandling = NullValueHandling.Ignore)]
		public string IsMobile { get; set; }

		/// <summary>
		/// Indicates whether the post was created via the Tumblr bookmarklet.
		/// </summary>
		[JsonProperty(PropertyName = "bookmarklet", NullValueHandling = NullValueHandling.Ignore)]
		public string IsBookmarklet { get; set; }

        /// <summary>
        /// Reblog
        /// </summary>
        [JsonProperty(PropertyName = "reblog", NullValueHandling = NullValueHandling.Ignore)]
        public Reblog Reblog { get; set; }

		/// <summary>
		/// The id of a post being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_from_id", NullValueHandling = NullValueHandling.Ignore)]
		public long? RebloggedFromId { get; set; }

		/// <summary>
		/// Name of a blog being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_from_url", NullValueHandling = NullValueHandling.Ignore)]
		public string RebloggedFromUrl { get; set; }

		/// <summary>
		/// Title of a post being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_from_name", NullValueHandling = NullValueHandling.Ignore)]
		public string RebloggedFromName { get; set; }

		/// <summary>
		/// URL of a post being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_from_title", NullValueHandling = NullValueHandling.Ignore)]
		public string RebloggedFromTitle { get; set; }

		/// <summary>
		/// The id of an original post being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_root_id", NullValueHandling = NullValueHandling.Ignore)]
		public long? RebloggedRootId { get; set; }

		/// <summary>
		/// Root name of a post being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_root_url", NullValueHandling = NullValueHandling.Ignore)]
		public string RebloggedRootUrl { get; set; }

		/// <summary>
		/// Title of the original post being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_root_name", NullValueHandling = NullValueHandling.Ignore)]
		public string RebloggedRootName { get; set; }

		/// <summary>
		/// URL of the original post being reblogged.
		/// </summary>
		[JsonProperty(PropertyName = "reblogged_root_title", NullValueHandling = NullValueHandling.Ignore)]
		public string RebloggedRootTitle { get; set; }

        /// <summary>
		/// Trail
		/// </summary>
        [JsonProperty(PropertyName = "trail", NullValueHandling = NullValueHandling.Include)]
        [JsonConverter(typeof(TrailConverter))]
        public List<Trail> Trails { get; set; }
    }
}
