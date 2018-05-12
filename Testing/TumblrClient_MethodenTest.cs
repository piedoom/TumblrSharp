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
        // This consumer-token and AccesToken is only for testing!

        private string _consumerKey = "hGSKqgb24RJBnWkodL5GTFIeadgyOnWl0qsXi7APRC76HELnrE";
        private string _consumerSecret = "jdNWSSbG7bZ8tYJcYzmyfH33o5cq7ihmJeWMVntB3pUHNptqn3";

        private string _accessKey = "F1G7BF1JW4f1VKJ93xJSi7D66yZKN3Uj0bArn5i5riwVEnMHuU";
        private string _accessSecret = "O977YH42yg98IsS9BAk80r5e5grYQDY9HauVmgf0aEmceZ2UTz";

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
    }
}
