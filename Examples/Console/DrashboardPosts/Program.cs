using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardPosts
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

        public async Task<BasePost[]> GetDrashBoardPostAsync()
        {
            BasePost[] result;

            result = await client.GetDashboardPostsAsync(current);

            if (result.Count() > 0)
            current = result[result.Count() - 1].Id;

            return result;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Tumblr tumblr = new Tumblr();

            bool cancel = false;

            int k = 1;

            while (!cancel)
            {
                BasePost[] basePosts = tumblr.GetDrashBoardPostAsync().GetAwaiter().GetResult();                

                foreach (var basePost in basePosts)
                {
                    Console.WriteLine($"{k.ToString()}. Post from {basePost.BlogName} with ID {basePost.Id.ToString()}");

                    if (basePost.Trials.Count() > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"--- Post has {basePost.Trials.Count().ToString()} trial ---");
                        Console.WriteLine();
                    }

                    int i = 1;

                    foreach (var trial in basePost.Trials)
                    {
                        Console.WriteLine($"   {i}. trial from {trial.Blog.Name}");
                        Console.WriteLine($"   {trial.Content}");
                        Console.WriteLine();

                        i++;
                    }

                    Console.WriteLine();

                    k++;
                }

                Console.Write("next posts? (*/n)");

                var key = Console.ReadKey();

                Console.WriteLine();

                if (key.KeyChar == 'n')
                {
                    cancel = true;
                }
            }
        }
    }
}
