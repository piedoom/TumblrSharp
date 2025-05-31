using DontPanic.TumblrSharp.OAuth;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTumblrSharp
{
    [TestClass]
    public class OAuthTest
    {
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

            current = new Token(Settings.consumerKey, null).IsValid;
            Assert.IsFalse(current);

            current = new Token(Settings.consumerKey, string.Empty).IsValid;
            Assert.IsFalse(current);

            current = new Token(null, Settings.consumerSecret).IsValid;
            Assert.IsFalse(current);

            current = new Token(string.Empty, Settings.consumerSecret).IsValid;
            Assert.IsFalse(current);

            current = new Token(Settings.consumerKey, Settings.consumerSecret).IsValid;
            Assert.IsTrue(current);
        }

        public static IEnumerable<object[]> SecretTokenNullData 
        {
            get 
            {
                yield return new object[] { null, null};
                yield return new object[] { Settings.consumerKey, null };
                yield return new object[] { null, Settings.consumerSecret };
            }
        }

        [TestMethod]
        [DynamicData(nameof(SecretTokenNullData), DynamicDataSourceType.Property)]
        public void OAuth_ClientFactory_Create_Null(string consumerKey, string consumerSecret)
        {
            OAuthClient lOAuthClient;
            Assert.ThrowsExactly<ArgumentNullException>(() => lOAuthClient = new OAuthClientFactory().Create(consumerKey, consumerSecret));
        }

        public static IEnumerable<object[]> SecretTokenEmptyData
        {
            get
            {
                yield return new object[] { string.Empty, string.Empty };
                yield return new object[] { Settings.consumerKey, string.Empty };
                yield return new object[] { string.Empty, Settings.consumerSecret };
            }
        }

        [TestMethod]
        [DynamicData(nameof(SecretTokenEmptyData), DynamicDataSourceType.Property)]
        public void OAuth_ClientFactory_Create_Empty(string consumerKey, string consumerSecret)
        {
            Assert.ThrowsExactly<ArgumentException>(() => new OAuthClientFactory().Create(consumerKey, consumerSecret));
        }

        [TestMethod]
        public void OAuth_ClientFactory_Create()
        {
            OAuthClient oAuthClient;

            oAuthClient = new OAuthClientFactory().Create(Settings.consumerKey, Settings.consumerSecret);

            Assert.IsNotNull(oAuthClient);
        }

        [TestMethod]
        public void OAuth_GetRequestTokenAsync_Null()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(Settings.consumerKey, Settings.consumerSecret);
            
            Assert.ThrowsExactly<ArgumentNullException>( () => oAuthClient.GetRequestTokenAsync(null).GetAwaiter().GetResult());
        }

        [TestMethod]
        public void OAuth_GetRequestTokenAsync_Empty()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(Settings.consumerKey, Settings.consumerSecret);

            Assert.ThrowsExactly<ArgumentException>(() => oAuthClient.GetRequestTokenAsync(string.Empty).GetAwaiter().GetResult());
        }

        [TestMethod]
        public void OAuth_GetRequestTokenAsync_Unauthorized()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create("ertd", "ertg");

            Assert.ThrowsExactly<OAuthException>(() => oAuthClient.GetRequestTokenAsync(Settings.callbackUrl).GetAwaiter().GetResult());
        }

        [TestMethod]
        public void OAuth_GetAuthorizeUrl_Arg_null_1()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(Settings.consumerKey, Settings.consumerSecret);

            Assert.ThrowsExactly<ArgumentNullException>(() => oAuthClient.GetAuthorizeUrl(null));
        }

        [TestMethod]
        [DataRow(null, null)]
        [DataRow(null, "ert")]
        public void OAuth_GetAuthorizeUrl_Arg_null_2(string requestKey, string requestSecret)
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(Settings.consumerKey, Settings.consumerSecret);

            Token requestToken = new Token(requestKey, requestSecret);

            Assert.ThrowsExactly<ArgumentNullException>(() => oAuthClient.GetAuthorizeUrl(requestToken));
        }

        [TestMethod]
        [DataRow("", "")]
        [DataRow("", "ert")]
        public void OAuth_GetAuthorizeUrl_Arg_empty_1(string requestKey, string requestSecret)
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(Settings.consumerKey, Settings.consumerSecret);

            Token requestToken = new Token(requestKey, requestSecret);

            Assert.ThrowsExactly<ArgumentException>(() => oAuthClient.GetAuthorizeUrl(requestToken));
        }

        [TestMethod]
        public async Task OAuth_GetAuthorizeUrl_Arg_1()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(Settings.consumerKey, Settings.consumerSecret);

            Token requestToken = await oAuthClient.GetRequestTokenAsync(Settings.callbackUrl);

            Uri url = oAuthClient.GetAuthorizeUrl(requestToken);

            Assert.IsNotNull(url);

            Assert.AreNotEqual(url.ToString(), string.Empty);
        }

        [TestMethod]
        public void OAuth_GetAccessTokenAsync_Null_1()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(Settings.consumerKey, Settings.consumerSecret);

            Assert.ThrowsExactly<ArgumentNullException>(() => oAuthClient.GetAccessTokenAsync(null, null).GetAwaiter().GetResult());
        }

        [TestMethod]
        public async Task OAuth_GetAccessTokenAsync_Null_2()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(Settings.consumerKey, Settings.consumerSecret);

            Token requestToken = await oAuthClient.GetRequestTokenAsync(Settings.callbackUrl);

            Assert.ThrowsExactly<ArgumentNullException>(() => oAuthClient.GetAccessTokenAsync(requestToken, null).GetAwaiter().GetResult());
        }

        [TestMethod]
        public async Task OAuth_GetAccessTokenAsync_Empty_1()
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(Settings.consumerKey, Settings.consumerSecret);

            Token requestToken = await oAuthClient.GetRequestTokenAsync(Settings.callbackUrl);

            Assert.ThrowsExactly<ArgumentException>(() => oAuthClient.GetAccessTokenAsync(requestToken, string.Empty).GetAwaiter().GetResult());
        }
    }
}
