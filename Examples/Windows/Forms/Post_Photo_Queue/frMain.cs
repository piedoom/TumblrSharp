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

namespace Post_Photo
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

            int idx = 0;

            foreach (var file in dlg.FileNames)
            {
                LoadPictureFile(file);
                idx++;

                if (idx == 10) break;
            }            
        }

        private void LoadPictureFile(string file)
        {
            byte[] array;

            MemoryStream arrayStream = new MemoryStream();

            using (var stream = new FileStream(file, FileMode.Open))
            {
                stream.CopyTo(arrayStream);
            }

            array = arrayStream.ToArray();

            binaryFiles.Add(new BinaryFile(array));
            
            Image bild = new Bitmap(arrayStream);

            PictureBox pictureBox = new PictureBox()
            {
                Height = 150,
                Width = 150,
                Image = bild.GetThumbnailImage(150, 150, null, IntPtr.Zero),
                Tag = binaryFiles.Count

            };

            PhotoView.Controls.Add(pictureBox);
        }

        private void BtnTag_Click(object sender, EventArgs e)
        {
            DialogAddTag dlg = new DialogAddTag(this, tags);

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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            PhotoView.Controls.Remove(PhotoView.Controls[PhotoView.Controls.Count - 1]);
            binaryFiles.Remove(binaryFiles[binaryFiles.Count - 1]);
        }

        private void PhotoView_ControlAdded(object sender, ControlEventArgs e)
        {
            BtnDelete.Enabled = (sender as FlowLayoutPanel).Controls.Count > 0;
            BtnAdd.Enabled = (sender as FlowLayoutPanel).Controls.Count < 10;
        }

        private void PhotoView_ControlRemoved(object sender, ControlEventArgs e)
        {
            BtnDelete.Enabled = (sender as FlowLayoutPanel).Controls.Count > 0;
            BtnAdd.Enabled = (sender as FlowLayoutPanel).Controls.Count < 10;
        }

        private async void BtnPostAsQueue_Click(object sender, EventArgs e)
        {
            EnabledForm(false);

            string blogName = cbBlogs.Text;

            string caption = TbCaption.Text;

            PostData postData = PostData.CreatePhoto(binaryFiles, caption, null, tags.ToList(), PostCreationState.Queue);

            if (CbDateTime.Checked)
            {
                postData.Publish_On = Convert.ToDateTime(TbDateTime.Text);
            }
            
            var postCreationInfo = await TumblrClient.CreatePostAsync(blogName, postData);

            System.Diagnostics.Process.Start($"https://www.tumblr.com/blog/{blogName}/queue");

            EnabledForm(true);
        }

        private async void BtnPost_Click(object sender, EventArgs e)
        {
            EnabledForm(false);
            
            string blogName = cbBlogs.Text;

            string caption = TbCaption.Text;

            PostData postData = PostData.CreatePhoto(binaryFiles, caption, null, tags.ToList());

            var postCreationInfo = await TumblrClient.CreatePostAsync(blogName, postData);

            System.Diagnostics.Process.Start($"https://www.tumblr.com/blog/{blogName}");

            EnabledForm(true);
        }

        private void CbDateTime_CheckedChanged(object sender, EventArgs e)
        {
            TbDateTime.Enabled = (sender as CheckBox).Checked == true;
        }

        private void EnabledForm(bool enabled)
        {
            foreach (var control in this.Controls)
            {
                if (control is Control)
                {
                    (control as Control).Enabled = enabled;
                }
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            PhotoView.Controls.Clear();
            binaryFiles.Clear();
            tags.Clear();

            TbCaption.Text = "";
            TbTags.Text = "";
        }
    }
}
