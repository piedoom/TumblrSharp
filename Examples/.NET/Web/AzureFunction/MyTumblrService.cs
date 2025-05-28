using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzureTest
{
    public class MyTumblrService : IMyTumblrService
    {
        private readonly TumblrClient _tc;

        public MyTumblrService(IHttpClientFactory clientFactory)
        {
            _tc = new TumblrClientFactory().Create<TumblrClient>(clientFactory, Settings.CONSUMER_KEY, Settings.CONSUMER_SECRET, new Token(Settings.OAUTH_TOKEN, Settings.OAUTH_TOKEN_SECRET));
        }

        public async Task<string> GetUser()
        {
            UserInfo user = await _tc.GetUserInfoAsync();

            StringBuilder sb = new StringBuilder();

            AddHtmlHeader(sb, "User");

            sb.AppendLine($"Willkommen {user.Name} du hast folgende Blogs:");

            sb.AppendLine("<ul>");

            foreach (var blog in user.Blogs)
            {
                sb.AppendLine(" <li>");
                sb.Append(@"<a href=""Blogs?name=");
                sb.Append($"{blog.Name}");
                sb.Append(@""">"); 
                sb.Append($"{blog.Name}");
                sb.Append(@"</a>");
                sb.AppendLine(" </li>");
            }

            sb.AppendLine("</ul>");

            AddEndHTML(sb);

            string result = sb.ToString();

            return result;
        }


        public async Task<string> GetBlog(string blogName)
        {
            Followers followers = await _tc.GetFollowersAsync(blogName);
            BlogInfo blogInfo = await _tc.GetBlogInfoAsync(blogName);

            StringBuilder sb = new StringBuilder();

            AddHtmlHeader(sb, "Blog");

            sb.AppendLine($"Your blog {blogName} with Title: {blogInfo.Title} have:");
            sb.AppendLine($"<p>{followers.Count} Follower</p>");
            sb.AppendLine($"<p>{blogInfo.PostsCount} Posts</p>");
            sb.AppendLine($"<p>{blogInfo.LikesCount} Likes</p>");

            AddEndHTML(sb);

            string result = sb.ToString();

            return result;
        }

        private void AddHtmlHeader(StringBuilder sb, string pageName)
        {
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine($"<title> Azure Funktiontest {pageName}</title>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
        }

        private void AddEndHTML(StringBuilder sb)
        {
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
        }
    }
}
