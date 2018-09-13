using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using System;

namespace Followers
{
    class Program
    {
        // your consumer- and accessToken 
        private static string CONSUMER_KEY = "xxx";
        private static string CONSUMER_SECRET = "xxx";
        private static string OAUTH_TOKEN = "xxx";
        private static string OAUTH_TOKEN_SECRET = "xxx";

        static void Main(string[] args)
        {
            Console.WriteLine("Follower sample!");

            // create TumblrClient
            TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(CONSUMER_KEY, CONSUMER_SECRET, new Token(OAUTH_TOKEN, OAUTH_TOKEN_SECRET));

            // get your blogs
            var userInfo = tumblrClient.GetUserInfoAsync().GetAwaiter().GetResult();

            // display blogs
            Console.WriteLine();
            Console.WriteLine("Blogs:");

            int idx = -1;
            foreach (var blog in userInfo.Blogs)
            {
                idx++;
                Console.WriteLine(idx.ToString() + ". " + blog.Name);
            }

            //select a blog
            Console.WriteLine();
            Console.Write("select a blog (0-" + idx.ToString() + "): ");
            string blogName = userInfo.Blogs[Convert.ToUInt32(Console.ReadLine())].Name;

            // get first 20 follower
            BlogBase[] blogs = tumblrClient.GetFollowersAsync(blogName).GetAwaiter().GetResult().Result;

            // display follower
            Console.WriteLine();
            Console.WriteLine("first 20th follower:");
            foreach (var blog in blogs)
            {
                Console.WriteLine(blog.Name);
            }

            Console.ReadLine();
        }
    }
}
}
