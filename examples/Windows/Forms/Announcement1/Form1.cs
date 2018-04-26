using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.OAuth;
using DontPanic.TumblrSharp.Client;

namespace Announcement1
{
    public partial class Form1 : Form
    {
        private static string _callbackUrl = "https://github.com/piedoom/TumblrSharp";

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            OAuthClient oAuthClient = new OAuthClientFactory().Create(ConsumerKey.Text, ConsumerSecret.Text);

            Token requestToken = await oAuthClient.GetRequestTokenAsync(_callbackUrl);

            Uri url = oAuthClient.GetAuthorizeUrl(requestToken);

            var verifierUrl = WebAuth.ShowDialog(url, _callbackUrl);

            Token accessToken = await oAuthClient.GetAccessTokenAsync(requestToken, verifierUrl.OriginalString);

            AccessKey.Text = accessToken.Key;
            AccessSecret.Text = accessToken.Secret;

            Activate();

            var tc = new TumblrClientFactory().Create<TumblrClient>(ConsumerKey.Text, ConsumerSecret.Text, accessToken);

            var userInfo = await tc.GetUserInfoAsync();

            MessageBox.Show($"Dein Blog lautet {userInfo.Blogs[0].Name}");

        }
    }
}
