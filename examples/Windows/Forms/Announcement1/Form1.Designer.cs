namespace Announcement1
{
    partial class Form1
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
            this.ConsumerKey = new System.Windows.Forms.TextBox();
            this.ConsumerSecret = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.AccessKey = new System.Windows.Forms.TextBox();
            this.AccessSecret = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ConsumerKey
            // 
            this.ConsumerKey.Location = new System.Drawing.Point(12, 27);
            this.ConsumerKey.Name = "ConsumerKey";
            this.ConsumerKey.Size = new System.Drawing.Size(424, 20);
            this.ConsumerKey.TabIndex = 0;
            this.ConsumerKey.Text = "hGSKqgb24RJBnWkodL5GTFIeadgyOnWl0qsXi7APRC76HELnrE";
            // 
            // ConsumerSecret
            // 
            this.ConsumerSecret.Location = new System.Drawing.Point(12, 70);
            this.ConsumerSecret.Name = "ConsumerSecret";
            this.ConsumerSecret.Size = new System.Drawing.Size(424, 20);
            this.ConsumerSecret.TabIndex = 1;
            this.ConsumerSecret.Text = "jdNWSSbG7bZ8tYJcYzmyfH33o5cq7ihmJeWMVntB3pUHNptqn3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ConsumerKey";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ConsumerSecret";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(170, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "AccessKey";
            // 
            // AccessKey
            // 
            this.AccessKey.Location = new System.Drawing.Point(12, 138);
            this.AccessKey.Name = "AccessKey";
            this.AccessKey.Size = new System.Drawing.Size(424, 20);
            this.AccessKey.TabIndex = 6;
            // 
            // AccessSecret
            // 
            this.AccessSecret.Location = new System.Drawing.Point(12, 184);
            this.AccessSecret.Name = "AccessSecret";
            this.AccessSecret.Size = new System.Drawing.Size(424, 20);
            this.AccessSecret.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "AccessSecret";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 223);
            this.Controls.Add(this.AccessSecret);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AccessKey);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConsumerSecret);
            this.Controls.Add(this.ConsumerKey);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ConsumerKey;
        private System.Windows.Forms.TextBox ConsumerSecret;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox AccessKey;
        private System.Windows.Forms.TextBox AccessSecret;
        private System.Windows.Forms.Label label4;
    }
}

