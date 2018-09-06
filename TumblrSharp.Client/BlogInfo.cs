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
        /// Title from askpage
        /// </summary>
        [JsonProperty(PropertyName = "ask_page_title")]
        public string AskPageTitle { get; set; }

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

        /// <summary>
        /// Blog is marked as adult
        /// </summary>
        [JsonProperty(PropertyName = "is_adult")]
        public bool IsAdult { get; set; }

        /// <summary>
        /// IsOptoutAds
        /// </summary>
        [JsonProperty(PropertyName = "is_optout_ads")]
        public bool IsOptoutAds { get; set; }

        /// <summary>
        /// Reply conditions
        /// </summary>
        [JsonProperty(PropertyName = "reply_conditions")]
        public long ReplyConditions { get; set; }

        /// <summary>
        /// Share likes
        /// </summary>
        [JsonProperty(PropertyName = "share_likes")]
        public bool ShareLikes { get; set; }

        /// <summary>
        /// Title from submissionpage
        /// </summary>
        [JsonProperty(PropertyName = "submission_page_title")]
        public string SubmissionPageTitle { get; set; }

        /// <summary>
        /// Subscribed
        /// </summary>
        [JsonProperty(PropertyName = "subscribed")]
        public bool Subscribed { get; set; }
    }
}
