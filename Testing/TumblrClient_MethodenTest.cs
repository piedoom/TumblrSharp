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

        // help vars
        private static readonly string _version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

        private readonly string _postText = "This is a textpost test.";

        private readonly string _postTextDelete = "This is a textpost for Deletetest.";

        private readonly string _postTextQueue = "This is a textpost test in queue posted " + DateTime.Now.ToString() + ".";

        private readonly string _footnote = "\n\nPackage <a href=\"https://www.nuget.org/packages/NewTumblrSharp/\">NewTumblrSharp for .Net</a>\n" +
                                            "Version: " + _version + "\n" +
                                            "Github: <a href=\"https://github.com/piedoom/TumblrSharp/\">piedoom/TumblrSharp</a>";

        private TumblrClient tumblrClient;

        // for postdelete
        private PostCreationInfo _deletePostInfo = null;
        public PostCreationInfo DeletePostInfo
        {
            get
            {
                if (_deletePostInfo == null)
                {
                    tumblrClient = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

                    PostData postData = PostData.CreateText(_postTextDelete + _footnote, "Testpost", new List<string> { "NewTumblrSharp", _version });

                    postData.State = PostCreationState.Published;

                    _deletePostInfo = tumblrClient.CreatePostAsync("newtsharp.tumblr.com", postData).GetAwaiter().GetResult();


                }
                return _deletePostInfo;
            }

            set
            {
                _deletePostInfo = value;
            }
        }

        public TumblrClient_MethodenTest()
        {
            tumblrClient = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));
        }

        ~TumblrClient_MethodenTest()
        {
            if (DeletePostInfo != null)
            {
                tumblrClient.DeletePostAsync("newtsharp.tumblr.com", DeletePostInfo.PostId);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task UserInfo_Required()
        {
            tumblrClient = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, null);

            var userInfo =  await tumblrClient.GetUserInfoAsync();
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task UserInfo_Disposed()
        {
            tumblrClient = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            tumblrClient.Dispose();

            var userInfo = await tumblrClient.GetUserInfoAsync();    
        }

        [TestMethod]
        public async Task UserInfo_1()
        {
            tumblrClient = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            var userInfo = await tumblrClient.GetUserInfoAsync();

            Assert.AreEqual(userInfo.Blogs[1].Name, "newtsharp");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task CreatePost_Required_1()
        {
            tumblrClient = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            PostData postData = PostData.CreateText(_postText + _footnote, "Testpost", new List<string> { "NewTumblrSharp", "Test" });

            PostCreationInfo postCreationInfo = await tumblrClient.CreatePostAsync("", postData);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task CreatePost_Required_2()
        {
            tumblrClient = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, null);

            PostData postData = PostData.CreateText(_postText + _footnote, "Testpost", new List<string> { "NewTumblrSharp", "Test" });

            PostCreationInfo postCreationInfo = await tumblrClient.CreatePostAsync("newtsharp.tumblr.com", postData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreatePost_Required_3()
        {
            tumblrClient = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));
            
            PostCreationInfo postCreationInfo = await tumblrClient.CreatePostAsync("newtsharp.tumblr.com", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task CreatePost_Required_4()
        {
            tumblrClient = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            PostCreationInfo postCreationInfo = await tumblrClient.CreatePostAsync("newtsharp.tumblr.com", PostData.CreateText());
        }

        [TestMethod]
        public async Task CreatePost_Text_1()
        {
            tumblrClient = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            PostData postData = PostData.CreateText(_postText + _footnote, "Testpost", new List<string> {"NewTumblrSharp", _version});
                        
            PostCreationInfo postCreationInfo = await tumblrClient.CreatePostAsync("newtsharp.tumblr.com", postData);

            Assert.IsNotNull(postCreationInfo);

            Assert.AreNotEqual(postCreationInfo.PostId, 0);                        
        }

        [TestMethod]
        public async Task CreatePost_Text_2()
        {
            tumblrClient = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            PostData postData = PostData.CreateText(_postTextQueue + _footnote, "Testpost", new List<string> { "NewTumblrSharp", _version });

            postData.State = PostCreationState.Queue;

            PostCreationInfo postCreationInfo = await tumblrClient.CreatePostAsync("newtsharp.tumblr.com", postData);

            Assert.IsNotNull(postCreationInfo);

            Assert.AreNotEqual(postCreationInfo.PostId, 0);
        }

        #region DeletePost

        #region success test
        [TestMethod]
        public async Task DeletePost_Success()
        {
            tumblrClient = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret));

            await tumblrClient.DeletePostAsync("newtsharp.tumblr.com", DeletePostInfo.PostId);

            DeletePostInfo = null;
        }

        #endregion

        #region argument tests
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task DeletePost_TumblrClient_1()
        {
            using (TumblrClient tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, null))
            {
                await tc.DeletePostAsync("newtsharp.tumblr.com", DeletePostInfo.PostId);
            }                
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task DeletePost_TumblrClient_2()
        {
            using (TumblrClient tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, _accessSecret)))
            {
                tc.Dispose();

                await tc.DeletePostAsync("newtsharp.tumblr.com", DeletePostInfo.PostId);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task DeletePost_2()
        {
            await tumblrClient.DeletePostAsync("", DeletePostInfo.PostId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeletePost_3()
        {
            await tumblrClient.DeletePostAsync(null, DeletePostInfo.PostId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task DeletePost_4()
        {
            await tumblrClient.DeletePostAsync("newtsharp.tumblr.com", -100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeletePost_5()
        {
            await tumblrClient.DeletePostAsync("newtsharp.tumblr.com", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(TumblrException))]
        public async Task DeletePost_6()
        {
            await tumblrClient.DeletePostAsync("newtsharp.tumblr.com", 1);
        }

        #endregion

        #endregion
    }
}
