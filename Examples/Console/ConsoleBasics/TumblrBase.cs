using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using System;

namespace ConsoleBasics
{
    public class TumblrBase
    {
        protected TumblrClient client = null;

        private readonly string CONSUMER_KEY = "xxx";
        private readonly string CONSUMER_SECRET = "xxx";
        private readonly string OAUTH_TOKEN = "xxx";
        private readonly string OAUTH_TOKEN_SECRET = "xxx";

        public TumblrBase()
        {
            if (CONSUMER_KEY == "xxx")
            {
                Console.WriteLine("Change in source the consumerKey, etc...!");
                Console.WriteLine();

                throw new Exception();
            }

            this.client = new TumblrClientFactory().Create<TumblrClient>(CONSUMER_KEY, CONSUMER_SECRET, new Token(OAUTH_TOKEN, OAUTH_TOKEN_SECRET));
        }
    }
}
