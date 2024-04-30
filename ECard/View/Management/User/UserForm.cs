using ECard.Common;
using ECard.View.Management.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ECard.User
{
    /// <summary>
    /// ユーザー登録、編集画面
    /// </summary>
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
            dataGridView1.Columns.Clear();

            // DBの接続情報
            var dbHelper = new DatabaseHelper();

            // 接続を開く
            var con = dbHelper.OpenConnection();

            // SQL
            string sql = $"SELECT * FROM users Where 1 = 1";
            dbHelper.ExecuteQuery(con, sql);

            // ボタン列を作成
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "更新"; // 列のヘッダーテキスト
            buttonColumn.Name = "ActionColumn"; // 列の名前
            buttonColumn.Text = "編集"; // ボタンに表示されるテキスト
            buttonColumn.UseColumnTextForButtonValue = true;

            // dataGridView1の最初の列としてボタン列を追加
            dataGridView1.Columns.Insert(0, buttonColumn);

            // ユーザー名が入力されている
            if (txtUser.Text != "")
            {
                sql += $" And username Like '%{txtUser.Text}%'";
            }
            // アカウント作成日が入力されている
            if (checkBox1.Checked)
            {
                sql += $" And created_at = '{dateTimePicker.Value.ToString("yyyy-MM-dd")}'";
            }

            // SQL時以降
            DataTable result = dbHelper.ExecuteQuery(con, sql);

            List<UserViewModel> list = new List<UserViewModel>();

            foreach (DataRow row in result.Rows)
            {
                UserViewModel model = new UserViewModel();


                model.UserID = int.Parse(row["user_id"].ToString());
                model.UserName = row["username"]?.ToString();
                model.CreatedAt = DateTime.Parse(row["created_at"].ToString());

                if (model.UpdateAt != null)
                {
                    model.UpdateAt = DateTime.Parse(row["update_at"].ToString());
                }
                    


                list.Add(model);
            }
            // TODO ; グリッドに値を当てはめる
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

        /// <summary>
        /// グリッドのボタンを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /* DataGridView dgv = (DataGridView)sender;
             //"Button"列ならば、ボタンがクリックされた
             if (dgv.Columns[e.ColumnIndex].Name == "Button")
             {
                 // 押された行のボタンのユーザー名を取得する。
                 var user = dataGridView1.Rows[e.RowIndex].Cells["users"].Value;

                 Update UpdateForm = new Update(user.ToString());
                 UpdateForm.Show();*/
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ActionColumn")
            {
                // 押された行のユーザー名を取得する。
                var username = dataGridView1.Rows[e.RowIndex].Cells["username"].Value;

                Update Update = new Update(username.ToString());
                Update.Show();

            }
        }
    }
}
