using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Testing
{
    [TestClass]
    public class JsonConverter
    {
        // This ConsumerToken and AccessToken is only for testing!

        private readonly string _consumerKey = "hGSKqgb24RJBnWkodL5GTFIeadgyOnWl0qsXi7APRC76HELnrE";
        private readonly string _consumerSecret = "jdNWSSbG7bZ8tYJcYzmyfH33o5cq7ihmJeWMVntB3pUHNptqn3";

        private readonly string _accessKey = "F1G7BF1JW4f1VKJ93xJSi7D66yZKN3Uj0bArn5i5riwVEnMHuU";
        private readonly string _accessSecret = "O977YH42yg98IsS9BAk80r5e5grYQDY9HauVmgf0aEmceZ2UTz";

        [TestMethod]
        public void NoteConverter_Theorie()
        {
            const string resultStr = "{\r\n  \"title\": null,\r\n  \"body\": \"testbody\",\r\n  \"type\": \"All\",\r\n  \"blog_name\": \"TestBlog\"," +
                "\r\n  \"id\": 12456789,\r\n  \"post_url\": null,\r\n  \"slug\": null,\r\n  \"timestamp\": -62135596800.0,\r\n  \"state\": \"published\"," +
                "\r\n  \"format\": \"Html\",\r\n  \"reblog_key\": null,\r\n  \"tags\": null,\r\n  \"short_url\": null,\r\n  \"summary\": null," +
                "\r\n  \"note_count\": 0,\r\n  \"notes\": [\r\n    {\r\n      \"type\": \"Like\",\r\n      \"timestamp\": -62135596800.0," +
                "\r\n      \"blog_name\": \"OtherBlock\",\r\n      \"blog_uuid\": null,\r\n      \"blog_url\": null,\r\n      \"followed\": false," +
                "\r\n      \"avatar_shape\": \"circle\",\r\n      \"reply_text\": null,\r\n      \"post_id\": null,\r\n      \"reblog_parent_blog_name\": null\r\n    }\r\n  ]," +
                "\r\n  \"source_url\": null,\r\n  \"source_title\": null,\r\n  \"total_posts\": 0,\r\n  \"liked\": null,\r\n  \"mobile\": null," +
                "\r\n  \"bookmarklet\": null,\r\n  \"reblog\": null,\r\n  \"reblogged_from_id\": 0,\r\n  \"reblogged_from_url\": null," +
                "\r\n  \"reblogged_from_name\": null,\r\n  \"reblogged_from_title\": null,\r\n  \"reblogged_root_id\": 0,\r\n  \"reblogged_root_url\": null," +
                "\r\n  \"reblogged_root_name\": null,\r\n  \"reblogged_root_title\": null,\r\n  \"trail\": []\r\n}";

            TextPost basicTextPost = new TextPost
            {
                BlogName = "TestBlog",
                Body = "testbody",
                Id = 12456789,
                Format = PostFormat.Html,
                Trails = new List<Trail>()
            };

            basicTextPost.Notes = new List<BaseNote>
            {
                new BaseNote()
                {
                    BlogName = "OtherBlock",
                    Type = NoteType.Like,
                    AvatarShape = AvatarShape.Circle
                }
            };

            // convert post
            string json = JsonConvert.SerializeObject(basicTextPost, Formatting.Indented);

            Assert.AreEqual(resultStr, json);

            TextPost tp = JsonConvert.DeserializeObject<TextPost>(json);

            Assert.AreEqual(basicTextPost.Notes[0].BlogName, tp.Notes[0].BlogName);
        }

        [TestMethod]
        public async Task NoteConverter()
        {
            TumblrClient tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            // find a post with notes
            PhotoPost notePost = null;
            
            bool findPostWithNotes = false;

            long currentID = 0;

            while (findPostWithNotes == false)
            {
                BasePost[] basePosts = await tc.GetDashboardPostsAsync(currentID, 0, 1, PostType.Photo, false, true);

                for (int i = 0; i < basePosts.Count(); i++)
                {
                    PhotoPost basePost = basePosts[i] as PhotoPost;

                    if (basePost.Notes?.Count() > 0)
                    {
                        findPostWithNotes = true;
                        notePost = basePost;
                        break;
                    }
                    currentID = basePost.Id;
                }
            }

            // convert post
            string json = JsonConvert.SerializeObject(notePost, Formatting.Indented);

            // deserialize post
            PhotoPost jsonPost = JsonConvert.DeserializeObject<PhotoPost>(json);

            //testing
            for (int i = 0; i < notePost.Notes.Count(); i++)
            {
                BaseNote baseNote = notePost.Notes[i];
                BaseNote jsonNote = jsonPost.Notes[i];

                Assert.AreEqual(baseNote.AvatarShape, jsonNote.AvatarShape);
                Assert.AreEqual(baseNote.BlogName, jsonNote.BlogName);
                Assert.AreEqual(baseNote.BlogUrl, jsonNote.BlogUrl);
                Assert.AreEqual(baseNote.BlogUuid, jsonNote.BlogUuid);
                Assert.AreEqual(baseNote.Followed, jsonNote.Followed);
                Assert.AreEqual(baseNote.PostId, jsonNote.PostId);
                Assert.AreEqual(baseNote.ReblogParentBlogName, jsonNote.ReblogParentBlogName);
                Assert.AreEqual(baseNote.ReplyText, jsonNote.ReplyText);
                Assert.AreEqual(baseNote.Timestamp, jsonNote.Timestamp);
                Assert.AreEqual(baseNote.Type, jsonNote.Type);
            }            
        }

        [TestMethod]
        public async Task TrailsConverter()
        {
            TumblrClient tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            // find a post with trials
            BasePost post = null;

            bool findPostWithTrials = false;

            long currentID = 0;

            while (findPostWithTrials == false)
            {
                BasePost[] basePosts = await tc.GetDashboardPostsAsync(currentID, 0, 20, PostType.All, false, true);

                for (int i = 0; i < 20; i++)
                {
                    BasePost basePost = basePosts[i];

                    if (basePost.Trails?.Count() > 0)
                    {
                        findPostWithTrials = true;
                        post = basePost;
                    }

                    if (i == 19)
                    {
                        currentID = basePost.Id;
                    }
                }
            }

            // convert post
            string json = JsonConvert.SerializeObject(post, Formatting.Indented);

            // deserialize post
            BasePost jsonPost = JsonConvert.DeserializeObject<BasePost>(json);

            //testing
            for (int i = 0; i < post.Trails.Count(); i++)
            {
                Trail baseTrail = post.Trails[i];
                Trail jsonTrail = jsonPost.Trails[i];

                Assert.AreEqual(baseTrail.Content, jsonTrail.Content);
                Assert.AreEqual(baseTrail.ContentRaw, jsonTrail.ContentRaw);

                Assert.AreEqual(baseTrail.Post.Id, jsonTrail.Post.Id);

                Assert.AreEqual(baseTrail.Blog.Active, jsonTrail.Blog.Active);
                Assert.AreEqual(baseTrail.Blog.CanBeFollowed, jsonTrail.Blog.CanBeFollowed);
                Assert.AreEqual(baseTrail.Blog.Name, jsonTrail.Blog.Name);
                Assert.AreEqual(baseTrail.Blog.ShareFollowing, jsonTrail.Blog.ShareFollowing);
                Assert.AreEqual(baseTrail.Blog.ShareLikes, jsonTrail.Blog.ShareLikes);

                Assert.AreEqual(baseTrail.Blog.Theme.AvatarShape, jsonTrail.Blog.Theme.AvatarShape);
                Assert.AreEqual(baseTrail.Blog.Theme.BackgroundColor, jsonTrail.Blog.Theme.BackgroundColor);
                Assert.AreEqual(baseTrail.Blog.Theme.BodyFont, jsonTrail.Blog.Theme.BodyFont);
                Assert.AreEqual(baseTrail.Blog.Theme.HeaderBounds, jsonTrail.Blog.Theme.HeaderBounds);
                Assert.AreEqual(baseTrail.Blog.Theme.HeaderFocusHeight, jsonTrail.Blog.Theme.HeaderFocusHeight);
                Assert.AreEqual(baseTrail.Blog.Theme.HeaderFocusWidth, jsonTrail.Blog.Theme.HeaderFocusWidth);
                Assert.AreEqual(baseTrail.Blog.Theme.HeaderFullHeight, jsonTrail.Blog.Theme.HeaderFullHeight);
                Assert.AreEqual(baseTrail.Blog.Theme.HeaderFullWidth, jsonTrail.Blog.Theme.HeaderFullWidth);
                Assert.AreEqual(baseTrail.Blog.Theme.HeaderImage, jsonTrail.Blog.Theme.HeaderImage);
                Assert.AreEqual(baseTrail.Blog.Theme.HeaderImageFocused, jsonTrail.Blog.Theme.HeaderImageFocused);
                Assert.AreEqual(baseTrail.Blog.Theme.HeaderImageScaled, jsonTrail.Blog.Theme.HeaderImageScaled);
                Assert.AreEqual(baseTrail.Blog.Theme.HeaderStretch, jsonTrail.Blog.Theme.HeaderStretch);
                Assert.AreEqual(baseTrail.Blog.Theme.LinkColor, jsonTrail.Blog.Theme.LinkColor);
                Assert.AreEqual(baseTrail.Blog.Theme.ShowAvatar, jsonTrail.Blog.Theme.ShowAvatar);
                Assert.AreEqual(baseTrail.Blog.Theme.ShowDescription, jsonTrail.Blog.Theme.ShowDescription);
                Assert.AreEqual(baseTrail.Blog.Theme.ShowHeaderImage, jsonTrail.Blog.Theme.ShowHeaderImage);
                Assert.AreEqual(baseTrail.Blog.Theme.ShowTitle, jsonTrail.Blog.Theme.ShowTitle);
                Assert.AreEqual(baseTrail.Blog.Theme.TitleColor, jsonTrail.Blog.Theme.TitleColor);
                Assert.AreEqual(baseTrail.Blog.Theme.TitleFont, jsonTrail.Blog.Theme.TitleFont);
                Assert.AreEqual(baseTrail.Blog.Theme.TitleFontWeight, jsonTrail.Blog.Theme.TitleFontWeight);
            }
        }

        [TestMethod]
        public void TumblrErrorConverter()
        {
            TumblrError te = new TumblrError
            {
                Code = 0,
                Title = "Not Found",
                Detail = "Da ist der Wurm drin. Versuche es noch mal."
            };

            string teStr = "{\r\n  \"title\": \"Not Found\",\r\n  \"code\": 0,\r\n  \"detail\": \"Da ist der Wurm drin. Versuche es noch mal.\"\r\n}";

            // test serialize
            string json = JsonConvert.SerializeObject(te, Formatting.Indented);

            Assert.AreEqual(teStr, json);

            // test deserialize
            TumblrError jsonTumblrError = JsonConvert.DeserializeObject<TumblrError>(teStr);

            Assert.AreEqual(te.Code, jsonTumblrError.Code);
            Assert.AreEqual(te.Detail, jsonTumblrError.Detail);
            Assert.AreEqual(te.Title, jsonTumblrError.Title);
        }
    }
}
