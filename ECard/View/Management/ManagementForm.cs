using ECard.Management.Image;
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
    /// 管理画面
    /// </summary>
    public partial class ManagementForm : Form
    {
        /// <summary>
        /// 管理者ログインのグローバル変数宣言
        /// </summary>
        private int ManagementLogin = 2;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ManagementForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ユーザー管理ボタンクリックイベント
        /// </summary>
        /// <param name="sender">イベントを発生させたオブジェクト</param>
        /// <param name="e">クリックイベント</param>
        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            // ユーザー画面を初期化
            var userForm = new UserForm();

            // 画面をモードレスで表示
            userForm.Show();
        }

        /// <summary>
        /// 画像管理ボタンクリックイベント
        /// </summary>
        /// <param name="sender">イベントを発生させたオブジェクト</param>
        /// <param name="e">クリックイベント</param>
        private void btnImage_Click(object sender, EventArgs e)
        {
            // 画像設定画面を初期化
            var userForm = new ImageForm(ManagementLogin);

            // 画面をモードレスで表示
            userForm.Show();
        }
    }
}
