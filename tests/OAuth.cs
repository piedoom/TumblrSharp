using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DontPanic.TumblrSharp.OAuth;
using System.Threading.Tasks;

namespace TestingTumblrSharp
{
    [TestClass]
    public class OAuthTest
    {
        // This consumer-token is only for testing!

        private readonly string _consumerKey = Environment.GetEnvironmentVariable("ConsumerKey");
        private readonly string _consumerSecret = Environment.GetEnvironmentVariable("ConsumerSecret");

        private readonly string _callbackUrl = "https://github.com/piedoom/TumblrSharp";

        [TestMethod]
        public void Token_IsValid()
        {
            bool current;

            current = new Token(null, null).IsValid;
            Assert.IsFalse(current);

            current = new Token(string.Empty, null).IsValid;
            Assert.IsFalse(current);

            current = new Token(null, string.Empty).IsValid;
            Assert.IsFalse(current);

            current = new Token(string.Empty, string.Empty).IsValid;
            Assert.IsFalse(current);

            current = new Token(_consumerKey, null).IsValid;
            Assert.IsFalse(current);

            current = new Token(_consumerKey, string.Empty).IsValid;
            Assert.IsFalse(current);

            current = new Token(null, _consumerSecret).IsValid;
            Assert.IsFalse(current);

            current = new Token(string.Empty, _consumerSecret).IsValid;
            Assert.IsFalse(current);

            current = new Token(_consumerKey, _consumerSecret).IsValid;
            Assert.IsTrue(current);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OAuth_ClientFactory_Create_Null_1()
        {
            OAuthClient lOAuthClient;

            lOAuthClient = new OAuthClientFactory().Create(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OAuth_ClientFactory_Create_Null_2()
        {
            OAuthClient lOAuthClient;
            
            lOAuthClient = new OAuthClientFactory().Create(_consumerKey, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OAuth_ClientFactory_Create_Null_3()
        {
            OAuthClient lOAuthClient;
            
            lOAuthClient = new OAuthClientFactory().Create(null, _consumerSecret);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OAuth_ClientFactory_Create_Empty_1()
        {
            OAuthClient lOAuthClient;

            lOAuthClient = new OAuthClientFactory().Create(string.Empty, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OAuth_ClientFactory_Create_Empty_2()
        {
            OAuthClient lOAuthClient;

            lOAuthClient = new OAuthClientFactory().Create(_consumerKey, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OAuth_ClientFactory_Create_Empty_3()
        {
            OAuthClient lOAuthClient;

            lOAuthClient = new OAuthClientFactory().Create(string.Empty, _consumerSecret);
        }

        [TestMethod]
        public void OAuth_ClientFactory_Create()
        {
            OAuthClient oAuthClient;

            oAuthClient = new OAuthClientFactory().Create(_consumerKey, _consumerSecret);

            Assert.IsNotNull(oAuthClient);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task OAuth_GetRequestTokenAsync_Null()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(_consumerKey, _consumerSecret);

            Token requestToken = await oAuthClient.GetRequestTokenAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task OAuth_GetRequestTokenAsync_Empty()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(_consumerKey, _consumerSecret);

            Token requestToken = await oAuthClient.GetRequestTokenAsync(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(OAuthException))]
        public async Task OAuth_GetRequestTokenAsync_Unauthorized()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create("ertd", "ertg");

            Token requestToken = await oAuthClient.GetRequestTokenAsync(_callbackUrl);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OAuth_GetAuthorizeUrl_Arg_null_1()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(_consumerKey, _consumerSecret);

            Uri url;

            url = oAuthClient.GetAuthorizeUrl(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OAuth_GetAuthorizeUrl_Arg_null_2()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(_consumerKey, _consumerSecret);

            Uri url = oAuthClient.GetAuthorizeUrl(new Token(null, null)); ;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OAuth_GetAuthorizeUrl_Arg_null_3()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(_consumerKey, _consumerSecret);

            Uri url = oAuthClient.GetAuthorizeUrl(new Token(null, "ertdgfrt"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OAuth_GetAuthorizeUrl_Arg_empty_1()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(_consumerKey, _consumerSecret);

            Token requestToken = new Token(string.Empty, string.Empty);

            Uri url = oAuthClient.GetAuthorizeUrl(requestToken);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OAuth_GetAuthorizeUrl_Arg_empty_2()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(_consumerKey, _consumerSecret);

            Token requestToken = new Token(string.Empty, "erdertf");

            Uri url = oAuthClient.GetAuthorizeUrl(new Token(string.Empty, "ertdgfrt"));
        }

        [TestMethod]
        public async Task OAuth_GetAuthorizeUrl_Arg_1()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(_consumerKey, _consumerSecret);

            Token requestToken = await oAuthClient.GetRequestTokenAsync(_callbackUrl);

            Uri url = oAuthClient.GetAuthorizeUrl(requestToken);

            Assert.IsNotNull(url);

            Assert.AreNotEqual(url, string.Empty);


        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task OAuth_GetAccessTokenAsync_Null_1()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(_consumerKey, _consumerSecret);

            Token accessToken = await oAuthClient.GetAccessTokenAsync(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task OAuth_GetAccessTokenAsync_Null_2()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(_consumerKey, _consumerSecret);

            Token requestToken = await oAuthClient.GetRequestTokenAsync(_callbackUrl);

            Token accessToken = await oAuthClient.GetAccessTokenAsync(requestToken, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task OAuth_GetAccessTokenAsync_Empty_1()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(_consumerKey, _consumerSecret);

            Token requestToken = await oAuthClient.GetRequestTokenAsync(_callbackUrl);

            Token accessToken = await oAuthClient.GetAccessTokenAsync(requestToken, string.Empty);
        }
    }
}
