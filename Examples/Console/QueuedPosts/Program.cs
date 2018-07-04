using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueuedPosts
{
    public class Tumblr
    {
        private TumblrClient client = null;

        private readonly string CONSUMER_KEY = "xxx";
        private readonly string CONSUMER_SECRET = "xxx";
        private readonly string OAUTH_TOKEN = "xxx";
        private readonly string OAUTH_TOKEN_SECRET = "xxx";

        private long current = 0;

        public Tumblr()
        {
            if (CONSUMER_KEY == "xxx")
            {
                Console.WriteLine("Change in sourcecode the consumerKey, etc...!");
                Console.WriteLine();

                throw new Exception();
            }

            this.client = new TumblrClientFactory().Create<TumblrClient>(CONSUMER_KEY, CONSUMER_SECRET, new Token(OAUTH_TOKEN, OAUTH_TOKEN_SECRET));
        }

        public async Task<List<string>> GetBlog()
        {
            List<string> result = new List<string>();

            UserInfo userInfo = await client.GetUserInfoAsync();

            foreach (var blog in userInfo.Blogs)
            {
                result.Add(blog.Name);
            }

            return result;
        }

        public async Task<Int32> GetCountOfQueued(string blog)
        {
            Int32 result = 0;

            BasePost[] test;

            test = await client.GetQueuedPostsAsync(blog);

            while (test.Count() == 20)
            {
                result = result + 20;

                test = await client.GetQueuedPostsAsync(blog);
            }

            result = result + test.Count();

            return result;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Tumblr tumblr = new Tumblr();

            var blogs = tumblr.GetBlog().GetAwaiter().GetResult();

            Console.WriteLine("Your blogs:");
            Console.WriteLine("");

            for (int i = 0; i < blogs.Count(); i++)
            {
                Console.WriteLine($"   {i}. {blogs[i]} ");
            }

            Console.WriteLine("");

            Console.Write("Please select a blog: ");
            var blogIdx = Convert.ToInt32(Console.ReadLine());

            var count = tumblr.GetCountOfQueued(blogs[blogIdx]).GetAwaiter().GetResult();

            Console.WriteLine("");
            Console.WriteLine($"You have {count} posts in Queued");

            Console.ReadLine();

        }
    }
}
