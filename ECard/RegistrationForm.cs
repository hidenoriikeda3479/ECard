using ECard.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ECard
{
    /// <summary>
    /// ユーザー登録画面
    /// </summary>
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 登録ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLan_Click(object sender, EventArgs e)
        {

            
            // 何も入力されていな場合、エラーメッセージを表示する
            if (string.IsNullOrEmpty(txtUser.Text + txtPswrd.Text + txtPswrdcon.Text))
            {
                MessageBox.Show("入力されていない箇所があります");
                return;
            }

            // パスワード確認の合否
            if(txtPswrd.Text != txtPswrdcon.Text)
            {
                MessageBox.Show("パスワードが合っていません");
                return;
            }
            

            // DBの接続情報
            var dbHelper = new DatabaseHelper();

            // 接続を開く
            var con = dbHelper.OpenConnection();

            // SQL
            string asd = ($"SELECT * FROM users Where username = '{txtUser.Text}'");

            // SQL時以降
            dbHelper.ExecuteQuery(con, asd);
            var asa = dbHelper.ExecuteQuery(con, asd);

            // ユーザー名の重複チェック
            if (asa.Rows.Count > 0)
            {
                MessageBox.Show("既に使用されている名前です");
                return;
            }
            else
            {
                MessageBox.Show("登録完了しました");
            }

            // ハッシュ化ヘルパー
            string hsps = HashHelper.sha512(txtPswrd.Text);

            // SQL
            string query = String.Format("INSERT INTO users (username, password_hash, created_at) " +
                "VALUES ('{0}','{1}','{2}')", txtUser.Text, hsps, DateTime.Now);

            // SQL時以降
            dbHelper.ExecuteQuery(con, query);
        }
    }
}
