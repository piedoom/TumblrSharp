using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTumblrSharp
{
    [TestClass]
    public class JsonConvertersTest
    {
        #region NoteConverter

         [TestMethod]
        public void NoteConverter_Theorie()
        {
            const string resultStr = "{\r\n  \"title\": null,\r\n  \"body\": \"testbody\",\r\n  \"type\": \"all\",\r\n  \"blog_name\": \"TestBlog\"," +
                "\r\n  \"id\": 12456789,\r\n  \"post_url\": null,\r\n  \"slug\": null,\r\n  \"timestamp\": 0,\r\n  \"state\": \"published\"," +
                "\r\n  \"format\": \"html\",\r\n  \"reblog_key\": null,\r\n  \"tags\": null,\r\n  \"short_url\": null,\r\n  \"summary\": null," +
                "\r\n  \"note_count\": 0,\r\n  \"notes\": [\r\n    {\r\n      \"type\": \"like\",\r\n      \"timestamp\": 0," +
                "\r\n      \"blog_name\": \"OtherBlock\",\r\n      \"blog_uuid\": null,\r\n      \"blog_url\": null,\r\n      \"followed\": false," +
                "\r\n      \"avatar_shape\": \"circle\",\r\n      \"reply_text\": null,\r\n      \"post_id\": null," +
                "\r\n      \"reblog_parent_blog_name\": null\r\n    }\r\n  ],\r\n  \"total_posts\": 0,\r\n  \"trail\": []\r\n}";

            TextPost basicTextPost = new TextPost
            {
                BlogName = "TestBlog",
                Body = "testbody",
                Id = 12456789,
                Timestamp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Format = PostFormat.Html,
                Trails = new List<Trail>()
            };

            basicTextPost.Notes = new List<BaseNote>
            {
                new BaseNote()
                {
                    BlogName = "OtherBlock",
                    Type = NoteType.Like,
                    AvatarShape = AvatarShape.Circle,
                    Timestamp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
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
            TumblrClient tc = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

            Assert.IsNotNull(tc);

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

                Assert.AreEqual(baseNote, jsonNote);
            }
        }

        #endregion

        #region TrailConverter

        [TestMethod]
        public async Task TrailConverter()
        {
            TumblrClient tc = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

            Assert.IsNotNull(tc);

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
                        break;
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

                Assert.AreEqual(baseTrail, jsonTrail);

            }
        }

        #endregion

        #region TumblrErrorsConverter

        [TestMethod]
        public void TumblrErrorsConverter()
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

            Assert.AreEqual(te, jsonTumblrError);
        }

        #endregion

        #region BoolConverter

        #region HelperClass
        private class TestBoolClass
        {
            [JsonProperty(PropertyName = "error")]
            [JsonConverter(typeof(BoolConverter))]
            public bool Error { get; set; }

            [JsonProperty(PropertyName = "success")]
            [JsonConverter(typeof(BoolConverter))]
            public bool Success { get; set; }
        }

        #endregion

        [TestMethod]
        [ExpectedException(typeof(JsonReaderException))]
        public void BoolConverters_FalseBoolString()
        {
            string exceptString = "{\r\n  \"error\": \"Yes\",\r\n  \"success\": \"N\"\r\n}";

            TestBoolClass jsonBool = JsonConvert.DeserializeObject<TestBoolClass>(exceptString);
        }

        [TestMethod]
        public void BoolConverters()
        {
            TestBoolClass exceptBoolClass = new TestBoolClass()
            {
                Error = true,
                Success = false
            };

            string exceptString = "{\r\n  \"error\": \"Y\",\r\n  \"success\": \"N\"\r\n}";

            TestBoolClass jsonBool = JsonConvert.DeserializeObject<TestBoolClass>(exceptString);

            Assert.AreEqual(exceptBoolClass.Error, jsonBool.Error);
            Assert.AreEqual(exceptBoolClass.Success, jsonBool.Success);

            string jsonString = JsonConvert.SerializeObject(exceptBoolClass, Formatting.Indented);

            Assert.AreEqual(exceptString, jsonString);
        }

        #endregion

        #region EnumStringConverter

        #region HelperClass

        private class TEnumStringHelperClass
        {
            [JsonProperty(PropertyName = "posttype")]
            [JsonConverter(typeof(EnumStringConverter))]
            public PostType PostType { get; set; }

            [JsonProperty(PropertyName = "notetype")]
            [JsonConverter(typeof(EnumStringConverter))]
            public NoteType NoteType { get; set; }

            [JsonProperty(PropertyName = "blogtype")]
            [JsonConverter(typeof(EnumStringConverter))]
            public BlogType BlogType { get; set; }

            public override bool Equals(object obj)
            {
                return obj is TEnumStringHelperClass @class &&
                       PostType == @class.PostType &&
                       NoteType == @class.NoteType &&
                       BlogType == @class.BlogType;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(PostType, NoteType, BlogType);
            }
        }

        #endregion

        [TestMethod]
        public void EnumStringConverter()
        {
            TEnumStringHelperClass exceptClass = new TEnumStringHelperClass()
            {
                PostType = PostType.Photo,
                NoteType = NoteType.Posted,
                BlogType = BlogType.Public
            };

            string exceptString = "{\r\n  \"posttype\": \"photo\",\r\n  \"notetype\": \"posted\",\r\n  \"blogtype\": \"public\"\r\n}";

            string jsonString = JsonConvert.SerializeObject(exceptClass, Formatting.Indented);

            Assert.AreEqual(exceptString, jsonString);

            TEnumStringHelperClass jsonClass = JsonConvert.DeserializeObject<TEnumStringHelperClass>(exceptString);

            Assert.AreEqual(exceptClass, jsonClass);
        }

        #endregion

        #region PostArrayConverter

        #region PostArrayConverterHelperClass

        private class PostArrayConverterHelperClass
        {
            [JsonProperty(PropertyName = "posts")]
            [JsonConverter(typeof(PostArrayConverter))]
            public BasePost[] Posts { get; set; }

            public override bool Equals(object obj)
            {
                if (!(obj is PostArrayConverterHelperClass compareObj))
                {
                    return false;
                }
                else
                {
                    if (Posts.Count() != compareObj.Posts.Count())
                    {
                        return false;
                    }

                    for (int i = 0; i < Posts.Count(); i++)
                    {
                        if (Posts[i].Equals(compareObj.Posts[i]) == false)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Posts);
            }
        }

        #endregion

        [TestMethod]
        public void PostArrayConverter_EmptyArray()
        {
            PostArrayConverterHelperClass exceptClass = new PostArrayConverterHelperClass
            {
                Posts = new BasePost[0]
            };

            string exceptString = "{\r\n  \"posts\": []\r\n}";

            string jsonString = JsonConvert.SerializeObject(exceptClass, Formatting.Indented);

            Assert.AreEqual(exceptString, jsonString);

            PostArrayConverterHelperClass jsonClass = JsonConvert.DeserializeObject<PostArrayConverterHelperClass>(exceptString);

            Assert.AreEqual(exceptClass.Posts.Count(), jsonClass.Posts.Count());
        }

        [TestMethod]
        public void PostArrayConverter_Values()
        {
            DateTime timeStamp = DateTime.Now;

            string timeStampStr = DateTimeHelper.ToTimestamp(timeStamp).ToString();

            PostArrayConverterHelperClass exceptClass = new PostArrayConverterHelperClass
            {
                Posts = new BasePost[1]
                {
                     new TextPost()
                     {
                         Type = PostType.Text,
                         Title = "Testpost",
                         Body = "this a textpost",
                         BlogName = "IrgendeinBlog",
                         Id = 1234563,
                         Tags = new string[0],
                         Timestamp = timeStamp,
                         Notes = new List<BaseNote>(),
                         Trails = new List<Trail>()
                     }
                }
            };

            string exceptString = "{\r\n  \"posts\": [\r\n    {\r\n      \"title\": \"Testpost\",\r\n      \"body\": \"this a textpost\"," +
                "\r\n      \"type\": \"text\",\r\n      \"blog_name\": \"IrgendeinBlog\",\r\n      \"id\": 1234563,\r\n      \"post_url\": null," +
                "\r\n      \"slug\": null,\r\n      \"timestamp\": " + timeStampStr + ",\r\n      \"state\": \"published\"," +
                "\r\n      \"format\": \"html\",\r\n      \"reblog_key\": null,\r\n      \"tags\": [],\r\n      \"short_url\": null," +
                "\r\n      \"summary\": null,\r\n      \"note_count\": 0,\r\n      \"notes\": [],\r\n      \"total_posts\": 0," +
                "\r\n      \"trail\": []\r\n    }\r\n  ]\r\n}";

            string jsonString = JsonConvert.SerializeObject(exceptClass, Formatting.Indented);

            Assert.AreEqual(exceptString, jsonString);

            PostArrayConverterHelperClass jsonClass = JsonConvert.DeserializeObject<PostArrayConverterHelperClass>(exceptString);

            Assert.AreEqual(exceptClass.Posts.Count(), jsonClass.Posts.Count());

        }


        #endregion

        #region TimestampConverter

        #region HelperClass

        private class TimestampConverterHelper
        {
            [JsonProperty(PropertyName = "timestamp")]
            [JsonConverter(typeof(TimestampConverter))]
            public DateTime Timestamp { get; set; }
        }

        #endregion

        [TestMethod]
        public void TimestampConverter()
        {
            TimestampConverterHelper exceptClass = new TimestampConverterHelper
            {
                Timestamp = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            string exceptString = "{\r\n  \"timestamp\": 0\r\n}";

            string json = JsonConvert.SerializeObject(exceptClass, Formatting.Indented);

            Assert.AreEqual(exceptString, json);
        }

        public void TimestampConverter_1()
        {
            TimestampConverterHelper exceptClass = new TimestampConverterHelper
            {
                Timestamp = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            string exceptString = "{\r\n  \"timestamp\": 0\r\n}";


            TimestampConverterHelper jsonClass = JsonConvert.DeserializeObject<TimestampConverterHelper>(exceptString);

            Assert.AreEqual(exceptClass.Timestamp.ToUniversalTime(), jsonClass.Timestamp.ToUniversalTime());    
        }

        public void TimestampConverter_2()
        {
            TimestampConverterHelper exceptClass = new TimestampConverterHelper
            {
                Timestamp = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            exceptClass.Timestamp = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Local);

            string exceptString = "{\r\n  \"timestamp\": 946681200\r\n}";

            string json = JsonConvert.SerializeObject(exceptClass, Formatting.Indented);

            Assert.AreEqual(exceptString, json);

            var jsonClass = JsonConvert.DeserializeObject<TimestampConverterHelper>(exceptString);

            Assert.AreEqual(exceptClass.Timestamp.ToLocalTime(), jsonClass.Timestamp.ToLocalTime());
        }

        #endregion
    }
}
