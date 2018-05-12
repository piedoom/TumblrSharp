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
    public class TumblrClientTest
    {
        // This consumer-token and AccesToken is only for testing!

        private string _consumerKey = "hGSKqgb24RJBnWkodL5GTFIeadgyOnWl0qsXi7APRC76HELnrE";
        private string _consumerSecret = "jdNWSSbG7bZ8tYJcYzmyfH33o5cq7ihmJeWMVntB3pUHNptqn3";

        private string _accessKey = "F1G7BF1JW4f1VKJ93xJSi7D66yZKN3Uj0bArn5i5riwVEnMHuU";
        private string _accessSecret = "O977YH42yg98IsS9BAk80r5e5grYQDY9HauVmgf0aEmceZ2UTz";

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
