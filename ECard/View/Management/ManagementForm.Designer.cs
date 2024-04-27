namespace ECard
{
    partial class ManagementForm
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
            this.btnUserManagement = new System.Windows.Forms.Button();
            this.btnImage = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUserManagement
            // 
            this.btnUserManagement.Location = new System.Drawing.Point(27, 27);
            this.btnUserManagement.Name = "btnUserManagement";
            this.btnUserManagement.Size = new System.Drawing.Size(233, 42);
            this.btnUserManagement.TabIndex = 0;
            this.btnUserManagement.Text = "ユーザー管理";
            this.btnUserManagement.UseVisualStyleBackColor = true;
            this.btnUserManagement.Click += new System.EventHandler(this.btnUserManagement_Click);
            // 
            // btnImage
            // 
            this.btnImage.Location = new System.Drawing.Point(27, 89);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(233, 42);
            this.btnImage.TabIndex = 0;
            this.btnImage.Text = "画像管理";
            this.btnImage.UseVisualStyleBackColor = true;
            this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(27, 151);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(233, 42);
            this.button3.TabIndex = 0;
            this.button3.Text = "XXXXXX";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // ManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 227);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnImage);
            this.Controls.Add(this.btnUserManagement);
            this.Name = "ManagementForm";
            this.Text = "管理";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUserManagement;
        private System.Windows.Forms.Button btnImage;
        private System.Windows.Forms.Button button3;
    }
}