using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
    /// <summary>
    /// post object from <see cref="Trail" />
    /// </summary>
    public class TrailPost
    {
        /// <summary>
        /// id from post
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// compare trailposts
        /// </summary>
        /// <param name="obj">to compare object</param>
        /// <returns>true for obj is equal</returns>
        public override bool Equals(object obj)
        {
            return obj is TrailPost post &&
                   Id == post.Id;
        }

        /// <summary>
        /// hash code for a trailpost
        /// </summary>
        /// <returns>hashcode</returns>
        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}