using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;

namespace Testing
{
    /// <summary>
    /// Tumblr Service
    /// </summary>
    public class MyTumblrService : IMyTumblrService
    {
        private TumblrClient _tc;

        /// <summary>
        /// create Tumblrservice
        /// </summary>
        /// <param name="clientFactory">IHttpClientFactory to create internal HttpClient</param>
        public MyTumblrService(IHttpClientFactory clientFactory)
        {
            _tc = new TumblrClientFactory().Create<TumblrClient>(clientFactory, Settings.CONSUMER_KEY, Settings.CONSUMER_SECRET, new Token(Settings.OAUTH_TOKEN, Settings.OAUTH_TOKEN_SECRET));
        }

        /// <summary>
        /// get follower for the first blog
        /// </summary>
        /// <returns>a string</returns>
        public async Task<string> GetFollowerCount()
        {
            UserInfo user = await _tc.GetUserInfoAsync();

            Followers followers = await _tc.GetFollowersAsync(user.Blogs.First().Name);

            string result = $"Your blog {user.Blogs.First().Name} have {followers.Count} Follower";

            return result;
        }
    }
}
