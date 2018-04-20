using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
    internal class UserInfoResponse
    {
        [JsonProperty(PropertyName = "user")]
        public UserInfo User { get; set; }
    }
}
