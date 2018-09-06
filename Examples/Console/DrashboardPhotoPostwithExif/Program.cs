using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardPhotoPostwithExif
{
    class Program
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

            public async Task<PhotoPost> GetDrashBoardPostAsync()
            {
                PhotoPost result = null;

                bool PhotoPostwithExifGefunden = false;

                while (!PhotoPostwithExifGefunden)
                {
                    BasePost[] BasePosts = await client.GetDashboardPostsAsync(current, 0, 20, PostType.Photo);

                    foreach (var basePost in BasePosts)
                    {
                        if (basePost.Type == PostType.Photo)
                        {
                            PhotoPost photoPost = basePost as PhotoPost;

                            Console.Clear();
                            Console.WriteLine($"Search {photoPost.Id}");

                            if (photoPost.Photo.Exif != null)
                            {
                                PhotoPostwithExifGefunden = true;
                                result = photoPost;
                                break;
                            }
                        }
                    }

                    if (BasePosts.Count() > 0)
                        current = BasePosts[BasePosts.Count() - 1].Id;

                }

                return result;
            }
        }

        static void Main(string[] args)
        {
            Tumblr tumblr = new Tumblr();

            PhotoPost photoPost = tumblr.GetDrashBoardPostAsync().GetAwaiter().GetResult();

            Console.WriteLine(photoPost.Photo.Caption);
            Console.WriteLine(photoPost.Photo.OriginalSize.ImageUrl);
            
            Console.WriteLine("Camera: " + photoPost.Photo.Exif.Camera);
            Console.WriteLine("Exposure: " + photoPost.Photo.Exif.Exposure);
            Console.WriteLine("Aperture: " + photoPost.Photo.Exif.Aperture);
            Console.WriteLine("Iso: " + photoPost.Photo.Exif.ISO);
            Console.WriteLine("FocalLength: " + photoPost.Photo.Exif.FocalLength);

            Console.ReadKey();
        }
    }
}
