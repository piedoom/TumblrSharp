using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.OAuth;
using DontPanic.TumblrSharp.Client;
using System;
using System.Threading.Tasks;

namespace CreateTextPost
{
    public class Tumblr
    {
        private TumblrClient client = null;

        private readonly string CONSUMER_KEY = "xxx";
        private readonly string CONSUMER_SECRET = "xxx";
        private readonly string OAUTH_TOKEN = "xxx";
        private readonly string OAUTH_TOKEN_SECRET = "xxx";

        public Tumblr()
        {
            this.client = new TumblrClientFactory().Create<TumblrClient>(CONSUMER_KEY, CONSUMER_SECRET, new Token(OAUTH_TOKEN, OAUTH_TOKEN_SECRET));
        }

        public async Task<PostCreationInfo> Post(string text)
        {
            var test = await client.CreatePostAsync("schnuppix", PostData.CreateText(text)).ConfigureAwait(false);

            return test;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Tumblr tb = new Tumblr();

            Console.WriteLine("Text posted:");
            Console.WriteLine();

            var text = Console.ReadLine();

            var test = tb.Post(text).GetAwaiter().GetResult();

            Console.WriteLine("Post ID: " + test.PostId.ToString());

            Console.ReadKey();
        }
    }
}
