using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTumblrSharp;

namespace TestTumblrSharp
{
    [TestClass]
    public class TumblrClient_MethodenTest
    {
        // help vars
        private static readonly string _version = typeof(TumblrClient).Assembly.GetName().Version.ToString();

        private readonly string _postText = "This is a textpost test. " + DateTime.Now.ToString();

        private readonly string _postTextDelete = "This is a textpost for Deletetest. " + DateTime.Now.ToString();

        private readonly string _postTextQueue = "This is a textpost test in queue posted " + DateTime.Now.ToString() + ".";

        private readonly string _footnote = "\n\nPackage <a href=\"https://www.nuget.org/packages/NewTumblrSharp/\">NewTumblrSharp for .Net</a>\n" +
                                            "Version: " + _version + "\n" +
                                            "Github: <a href=\"https://github.com/piedoom/TumblrSharp/\">piedoom/TumblrSharp</a>";

        // for postdelete
        private PostCreationInfo _deletePostInfo = null;

        public PostCreationInfo DeletePostInfo
        {
            get
            {
                if (_deletePostInfo == null)
                {
                    using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, new Token(Settings.accessKey, Settings.accessSecret));

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

        #region UserInfo

        [TestMethod]
        public void UserInfo_NotAuthorized()
        {
            using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, null);

            Assert.ThrowsExactly<InvalidOperationException>(() => tumblrClient.GetUserInfoAsync().GetAwaiter().GetResult());
        }

        [TestMethod]
        public void UserInfo_ClientDisposed()
        {
            TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

            tumblrClient.Dispose();

            Assert.ThrowsExactly<ObjectDisposedException>(() => tumblrClient.GetUserInfoAsync().GetAwaiter().GetResult());    
        }

        [TestMethod]
        public async Task UserInfo_ParamsCorrect()
        {
            using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

            var userInfo = await tumblrClient.GetUserInfoAsync();

            Assert.AreEqual("newtsharp", userInfo.Blogs[1].Name);
        }

        #endregion

        #region CreatePost

        [TestMethod]
        public void CreatePost_Required_BlognameDontRExists()
        {
            using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

            PostData postData = PostData.CreateText(_postText + _footnote, "Testpost", new List<string> { "NewTumblrSharp", "Test" });

            Assert.ThrowsExactly<ArgumentException>(() => tumblrClient.CreatePostAsync("", postData).GetAwaiter().GetResult());
        }

        [TestMethod]
        public void CreatePost_Required_NoAuthorized()
        {
            using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, null);

            PostData postData = PostData.CreateText(_postText + _footnote, "Testpost", new List<string> { "NewTumblrSharp", "Test" });

            Assert.ThrowsExactly<InvalidOperationException>(() => tumblrClient.CreatePostAsync("newtsharp.tumblr.com", postData).GetAwaiter().GetResult());
        }

        [TestMethod]
        public void CreatePost_Required_NullPostData()
        {
            using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

            Assert.ThrowsExactly<ArgumentNullException>(() => tumblrClient.CreatePostAsync("newtsharp.tumblr.com", null).GetAwaiter().GetResult());
        }

        [TestMethod]
        public void CreatePost_Required_EmptyPostData()
        {
            using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

            Assert.ThrowsExactly<ArgumentException>(() => tumblrClient.CreatePostAsync("newtsharp.tumblr.com", PostData.CreateText()).GetAwaiter().GetResult());
        }

        #region Text 

        [TestMethod]
        public async Task CreatePost_Text()
        {
            using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

            PostData postData = PostData.CreateText(_postText + _footnote, "Testpost", new List<string> {"NewTumblrSharp", _version});
                        
            PostCreationInfo postCreationInfo = await tumblrClient.CreatePostAsync("newtsharp.tumblr.com", postData);

            Assert.IsNotNull(postCreationInfo);

            Assert.AreNotEqual(0, postCreationInfo.PostId);                        
        }

        [TestMethod]
        public async Task CreatePost_ToQueue()
        {
            using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

            PostData postData = PostData.CreateText(_postTextQueue + _footnote, "Testpost", new List<string> { "NewTumblrSharp", _version });

            postData.State = PostCreationState.Queue;

            PostCreationInfo postCreationInfo = await tumblrClient.CreatePostAsync("newtsharp.tumblr.com", postData);

            Assert.IsNotNull(postCreationInfo);

            Assert.AreNotEqual(0, postCreationInfo.PostId);
        }

        #endregion

        #endregion

        #region DeletePost

        #region success test
        
        [TestMethod]
        public async Task DeletePost_Success()
        {
            using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

            await tumblrClient.DeletePostAsync("newtsharp.tumblr.com", DeletePostInfo.PostId);

            DeletePostInfo = null;
        }

        #endregion

        #region argument tests

        [TestMethod]
        public void DeletePost_ClientNotAuthorized()
        {
            using TumblrClient tc = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, null);
            
            Assert.ThrowsExactly<InvalidOperationException>(() => tc.DeletePostAsync("newtsharp.tumblr.com", DeletePostInfo.PostId).GetAwaiter().GetResult());                            
        }

        [TestMethod]
        public void  DeletePost_ClientDisposed()
        {
            TumblrClient tc = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);
            
            tc.Dispose();
            
            Assert.ThrowsExactly<ObjectDisposedException>(() => tc.DeletePostAsync("newtsharp.tumblr.com", DeletePostInfo.PostId).GetAwaiter().GetResult());
        }

        [TestMethod]
        public void DeletePost_EmptyBlogname()
        {
            using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

            Assert.ThrowsExactly<ArgumentException>(() => tumblrClient.DeletePostAsync(string.Empty, DeletePostInfo.PostId).GetAwaiter().GetResult());
        }

        [TestMethod]
        public void DeletePost_NullBlogname()
        {
            using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

            Assert.ThrowsExactly<ArgumentNullException>(() => tumblrClient.DeletePostAsync(null, DeletePostInfo.PostId).GetAwaiter().GetResult());
        }

        [TestMethod]
        public void DeletePost_NonCorrectId()
        {
            using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => tumblrClient.DeletePostAsync("newtsharp.tumblr.com", -100).GetAwaiter().GetResult());
        }

        [TestMethod]
        public void DeletePost_IdDontExist()
        {
            using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

            Assert.ThrowsExactly<TumblrException>(() => tumblrClient.DeletePostAsync("newtsharp.tumblr.com", 1).GetAwaiter().GetResult());
        }

        #endregion

        #endregion
    }
}
