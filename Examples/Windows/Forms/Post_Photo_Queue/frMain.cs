using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using Examples.Basics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TumblrSharp.Samples.Common.Dialog;

namespace Post_Photo_Queue
{
    public partial class FrMain : Form
    {
        private string CONSUMER_KEY = string.Empty;
        private string CONSUMER_SECRET = string.Empty;
        private string OAUTH_TOKEN = string.Empty;
        private string OAUTH_TOKEN_SECRET = string.Empty;

        private List<BinaryFile> binaryFiles;
        private Tags tags;

        public TumblrClient TumblrClient
        {
            get
            {
                if (CONSUMER_KEY == string.Empty)
                {
                    DlgSetToken dlg = new DlgSetToken();

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        CONSUMER_KEY = dlg.ConsumerKey;
                        CONSUMER_SECRET = dlg.ConsumerSecret;
                        OAUTH_TOKEN = dlg.AccessKey;
                        OAUTH_TOKEN_SECRET = dlg.AccessSecret;
                    }
                    else
                    {
                        this.Close();
                    }
                }

                return new TumblrClientFactory().Create<TumblrClient>(CONSUMER_KEY, CONSUMER_SECRET, new Token(OAUTH_TOKEN, OAUTH_TOKEN_SECRET));
            }
        }

        public List<String> Blogs
        {
            get
            {
                List<string> blogs = new List<string>();

                var userInfo = TumblrClient.GetUserInfoAsync().GetAwaiter().GetResult();

                foreach (var blog in userInfo.Blogs)
                {
                    blogs.Add(blog.Name);
                }

                return blogs;

            }
        }

        public FrMain()
        {
            InitializeComponent();

            tags = new Tags(null, false, TumblrClient);

            binaryFiles = new List<BinaryFile>();

            cbBlogs.DataSource = Blogs;

            DateTime publishedDay = DateTime.Now;

            publishedDay = publishedDay.AddDays(1);

            TbDateTime.Text = publishedDay.ToString();
                        
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            DlgAddPhoto.ShowDialog();
        }

        private void DlgAddPhoto_FileOk(object sender, CancelEventArgs e)
        {
            FileDialog dlg = sender as FileDialog;

            var file = dlg.FileName;

            byte[] array;

            MemoryStream arrayStream = new MemoryStream();

            using (var stream = new FileStream(file, FileMode.Open))
            {
                stream.CopyTo(arrayStream);
            }

            array = arrayStream.ToArray();

            binaryFiles.Add(new BinaryFile(array));

            var idx = binaryFiles.Count;

            Image bild = new Bitmap(arrayStream);

            PictureBox pictureBox = new PictureBox()
            {
                Height = 150,
                Width = 150,
                Image = bild.GetThumbnailImage(150, 150, null, IntPtr.Zero),
                Tag = idx

            };

            PhotoView.Controls.Add(pictureBox);
        }

        private async void BtnPost_Click(object sender, EventArgs e)
        {
            string blogName = cbBlogs.Text;

            string caption = TbCaption.Text;

            PostData postData = PostData.CreatePhoto(binaryFiles, caption, null, tags.ToList(), PostCreationState.Queue);

            if (CbDateTime.Checked)
            {
                postData.Publish_On = Convert.ToDateTime(TbDateTime.Text);
            }

            var postCreationInfo = await TumblrClient.CreatePostAsync(blogName, postData);

            System.Diagnostics.Process.Start($"https://www.tumblr.com/blog/{blogName}/queue");
        }

        private void BtnTag_Click(object sender, EventArgs e)
        {
            DialogAddTag dlg = new DialogAddTag(tags);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tags.Add(dlg.Result);

                RefreshTags();
            }
        }

        private void RefreshTags()
        {
            TbTags.Text = tags.ToString();
        }

    }
}
