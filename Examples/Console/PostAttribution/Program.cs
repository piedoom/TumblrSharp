using ConsoleBasics;
using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostAttribution
{
    public class Tumblr : TumblrBase
    {
        private long current = 0;
         
        public BasePost GetGetPostwithNoteandPostAttribution()
        {
            BasePost result = null;

            bool found = false;

            long k = 1;

            while (found == false)
            {
                Console.Write("\rRead the next {0}th 20 posts...", k.ToString());

                BasePost[] basePosts  = client.GetDashboardPostsAsync(current, DashboardOption.Before, 0, 20, PostType.All, false, true).GetAwaiter().GetResult();

                if (basePosts.Count() > 0)
                    current = basePosts[basePosts.Count() - 1].Id;

                foreach (var basepost in basePosts)
                {
                    if (basepost.NotesCount > 0)
                    {
                        foreach (var note in basepost.Notes)
                        {
                            if (note.Type == NoteType.Post_attribution)
                            {
                                result = basepost;
                                found = true;

                                break;
                            }
                        }
                    }

                    if (found == true)
                    {
                        break;
                    }
                }

                k++;
            }

            Console.WriteLine();
            Console.WriteLine();

            return result;
        }        
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Search post with notes, this note have a type post_attributation");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine();

            Tumblr tumblr = new Tumblr();

            BasePost result = tumblr.GetGetPostwithNoteandPostAttribution();

            if (result == null)
            {
                Console.WriteLine("Not found post");
            }
            else
            {
                Console.WriteLine($"Post found: {result.Id}");
                foreach (var note in result.Notes)
                {
                    if (note.Type == NoteType.Post_attribution)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"  Note from {note.BlogName}");

                        PostAttributionNote postAttributionNote = (PostAttributionNote)note;
                        Console.WriteLine($"    type:     {postAttributionNote.Post_attribution_type}");
                        Console.WriteLine($"    typename: {postAttributionNote.Post_attribution_type_name}");
                        Console.WriteLine($"    url:      {postAttributionNote.Photo_url}");
                        Console.WriteLine($"    width:    {postAttributionNote.Photo_width}");
                        Console.WriteLine($"    height:   {postAttributionNote.Photo_height}");
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
