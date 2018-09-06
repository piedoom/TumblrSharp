namespace Post_Photo_Queue
{
    partial class FrMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbBlogs = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TbCaption = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.DlgAddPhoto = new System.Windows.Forms.OpenFileDialog();
            this.PhotoView = new System.Windows.Forms.FlowLayoutPanel();
            this.TbDateTime = new System.Windows.Forms.TextBox();
            this.CbDateTime = new System.Windows.Forms.CheckBox();
            this.BtnPost = new System.Windows.Forms.Button();
            this.TbTags = new System.Windows.Forms.TextBox();
            this.lTags = new System.Windows.Forms.Label();
            this.BtnTag = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbBlogs
            // 
            this.cbBlogs.FormattingEnabled = true;
            this.cbBlogs.Location = new System.Drawing.Point(12, 23);
            this.cbBlogs.Name = "cbBlogs";
            this.cbBlogs.Size = new System.Drawing.Size(505, 21);
            this.cbBlogs.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Blog:";
            // 
            // TbCaption
            // 
            this.TbCaption.Location = new System.Drawing.Point(12, 79);
            this.TbCaption.Name = "TbCaption";
            this.TbCaption.Size = new System.Drawing.Size(505, 20);
            this.TbCaption.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Caption:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Photo:";
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(12, 308);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 23);
            this.BtnAdd.TabIndex = 6;
            this.BtnAdd.Text = "Add";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // DlgAddPhoto
            // 
            this.DlgAddPhoto.FileName = "openFileDialog1";
            this.DlgAddPhoto.Filter = "Picture|*.jpg;*.png;*.gif; *.bmp";
            this.DlgAddPhoto.FileOk += new System.ComponentModel.CancelEventHandler(this.DlgAddPhoto_FileOk);
            // 
            // PhotoView
            // 
            this.PhotoView.AutoScroll = true;
            this.PhotoView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PhotoView.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.PhotoView.Location = new System.Drawing.Point(12, 141);
            this.PhotoView.Name = "PhotoView";
            this.PhotoView.Size = new System.Drawing.Size(502, 161);
            this.PhotoView.TabIndex = 7;
            // 
            // TbDateTime
            // 
            this.TbDateTime.Location = new System.Drawing.Point(12, 429);
            this.TbDateTime.Name = "TbDateTime";
            this.TbDateTime.Size = new System.Drawing.Size(502, 20);
            this.TbDateTime.TabIndex = 9;
            // 
            // CbDateTime
            // 
            this.CbDateTime.AutoSize = true;
            this.CbDateTime.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CbDateTime.Location = new System.Drawing.Point(12, 406);
            this.CbDateTime.Name = "CbDateTime";
            this.CbDateTime.Size = new System.Drawing.Size(114, 17);
            this.CbDateTime.TabIndex = 10;
            this.CbDateTime.Text = "Time for publishing";
            this.CbDateTime.UseVisualStyleBackColor = true;
            // 
            // BtnPost
            // 
            this.BtnPost.Location = new System.Drawing.Point(12, 472);
            this.BtnPost.Name = "BtnPost";
            this.BtnPost.Size = new System.Drawing.Size(75, 23);
            this.BtnPost.TabIndex = 11;
            this.BtnPost.Text = "post";
            this.BtnPost.UseVisualStyleBackColor = true;
            this.BtnPost.Click += new System.EventHandler(this.BtnPost_Click);
            // 
            // TbTags
            // 
            this.TbTags.Enabled = false;
            this.TbTags.Location = new System.Drawing.Point(12, 371);
            this.TbTags.Name = "TbTags";
            this.TbTags.Size = new System.Drawing.Size(415, 20);
            this.TbTags.TabIndex = 12;
            // 
            // lTags
            // 
            this.lTags.AutoSize = true;
            this.lTags.Location = new System.Drawing.Point(12, 355);
            this.lTags.Name = "lTags";
            this.lTags.Size = new System.Drawing.Size(34, 13);
            this.lTags.TabIndex = 13;
            this.lTags.Text = "Tags:";
            // 
            // BtnTag
            // 
            this.BtnTag.Location = new System.Drawing.Point(442, 371);
            this.BtnTag.Name = "BtnTag";
            this.BtnTag.Size = new System.Drawing.Size(72, 23);
            this.BtnTag.TabIndex = 14;
            this.BtnTag.Text = "Add";
            this.BtnTag.UseVisualStyleBackColor = true;
            this.BtnTag.Click += new System.EventHandler(this.BtnTag_Click);
            // 
            // FrMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 517);
            this.Controls.Add(this.BtnTag);
            this.Controls.Add(this.lTags);
            this.Controls.Add(this.TbTags);
            this.Controls.Add(this.BtnPost);
            this.Controls.Add(this.CbDateTime);
            this.Controls.Add(this.TbDateTime);
            this.Controls.Add(this.PhotoView);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TbCaption);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbBlogs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrMain";
            this.Text = "PhotoPost as queue";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbBlogs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TbCaption;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.OpenFileDialog DlgAddPhoto;
        private System.Windows.Forms.FlowLayoutPanel PhotoView;
        private System.Windows.Forms.TextBox TbDateTime;
        private System.Windows.Forms.CheckBox CbDateTime;
        private System.Windows.Forms.Button BtnPost;
        private System.Windows.Forms.TextBox TbTags;
        private System.Windows.Forms.Label lTags;
        private System.Windows.Forms.Button BtnTag;
    }
}

