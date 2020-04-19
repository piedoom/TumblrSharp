using Newtonsoft.Json;
using System.Collections.Generic;

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

        /// <summary>
        /// Compare a trailblog with another
        /// </summary>
        /// <param name="obj">Object to be equals</param>
        /// <returns>bool</returns>
        public override bool Equals(object obj)
        {
            return obj is TrailBlog blog &&
                   Name == blog.Name &&
                   Active == blog.Active &&
                   Theme.Equals(blog.Theme) &&
                   ShareLikes == blog.ShareLikes &&
                   ShareFollowing == blog.ShareFollowing &&
                   CanBeFollowed == blog.CanBeFollowed;
        }

        /// <summary>
        /// return a hash code
        /// </summary>
        /// <returns>hashcode as <cref>int</cref></returns>
        public override int GetHashCode()
        {
            var hashCode = 1185437142;
            hashCode = hashCode * -1521134295 + Name.GetHashCode();
            hashCode = hashCode * -1521134295 + Active.GetHashCode();
            hashCode = hashCode * -1521134295 + Theme.GetHashCode();
            hashCode = hashCode * -1521134295 + ShareLikes.GetHashCode();
            hashCode = hashCode * -1521134295 + ShareFollowing.GetHashCode();
            hashCode = hashCode * -1521134295 + CanBeFollowed.GetHashCode();
            return hashCode;
        }
    }
}