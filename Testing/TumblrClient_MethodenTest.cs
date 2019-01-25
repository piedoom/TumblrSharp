using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    [TestClass]
    public class TumblrClient_MethodenTest
    {
        // This ConsumerToken and AccessToken is only for testing!

        private readonly string _consumerKey = "hGSKqgb24RJBnWkodL5GTFIeadgyOnWl0qsXi7APRC76HELnrE";
        private readonly string _consumerSecret = "jdNWSSbG7bZ8tYJcYzmyfH33o5cq7ihmJeWMVntB3pUHNptqn3";

        private readonly string _accessKey = "F1G7BF1JW4f1VKJ93xJSi7D66yZKN3Uj0bArn5i5riwVEnMHuU";
        private readonly string _accessSecret = "O977YH42yg98IsS9BAk80r5e5grYQDY9HauVmgf0aEmceZ2UTz";

        private static readonly string _version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

        private readonly string _postText = "This is a textpost test.";

        private readonly string _postTextQueue = "This is a textpost test in queue posted " + DateTime.Now.ToString() + ".";

        private readonly string _footnote = "\n\nPackage <a href=\"https://www.nuget.org/packages/NewTumblrSharp/\">NewTumblrSharp for .Net</a>\n" +
                                            "Version: " + _version + "\n" +
                                            "Github: <a href=\"https://github.com/piedoom/TumblrSharp/\">piedoom/TumblrSharp</a>";

        private TumblrClient tc;

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task UserInfo_Required()
        {
            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, null);

            var userInfo =  await tc.GetUserInfoAsync();
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task UserInfo_Disposed()
        {
            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            tc.Dispose();

            var userInfo = await tc.GetUserInfoAsync();    
        }

        [TestMethod]
        public async Task UserInfo_1()
        {
            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            var userInfo = await tc.GetUserInfoAsync();

            Assert.AreEqual(userInfo.Blogs[1].Name, "newtsharp");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task CreatePost_Required_1()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            PostData postData = PostData.CreateText(_postText + _footnote, "Testpost", new List<string> { "NewTumblrSharp", "Test" });

            PostCreationInfo postCreationInfo = await tc.CreatePostAsync("", postData);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task CreatePost_Required_2()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            PostData postData = PostData.CreateText(_postText + _footnote, "Testpost", new List<string> { "NewTumblrSharp", "Test" });

            PostCreationInfo postCreationInfo = await tc.CreatePostAsync("test", postData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreatePost_Required_3()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));
            
            PostCreationInfo postCreationInfo = await tc.CreatePostAsync("newtsharp.tumblr.com", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task CreatePost_Required_4()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            PostCreationInfo postCreationInfo = await tc.CreatePostAsync("newtsharp.tumblr.com", PostData.CreateText());
        }

        [TestMethod]
        public async Task CreatePost_Text_1()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            PostData postData = PostData.CreateText(_postText + _footnote, "Testpost", new List<string> {"NewTumblrSharp", version});
                        
            PostCreationInfo postCreationInfo = await tc.CreatePostAsync("newtsharp.tumblr.com", postData);

            Assert.IsNotNull(postCreationInfo);

            Assert.AreNotEqual(postCreationInfo.PostId, 0);                        
        }

        [TestMethod]
        public async Task CreatePost_Text_2()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            PostData postData = PostData.CreateText(_postTextQueue + _footnote, "Testpost", new List<string> { "NewTumblrSharp", version });

            postData.State = PostCreationState.Queue;

            PostCreationInfo postCreationInfo = await tc.CreatePostAsync("newtsharp.tumblr.com", postData);

            Assert.IsNotNull(postCreationInfo);

            Assert.AreNotEqual(postCreationInfo.PostId, 0);
        }

        [TestMethod]
        public async Task DeletePost_1()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            PostData postData = PostData.CreateText(_postTextQueue + _footnote, "Testpost", new List<string> { "NewTumblrSharp", version });

            postData.State = PostCreationState.Queue;

            PostCreationInfo postCreationInfo = await tc.CreatePostAsync("newtsharp.tumblr.com", postData);

            await tc.DeletePostAsync("newtsharp.tumblr.com", postCreationInfo.PostId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task DeletePost_2()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            PostData postData = PostData.CreateText(_postTextQueue + _footnote, "Testpost", new List<string> { "NewTumblrSharp", version });

            postData.State = PostCreationState.Queue;

            PostCreationInfo postCreationInfo = await tc.CreatePostAsync("newtsharp.tumblr.com", postData);

            await tc.DeletePostAsync("", postCreationInfo.PostId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeletePost_3()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            PostData postData = PostData.CreateText(_postTextQueue + _footnote, "Testpost", new List<string> { "NewTumblrSharp", version });

            postData.State = PostCreationState.Queue;

            PostCreationInfo postCreationInfo = await tc.CreatePostAsync("newtsharp.tumblr.com", postData);

            await tc.DeletePostAsync(null, postCreationInfo.PostId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task DeletePost_4()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            await tc.DeletePostAsync("newtsharp.tumblr.com", -100);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task DeletePost_5()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            await tc.DeletePostAsync("newtsharp.tumblr.com", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(TumblrException))]
        public async Task DeletePost_6()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            await tc.DeletePostAsync("newtsharp.tumblr.com", 1);
        }
    }
}
