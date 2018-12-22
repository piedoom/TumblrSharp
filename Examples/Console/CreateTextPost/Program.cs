using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.OAuth;
using DontPanic.TumblrSharp.Client;
using System;
using System.Threading.Tasks;
using ConsoleBasics;

namespace CreateTextPost
{
    public class Tumblr : TumblrBase
    {
        public async Task<PostCreationInfo> Post(string text)
        {
            //replace blogName with your blogname
            string blogName = "Your blogName";

            var test = await client.CreatePostAsync(blogName, PostData.CreateText(text));

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
