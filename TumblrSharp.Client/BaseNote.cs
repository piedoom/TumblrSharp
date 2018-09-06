﻿using System;
using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Base class for post notes.
	/// </summary>
	public class BaseNote
	{
		/// <summary>
		/// The type of note applied to the post
		/// </summary>
		[JsonConverter(typeof(EnumStringConverter))]
		[JsonProperty(PropertyName = "type")]
		public NoteType Type { get; set; }

		/// <summary>
		/// The date and time of the post (in local time).
		/// </summary>
		[JsonConverter(typeof(TimestampConverter))]
		[JsonProperty(PropertyName = "timestamp")]
		public DateTime Timestamp { get; set; }

		/// <summary>
		/// The short name used to uniquely identify a blog.
		/// </summary>
		[JsonProperty(PropertyName = "blog_name")]
		public string BlogName { get; set; }
		
		/// <summary>
		/// The full domain used to uniquely identify a blog.
		/// </summary>
		[JsonProperty(PropertyName = "blog_uuid")]
		public string BlogUuid { get; set; }
		
		/// <summary>
		/// The base URL of the blog that left the note
		/// </summary>
		[JsonProperty(PropertyName = "blog_url")]
		public string BlogUrl { get; set; }

		/// <summary>
		/// Indicates if the current user is following 
		/// the blog who left the post
		/// </summary>
		[JsonProperty(PropertyName = "followed")]
		public bool Followed { get; set; }
		
		/// <summary>
		/// The avatar shape used by the blog that
		/// left the note
		/// </summary>
		[JsonProperty(PropertyName = "avatar_shape")]
		public AvatarShape AvatarShape { get; set; }

		/// <summary>
		/// The text of the note if it is a reply
		/// </summary>
		[JsonProperty(PropertyName = "reply_text")]
		public string ReplyText { get; set; }

		/// <summary>
		/// The post ID of the reblogged post
		/// if the note is a reblog
		/// </summary>
		[JsonProperty(PropertyName = "post_id")]
		public string PostId { get; set; }

		/// <summary>
		/// The parent blog of the reblogged post
		/// if the note is a reblog
		/// </summary>
		[JsonProperty(PropertyName = "reblog_parent_blog_name")]
		public string ReblogParentBlogName { get; set; }
	}
}
