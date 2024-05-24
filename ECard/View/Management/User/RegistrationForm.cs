using ECard.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            UserCon();
        }

        #region イベント一覧

        /// <summary>
        /// ユーザー登録イベント
        /// </summary>
        private void UserCon()
        {
            if (!InputCheck() || !PasCheck())
            {
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
            }
            else
            {
                // ユーザー登録処理
                UserElse(); 
            }
        }

        /// <summary>
        /// 入力Checkイベント
        /// </summary>
        /// <returns></returns>
        private bool InputCheck()
        {
            // ユーザー名、パスワード、パスワード確認のいずれかが空白の場合、エラーメッセージを表示する
            if (string.IsNullOrEmpty(txtUser.Text + txtPswrd.Text + txtPswrdcon.Text))
            {
                MessageBox.Show("入力されていない箇所があります");
                return false;
            }
            return true;
        }

        /// <summary>
        /// パスワード確認イベント
        /// </summary>
        /// <returns></returns>
        private bool PasCheck()
        {
            // パスワード確認の合否
            if (txtPswrd.Text != txtPswrdcon.Text)
            {
                MessageBox.Show("パスワードが合っていません");
                return false;
            }
            return true;
        }
        /// <summary>
        /// ユーザー登録処理
        /// </summary>
        private void UserElse()
        {
            // DBの接続情報
            var dbHelper = new DatabaseHelper();

            // 接続を開く
            var con = dbHelper.OpenConnection();

            // 登録確認
            var result = MessageBox.Show(txtUser.Text + "でよろしいですか？", "確認",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // はいを選択した場合
            if (result == DialogResult.Yes)
            {
                // ハッシュ化ヘルパー
                string hsps = HashHelper.sha512(txtPswrd.Text);

                // SQL
                string query = String.Format("INSERT INTO users (username, password_hash, created_at) " +
                    "VALUES ('{0}','{1}','{2}')", txtUser.Text, hsps, DateTime.Now);

                // SQL時以降
                dbHelper.ExecuteQuery(con, query);

                // 登録完了メッセージを表示
                MessageBox.Show("登録完了しました");

                // フォームを閉じる
                this.Close();
            }
            // いいえを選択した場合
            return;
        }
        #endregion
    }
}

