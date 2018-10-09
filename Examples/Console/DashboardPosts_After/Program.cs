using ConsoleBasics;
using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardPosts_After
{
    class Program
    {
        public class Tumblr : TumblrBase
        {
            private long current = 0;

            public async Task<List<BasePost>> GetDashBoardPostAsync()
            {
                List<BasePost> result = new List<BasePost>();

                if (current == 0)
                {
                    var basePosts = await client.GetDashboardPostsAsync(current, 0, 1);

                    foreach (var item in basePosts)
                    {
                        result.Add(item);
                    }
                }
                else
                {
                    bool more = true;

                    long k = 0;

                    while (more)
                    {
                        var nextResults = await client.GetDashboardPostsAsync(current, DashboardOption.After, k);

                        more = (nextResults.Count() == 20);

                        foreach (var item in nextResults)
                        {
                            result.Add(item);
                        }

                        k = k + 20;
                    }
                }

                if (result.Count() > 0 && current == 0)
                    current = result[0].Id;

                return result;
            }

        }

        static void Main(string[] args)
        {
            Tumblr tumblr = new Tumblr();

            bool cancel = false;

            // the last Posts of Dashboard

            Console.WriteLine("The last Post from Dashboard");
            Console.WriteLine();

            List <BasePost> basePosts = tumblr.GetDashBoardPostAsync().GetAwaiter().GetResult();

            foreach (var basePost in basePosts)
            {
                Console.WriteLine($"Post from {basePost.BlogName} with ID {basePost.Id.ToString()}");

                Console.WriteLine();
            }

            Console.WriteLine("++++ newer posts ++++");
            Console.WriteLine();

            int k = 1;

            while (!cancel)
            {
                basePosts = tumblr.GetDashBoardPostAsync().GetAwaiter().GetResult();

                if (basePosts.Count() < 1)
                {
                    Console.WriteLine($"not found");

                    Console.WriteLine();
                }

                foreach (var basePost in basePosts)
                {
                    Console.WriteLine($"{k.ToString()}. Post from {basePost.BlogName} with ID {basePost.Id.ToString()}");

                    Console.WriteLine();

                    k++;
                }

                Console.Write("search newer posts? (*/n) ");
                var key = Console.ReadKey();
                Console.WriteLine();
                Console.WriteLine();

                if (key.KeyChar == 'n')
                {
                    cancel = true;
                }
                else
                {
                    k = 1;
                }
            }
        }
    }
}
