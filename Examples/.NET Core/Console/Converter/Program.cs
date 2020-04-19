using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            TextPost textPost = new TextPost
            {
                BlogName = "NewTumblrSharp",

                Id = 1,

                Trails = new List<Trail>
                {
                    new Trail()
                    {
                        Content = "test content",

                        Blog = new TrailBlog()
                        {
                            Active = true,
                            Name = "TestBlog",
                            Theme = new TrailTheme()
                            {
                                AvatarShape = AvatarShape.AvatarCircle,
                                ShowTitle = true
                            }
                        }
                    },

                    new Trail()
                    {
                        Content = "test 2 content"
                    }
                },

                Notes = new List<BaseNote>
                {
                    new BaseNote()
                    {
                        PostId = "1",
                        ReplyText = "This a note",
                        Type = NoteType.Reblog
                    },

                    new PostAttributionNote()
                    {
                        PostId = "2",
                        Post_attribution_type = "test",
                        Type = NoteType.Post_attribution
                    }
                },

                NotesCount = 2
            };

            string json = JsonConvert.SerializeObject(textPost, Formatting.Indented);

            Console.WriteLine(json);

            Console.ReadLine();
        }
    }
}
