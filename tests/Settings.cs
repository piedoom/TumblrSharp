using DontPanic.TumblrSharp.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTumblrSharp
{
    internal static class Settings
    {
        // These tokens are for testing purposes only. They are not valid and will not work.

        public static string consumerKey = Environment.GetEnvironmentVariable("TUMBLR_CONSUMER_KEY") ?? "***";
        public static string consumerSecret = Environment.GetEnvironmentVariable("TUMBLR_CONSUMER_SECRET") ?? "***";

        public static string accessKey = Environment.GetEnvironmentVariable("TUMBLR_ACCESS_KEY") ?? "***";
        public static string accessSecret = Environment.GetEnvironmentVariable("TUMBLR_ACCESS_SECRET") ?? "***";

        public static Token AccessToken => new Token(accessKey, accessSecret);

        public static string callbackUrl = "https://www.test.de";
    }
}
