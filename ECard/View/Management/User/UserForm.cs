using ECard.Common;
using ECard.View.Management.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

// TODOリスト

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
            // 検索処理
            SearchBtn();
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

        /// <summary>
        /// グリッドのボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ActionColumn")
            {
                // 更新ボタン
                UpdateBtn(e);
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "deleteBtn")
            {
                // 削除ボタン
                DeleteBtn(e);
            }
        }

        /// <summary>
        /// 削除実行時の成否イベント
        /// </summary>
        /// <param name="id"></param>
        private void DeleteData(int id)
        {
            var dbHelper = new DatabaseHelper();
            using (var con = dbHelper.OpenConnection())
            {
                try
                {
                    // SQLクエリ
                    string query = "DELETE FROM users WHERE user_id = @UserId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", id);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            // DataGridViewからも行を削除
                            MessageBox.Show("データが削除されました。");
                            SearchBtn();
                        }
                        else
                        {
                            MessageBox.Show("データ削除に失敗しました。");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("エラーが発生しました: " + ex.Message);
                }
            }
        }
        #region イベント一覧
        /// <summary>
        /// 検索ボタンクリックイベント
        /// </summary>
        private void SearchBtn()
        {
            // dataGridViewクリア
            dataGridView1.Columns.Clear();

            // DBの接続情報
            var dbHelper = new DatabaseHelper();

            // 接続を開く
            var con = dbHelper.OpenConnection();

            /// SQLクエリ
            string sql = $"SELECT * FROM users Where 1 = 1";

            // 検索
            sql += SearchCheck();

            DataTable result = dbHelper.ExecuteQuery(con, sql);

            // データグリッドビュー列作成
            DataGridView();

            dataGridView1.DataSource = DataRef(result);
        }

        /// <summary>
        /// DataGridView列作成メソッド
        /// </summary>
        private void DataGridView()
        {
            // 更新ボタン列を作成
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "更新"; // 列のヘッダーテキスト
            buttonColumn.Name = "ActionColumn"; // 列の名前
            buttonColumn.Text = "編集"; // ボタンに表示されるテキスト
            buttonColumn.UseColumnTextForButtonValue = true; //全てのボタンに"編集"と表示されます

            // 削除ボタン列を作成
            DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
            deleteBtn.HeaderText = "データ削除"; // 列のヘッダーテキスト
            deleteBtn.Name = "deleteBtn"; // 列の名前
            deleteBtn.Text = "削除"; // ボタンに表示されるテキスト
            deleteBtn.UseColumnTextForButtonValue = true; // 全てのボタンに"削除"と表示されます

            // dataGridView1の最初の列としてボタン列を追加
            dataGridView1.Columns.Insert(0, buttonColumn);

            // dataGridView1の最初の列としてボタン列を追加
            dataGridView1.Columns.Insert(1, deleteBtn);
        }

        /// <summary>
        /// 検索イベント
        /// </summary>
        private string SearchCheck()
        {
            // ユーザー名が入力されている
            if (txtUser.Text != "")
            {
                return $" And username Like '%{txtUser.Text}%'";
            }

            // チェックの有無
            if (checkBox1.Checked)
            {
                // 登録日から検索
                return $" And CONVERT(date, created_at) = '{dateTimePicker.Value.ToString("yyyy/MM/dd")}'";
            }
            return string .Empty ;
        }

        /// <summary>
        /// DataGridView反映イベント
        /// </summary>
        private List<UserViewModel> DataRef(DataTable result)
        {
            List<UserViewModel> list = new List<UserViewModel>();

            // データテーブルをデータグリッドビューへ反映
            foreach (DataRow row in result.Rows)
            {
                UserViewModel model = new UserViewModel();

                model.UserId = int.Parse(row["user_id"].ToString());
                model.UserName = row["username"]?.ToString();
                model.CreatedAt = DateTime.Parse(row["created_at"].ToString());

                // nullの有無を確認
                if (model.UpdateAt != null)
                {
                    model.UpdateAt = DateTime.Parse(row["update_at"].ToString());
                }
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 更新ボタンイベント
        /// </summary>
        private void UpdateBtn(DataGridViewCellEventArgs e)
        {
                // 押された行のユーザー名、IDを取得する。
                var userId = dataGridView1.Rows[e.RowIndex].Cells["UserId"].Value;
                var username = dataGridView1.Rows[e.RowIndex].Cells["UserName"].Value;

                Update Update = new Update(userId.ToString(), username.ToString());
                Update.Show();
        }

        /// <summary>
        /// 削除ボタンイベント
        /// </summary>
        private void DeleteBtn(DataGridViewCellEventArgs e)
        {
                if (MessageBox.Show("この行を削除してもよろしいですか？", "確認",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // 削除する行の主キーを取得
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["UserId"].Value);
                　　DeleteData(id);
    　     　   }
            }
        }
        #endregion
    }