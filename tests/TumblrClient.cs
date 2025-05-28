using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestTumblrSharp;

namespace TestingTumblrSharp
{
    [TestClass]
    public class TumblrClientTest
    {
        public static IEnumerable<object[]> ConsumerTokenEmptyData
        {
            get
            {
                yield return new object[] { string.Empty, string.Empty };
                yield return new object[] { Settings.consumerKey, string.Empty };
                yield return new object[] { string.Empty, Settings.consumerSecret };
            }
        }

        public static IEnumerable<object[]> ConsumerTokenNullData
        {
            get
            {
                yield return new object[] { null, null };
                yield return new object[] { Settings.consumerKey, null };
                yield return new object[] { null, Settings.consumerSecret };
            }
        }

        public static IEnumerable<object[]> AccessTokenNullData
        {
            get
            {
                yield return new object[] { null, null };
                yield return new object[] { Settings.accessKey, null };
                yield return new object[] { null, Settings.accessSecret };
            }
        }

        [TestMethod]
        [DynamicData(nameof(ConsumerTokenNullData), DynamicDataSourceType.Property)]
        public void TumblrClient_Create_NullPrams(string consumerKey, string consumerSecret)
        {
            Assert.ThrowsExactly<ArgumentNullException>( () => new TumblrClientFactory().Create<TumblrClient>(consumerKey, consumerSecret) );
        }

        [TestMethod]
        [DynamicData(nameof(ConsumerTokenEmptyData), DynamicDataSourceType.Property)]
        public void TumblrClient_Create_EmptyParams(string consumerKey, string consumerSecret)
        {
            Assert.ThrowsExactly<ArgumentException>(() => new TumblrClientFactory().Create<TumblrClient>(consumerKey, consumerSecret) );
        }

        [TestMethod]
        [DynamicData(nameof(AccessTokenNullData), DynamicDataSourceType.Property)]
        public void TumblrClient_Create_NotValid(string accessKey, string accessSecret)
        {
            Assert.ThrowsExactly<ArgumentException>(() => new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, new Token(accessKey, accessSecret)) );
        }
    }
}
