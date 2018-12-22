using ConsoleBasics;
using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reblog_Queue
{
    public class Tumblr : TumblrBase
    {
        public async Task<BasePost> GetDrashBoardPostAsync()
        {
            BasePost[] result;

            result = await client.GetDashboardPostsAsync(0, 0, 1, PostType.Photo);
            
            return result[0];
        }

        public async void Reblog(string blogName, BasePost basePost)
        {
            await client.ReblogAsync(blogName, basePost.Id, basePost.ReblogKey, PostCreationState.Queue);
        }

        public async Task<List<string>> GetBlogs()
        {
            List<string> result = new List<string>();

            UserInfo userInfo = await client.GetUserInfoAsync();

            foreach (var blog in userInfo.Blogs)
            {
                result.Add(blog.Name);
            } 

            return result;
        }

        public void Reblog(string blogName, BasePost basePost, DateTime dateTime)
        {
            client.ReblogAsync(blogName, basePost.Id, basePost.ReblogKey, PostCreationState.Queue, dateTime);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Tumblr tumblr = new Tumblr();

            BasePost basePost = tumblr.GetDrashBoardPostAsync().GetAwaiter().GetResult();

            Console.WriteLine($"Post with ID {basePost.Id} found");
            Console.WriteLine();

            Console.WriteLine("Select a blog:");

            int idx = 0;

            var blogs = tumblr.GetBlogs().GetAwaiter().GetResult();

            foreach (var blog in blogs)
            {
                Console.WriteLine($"   {idx} {blog}");
                idx++;
            }

            Console.WriteLine();
            Console.Write("Input: ");
            idx = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            tumblr.Reblog(blogs[idx], basePost);

            Console.WriteLine("Post with standard date rebloged in queue");

            Console.WriteLine();
            Console.Write("Input a date: ");
            DateTime dateTime = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine();

            tumblr.Reblog(blogs[idx], basePost, dateTime);

            Console.WriteLine($"Post with {dateTime} rebloged in queue");

            System.Diagnostics.Process.Start($"https://www.tumblr.com/blog/{blogs[idx]}/queue");

            Console.ReadLine();
        }
    }
}
