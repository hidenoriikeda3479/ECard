using ECard.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECard.User
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 検索ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSae_Click(object sender, EventArgs e)
        {
            // SQL
            string sql = $"SELECT * FROM  ECard Where 1 = 1";

            // ユーザー名が入力されている
            if (txtUser.Text != "")
            {
                sql += $" And username Like '%{txtUser.Text}%'";
            }
            // アカウント作成日が入力されている
            if (dateTimePicker.Value != null)
            {
                sql += $" And created_at = '{dateTimePicker.Value.ToString("yyyy-MM-dd")}'";
            }
        }

        /// <summary>
        /// 登録ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLan_Click_1(object sender, EventArgs e)
        {
            RegistrationForm RegistrationForm = new RegistrationForm();
            RegistrationForm.Show();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
