using Newtonsoft.Json;
using System.Collections.Generic;

namespace DontPanic.TumblrSharp.Client
{
    /// <summary>
    /// Trail
    /// </summary>
    public class Trail
    {
        /// <summary>
        /// the blog
        /// </summary>
        [JsonProperty(PropertyName = "blog")]
        public TrailBlog Blog { get; set; }

        /// <summary>
        /// the post that trail
        /// </summary>
        [JsonProperty(PropertyName ="post")]
        public TrailPost Post { get; set; }

        /// <summary>
        /// ContentRaw
        /// </summary>
        [JsonProperty(PropertyName = "content_raw")]
        public string ContentRaw { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Trail trail &&
                   Blog.Equals(trail.Blog) &&
                   Post.Equals(trail.Post) &&
                   ContentRaw == trail.ContentRaw &&
                   Content == trail.Content;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 326892894;
            hashCode = hashCode * -1521134295 + EqualityComparer<TrailBlog>.Default.GetHashCode(Blog);
            hashCode = hashCode * -1521134295 + EqualityComparer<TrailPost>.Default.GetHashCode(Post);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ContentRaw);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Content);
            return hashCode;
        }
    }
}