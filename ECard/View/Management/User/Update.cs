using ECard.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECard
{
    /// <summary>
    /// ユーザー編集画面
    /// </summary>
    public partial class Update : Form
    {
        private string UserId;

        public Update(string userid, string username)
        {
            InitializeComponent();
            txtUser.Text = username;
            UserId = userid; // ユーザーIDを保存
        }

        /// <summary>
        /// クリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            // 入力値の検証
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtChanps.Text))
            {
                MessageBox.Show("必要な情報が入力されていません");
                return;
            }

            // DBの接続情報
            var dbHelper = new DatabaseHelper();
            using (var con = dbHelper.OpenConnection())
            {
                try
                {
                    // SQL
                    string query = "UPDATE users SET username = @UserName, password_hash = @Chanps, update_at = @UpdateAt WHERE user_id = @UserId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserName", txtUser.Text);
                        cmd.Parameters.AddWithValue("@Chanps", txtChanps.Text);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@UpdateAt", DateTime.Now);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("更新が完了しました。" + DateTime.Now);

                            // フォームを閉じる
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("更新に失敗しました。");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("エラーが発生しました: " + ex.Message);
                }
            }
        }
    }
}