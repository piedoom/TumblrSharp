namespace TumblrSharp.Samples.Common.Dialog
{
    partial class DlgSetToken
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbConsumerKey = new PlaceholderTextBox.PlaceholderTextBox();
            this.tbConsumerSecret = new PlaceholderTextBox.PlaceholderTextBox();
            this.tbAccessKey = new PlaceholderTextBox.PlaceholderTextBox();
            this.tbAccessSecret = new PlaceholderTextBox.PlaceholderTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbConsumerKey
            // 
            this.tbConsumerKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbConsumerKey.Location = new System.Drawing.Point(12, 25);
            this.tbConsumerKey.Name = "tbConsumerKey";
            this.tbConsumerKey.PlaceholderText = "<ConsumerKey>";
            this.tbConsumerKey.Size = new System.Drawing.Size(360, 20);
            this.tbConsumerKey.TabIndex = 0;
            // 
            // tbConsumerSecret
            // 
            this.tbConsumerSecret.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbConsumerSecret.Location = new System.Drawing.Point(12, 74);
            this.tbConsumerSecret.Name = "tbConsumerSecret";
            this.tbConsumerSecret.PlaceholderText = "<ConsumerSecret>";
            this.tbConsumerSecret.Size = new System.Drawing.Size(360, 20);
            this.tbConsumerSecret.TabIndex = 1;
            // 
            // tbAccessKey
            // 
            this.tbAccessKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAccessKey.Location = new System.Drawing.Point(12, 127);
            this.tbAccessKey.Name = "tbAccessKey";
            this.tbAccessKey.PlaceholderText = "<AccessKey>";
            this.tbAccessKey.Size = new System.Drawing.Size(360, 20);
            this.tbAccessKey.TabIndex = 2;
            // 
            // tbAccessSecret
            // 
            this.tbAccessSecret.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAccessSecret.Location = new System.Drawing.Point(12, 181);
            this.tbAccessSecret.Name = "tbAccessSecret";
            this.tbAccessSecret.PlaceholderText = "<AccessSecret>";
            this.tbAccessSecret.Size = new System.Drawing.Size(360, 20);
            this.tbAccessSecret.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "ConsumerKey";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "ConsumerSecret";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "AccessKey";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "AccessSecret";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(155, 220);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // DlgSetToken
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 255);
            this.ControlBox = false;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbAccessSecret);
            this.Controls.Add(this.tbAccessKey);
            this.Controls.Add(this.tbConsumerSecret);
            this.Controls.Add(this.tbConsumerKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DlgSetToken";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Input Your Token";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DlgSetToken_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PlaceholderTextBox.PlaceholderTextBox tbConsumerKey;
        private PlaceholderTextBox.PlaceholderTextBox tbConsumerSecret;
        private PlaceholderTextBox.PlaceholderTextBox tbAccessKey;
        private PlaceholderTextBox.PlaceholderTextBox tbAccessSecret;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOk;
    }
}