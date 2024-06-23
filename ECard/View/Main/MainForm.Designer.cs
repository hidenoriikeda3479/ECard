namespace ECard
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnMatch = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAccountName = new System.Windows.Forms.Label();
            this.btnManagement = new System.Windows.Forms.Button();
            this.dgvRanking = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRanking)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(305, 46);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(91, 36);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "ログアウト";
            this.btnLogout.UseVisualStyleBackColor = true;
            // 
            // btnMatch
            // 
            this.btnMatch.Location = new System.Drawing.Point(19, 46);
            this.btnMatch.Name = "btnMatch";
            this.btnMatch.Size = new System.Drawing.Size(91, 36);
            this.btnMatch.TabIndex = 1;
            this.btnMatch.Text = "対戦";
            this.btnMatch.UseVisualStyleBackColor = true;
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(114, 46);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(91, 36);
            this.btnRecord.TabIndex = 1;
            this.btnRecord.Text = "成績表";
            this.btnRecord.UseVisualStyleBackColor = true;
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(211, 46);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(91, 36);
            this.btnSetting.TabIndex = 1;
            this.btnSetting.Text = "設定";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Account：";
            // 
            // lblAccountName
            // 
            this.lblAccountName.AutoSize = true;
            this.lblAccountName.Location = new System.Drawing.Point(328, 20);
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Size = new System.Drawing.Size(47, 12);
            this.lblAccountName.TabIndex = 3;
            this.lblAccountName.Text = "XXXXXX";
            // 
            // btnManagement
            // 
            this.btnManagement.Location = new System.Drawing.Point(16, 12);
            this.btnManagement.Name = "btnManagement";
            this.btnManagement.Size = new System.Drawing.Size(188, 28);
            this.btnManagement.TabIndex = 1;
            this.btnManagement.Text = "管理";
            this.btnManagement.UseVisualStyleBackColor = true;
            this.btnManagement.Click += new System.EventHandler(this.btnManagement_Click);
            // 
            // dgvRanking
            // 
            this.dgvRanking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRanking.Location = new System.Drawing.Point(16, 108);
            this.dgvRanking.Name = "dgvRanking";
            this.dgvRanking.RowTemplate.Height = 21;
            this.dgvRanking.Size = new System.Drawing.Size(380, 201);
            this.dgvRanking.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ranking";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 318);
            this.Controls.Add(this.dgvRanking);
            this.Controls.Add(this.lblAccountName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnManagement);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnMatch);
            this.Controls.Add(this.btnLogout);
            this.Name = "MainForm";
            this.Text = "メイン";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRanking)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnMatch;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAccountName;
        private System.Windows.Forms.Button btnManagement;
        private System.Windows.Forms.DataGridView dgvRanking;
        private System.Windows.Forms.Label label3;
    }
}

