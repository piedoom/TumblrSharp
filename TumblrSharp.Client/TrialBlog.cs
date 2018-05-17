using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
    public class TrialBlog
    {
        [JsonProperty(PropertyName ="name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "theme")]
        public TrialTheme Theme { get; set; }

        [JsonProperty(PropertyName = "share_likes")]
        public bool ShareLikes { get; set; }

        [JsonProperty(PropertyName = "share_following")]
        public bool ShareFollowing { get; set; }

        [JsonProperty(PropertyName = "can_be_followed")]
        public bool CanBeFollowed { get; set; }
    }
}