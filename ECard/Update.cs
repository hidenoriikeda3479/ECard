using ECard.Common;
using System;
using System.Collections;
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
    /// ユーザー編集画面
    /// </summary>
    public partial class Update : Form
    {
        public Update(string username)
        {
            InitializeComponent();

            txtUser.Text = username;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            // DBの接続情報
            var dbHelper = new DatabaseHelper();

            // 接続を開く
            var con = dbHelper.OpenConnection();

            var sql = $"SELECT * FROM ECard Where 1 = 1";

            // SQL
            string query = String.Format("UPDATE ECard SET UserName = '{0}',  = '{1}'", txtUser.Text, txtPswrd.Text);

            // SQL時以降
            DataTable result = dbHelper.ExecuteQuery(con, query);
            dbHelper.ExecuteQuery(con, sql);

            // 情報が全て入力されている
            if (txtUser.Text != "" || txtPswrd.Text != "" || txtChan.Text != "" || txtCon.Text != "")
            {
                MessageBox.Show("更新が完了しました。"+ DateTime.Now);
            }
            else
            {
                MessageBox.Show("必要な情報が入力されていません");
            }         
            return;
        }
    }
}
