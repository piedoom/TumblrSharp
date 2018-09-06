﻿using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
    /// <summary>
    /// The blog from Trail
    /// </summary>
    public class TrailBlog
    {
        /// <summary>
        /// the name of the blog
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// is Blog active
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }

        /// <summary>
        /// theme of the blog
        /// </summary>
        [JsonConverter(typeof(TrailThemeConverter))]
        [JsonProperty(PropertyName = "theme")]
        public TrailTheme Theme { get; set; }

        /// <summary>
        /// share likes
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty(PropertyName = "share_likes")]
        public bool ShareLikes { get; set; }

        /// <summary>
        /// share following
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty(PropertyName = "share_following")]
        public bool ShareFollowing { get; set; }

        /// <summary>
        /// can be followed
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty(PropertyName = "can_be_followed")]
        public bool CanBeFollowed { get; set; }
    }
}