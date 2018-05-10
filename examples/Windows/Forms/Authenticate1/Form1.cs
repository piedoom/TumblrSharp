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

namespace Authenticate1
{
    public partial class Form1 : Form
    {
        private static string _callbackUrl = "https://github.com/piedoom/TumblrSharp";

        public Form1()
        {
            InitializeComponent();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            // creating oAuth-client
            OAuthClient oAuthClient = new OAuthClientFactory().Create(ConsumerKey.Text, ConsumerSecret.Text);

            // get requesttoken
            Token requestToken = await oAuthClient.GetRequestTokenAsync(_callbackUrl);

            // get the authorize Url
            Uri url = oAuthClient.GetAuthorizeUrl(requestToken);

            var verifierUrl = WebAuth.ShowDialog(url, _callbackUrl);

            Token accessToken = await oAuthClient.GetAccessTokenAsync(requestToken, verifierUrl.OriginalString);

            AccessKey.Text = accessToken.Key;
            AccessSecret.Text = accessToken.Secret;

            Activate();

            UserInfo userInfo = null;

            try
            {
                var tc = new TumblrClientFactory().Create<TumblrClient>(ConsumerKey.Text, ConsumerSecret.Text, accessToken);
                userInfo = await tc.GetUserInfoAsync();
            }
            catch (Exception)
            {
                MessageBox.Show("Logon failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            

            MessageBox.Show($"Success! the name of your blog is {userInfo.Blogs[0].Name}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
