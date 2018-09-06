using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TumblrSharp.Samples.Common.Dialog
{
    public partial class DlgSetToken : Form
    {
        public DlgSetToken()
        {
            InitializeComponent();
        }

        public string ConsumerKey { get; private set; }
        public string ConsumerSecret { get; private set; }
        public string AccessKey { get; private set; }
        public string AccessSecret { get; private set; }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if ( tbConsumerKey.Text != string.Empty && tbConsumerSecret.Text != string.Empty && tbAccessKey.Text != string.Empty && tbAccessSecret.Text != string.Empty)
            {
                ConsumerKey = tbConsumerKey.Text;
                ConsumerSecret = tbConsumerSecret.Text;
                AccessKey = tbAccessKey.Text;
                AccessSecret = tbAccessSecret.Text;

                Close();
            }
        }

        private void DlgSetToken_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(tbConsumerKey.Text != string.Empty && tbConsumerSecret.Text != string.Empty && tbAccessKey.Text != string.Empty && tbAccessSecret.Text != string.Empty))
            {
                e.Cancel = true;
            }
        }
    }
}
