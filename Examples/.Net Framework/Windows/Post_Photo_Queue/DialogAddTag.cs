using TumblrSharp.Extension.Tags;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Post_Photo
{
    public partial class DialogAddTag : Form
    {
        private Tags tags;

        public DialogAddTag(Form parent, Tags tags)
        {
            InitializeComponent();

            Owner = parent;

            this.tags = tags;

            tbTag.AutoCompleteCustomSource = new AutoCompleteStringCollection();

            tbTag.AutoCompleteCustomSource.AddRange(tags.GetLookupList().ToArray());

            tbTag.AutoCompleteSource = AutoCompleteSource.CustomSource;

            tbTag.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            
        }

        public string Result { get; private set; }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Result = tbTag.Text;

            Close();
        }

        private async void tbTag_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (tbTag.Text != string.Empty && tbTag.Text.Length > 2)
            {
                List<string> result = await tags.LookupTag(tbTag.Text);
                if (result.Count > 0)
                {
                    tbTag.AutoCompleteCustomSource.AddRange(result.ToArray());
                }
            }
        }
    }
}
