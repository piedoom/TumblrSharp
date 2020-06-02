using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestingTumblrSharp
{
    [TestClass]
    public class TumblrClientTest
    {
        // This consumer-token and AccesToken is only for testing!

        private readonly string _consumerKey = Environment.GetEnvironmentVariable("ConsumerKey");
        private readonly string _consumerSecret = Environment.GetEnvironmentVariable("ConsumerSecret");

        private readonly string _accessKey = Environment.GetEnvironmentVariable("AccessKey");
        private readonly string _accessSecret = Environment.GetEnvironmentVariable("AccessSecret");

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TumblrClient_Create_Null_1()
        {
            var tc = new TumblrClientFactory().Create<TumblrClient>(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TumblrClient_Create_Null_2()
        {
            var tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TumblrClient_Create_Null_3()
        {
            var tc = new TumblrClientFactory().Create<TumblrClient>(null, _consumerKey, _consumerSecret);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TumblrClient_Create_Empty_1()
        {
            var tc = new TumblrClientFactory().Create<TumblrClient>(string.Empty, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TumblrClient_Create_Empty_2()
        {
            var tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TumblrClient_Create_Empty_3()
        {
            var tc = new TumblrClientFactory().Create<TumblrClient>(string.Empty, _consumerSecret);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TumblrClient_Create_NotValid_1()
        {
            var tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(null, null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TumblrClient_Create_NotValid_2()
        {
            var tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(_accessKey, null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TumblrClient_Create_NotValid_3()
        {
            var tc = new TumblrClientFactory().Create<TumblrClient>(_consumerKey, _consumerSecret, new Token(null, _accessSecret));
        }
    }
}
