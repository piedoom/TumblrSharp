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

            PostData postData = PostData.CreateText("this is a textpost test. NewTumblrSharp " + version, "Testpost", new List<string> { "NewTumblrSharp", "Test" });

            PostCreationInfo postCreationInfo = await tc.CreatePostAsync("", postData);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task CreatePost_Required_2()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            PostData postData = PostData.CreateText("this is a textpost test. NewTumblrSharp " + version, "Testpost", new List<string> { "NewTumblrSharp", "Test" });

            PostCreationInfo postCreationInfo = await tc.CreatePostAsync("test", postData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreatePost_Required_3()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));
                        
            UserInfo userInfo = await tc.GetUserInfoAsync();

            PostCreationInfo postCreationInfo = await tc.CreatePostAsync(userInfo.Blogs[0].Name, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task CreatePost_Required_4()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            UserInfo userInfo = await tc.GetUserInfoAsync();

            PostCreationInfo postCreationInfo = await tc.CreatePostAsync("newtsharp.tumblr.com", PostData.CreateText());
        }

        [TestMethod]
        public async Task CreatePost_Text()
        {
            string version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

            tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            PostData postData = PostData.CreateText("This is a textpost test.\n\n Package NewTumblrSharp " + version, "Testpost", new List<string> {"NewTumblrSharp", version});
                        
            PostCreationInfo postCreationInfo = await tc.CreatePostAsync("newtsharp.tumblr.com", postData);

            Assert.IsNotNull(postCreationInfo);

            Assert.AreNotEqual(postCreationInfo.PostId, 0);                        
        }
    }
}
