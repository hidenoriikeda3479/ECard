namespace ECard.View.Management
{
    partial class SettingForm
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
            this.btnImageForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnImageForm
            // 
            this.btnImageForm.Location = new System.Drawing.Point(106, 35);
            this.btnImageForm.Name = "btnImageForm";
            this.btnImageForm.Size = new System.Drawing.Size(217, 61);
            this.btnImageForm.TabIndex = 0;
            this.btnImageForm.Text = "画像一覧";
            this.btnImageForm.UseVisualStyleBackColor = true;
            this.btnImageForm.Click += new System.EventHandler(this.btnImageForm_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 450);
            this.Controls.Add(this.btnImageForm);
            this.Name = "SettingForm";
            this.Text = "設定";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImageForm;
    }
}