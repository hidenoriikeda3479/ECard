using ECard.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECard
{
    /// <summary>
    /// メイン画面
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            // 画面情報を初期化
            InitializeComponent();
        }

        /// <summary>
        /// 管理者ボタンクリックイベント
        /// </summary>
        /// <param name="sender">イベントを発生させたオブジェクト</param>
        /// <param name="e">クリックイベント</param>
        private void btnManagement_Click(object sender, EventArgs e)
        {
            // 管理画面を初期化
            var managementForm = new ManagementForm();

            // 画面をモードレスで表示
            managementForm.Show();
        }
    }
}
