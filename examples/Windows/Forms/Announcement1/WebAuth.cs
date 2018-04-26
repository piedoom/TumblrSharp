using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Announcement1
{
    public partial class WebAuth : Form
    {
        private Uri startUrl;

        public WebAuth()
        {
            InitializeComponent();
        }

        public Uri StartUrl
        {
            get
            {
                return startUrl;
            }
            set
            {
                if (value != null)
                {
                    startUrl = value;
                    webBrowser1.Url = value;
                }
            }
        }

        public string CallBackUrl { get; set; }

        public Uri Result { get; set; }

        public static Uri ShowDialog(Uri url, string callBackUrl)
        {
            Uri result = null;

            using (WebAuth wa = new WebAuth())
            {
                wa.StartUrl = url;
                wa.CallBackUrl = callBackUrl;
                wa.ShowDialog();

                result = wa.Result;
            }

            return result;
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            Uri url = webBrowser1.Url;

            if (url.OriginalString.StartsWith(CallBackUrl) == true)
            {
                Result = url;
                Close();
            }
        }
    }
}
