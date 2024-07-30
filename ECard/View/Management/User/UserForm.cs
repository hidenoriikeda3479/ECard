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
using ECard.View.Management.Role;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        /// dataGridViewボタン
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
            if (dataGridView1.Columns[e.ColumnIndex].Name == "additionBtn")
            {
                // 権限付与ボタン
                AdditionBtn(e);
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
                    // SQL
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
                catch (SqlException ex)
                {
                    // 権限が付与されているユーザー
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("権限が付与されている為削除できません");
                    }
                    else
                    {
                        MessageBox.Show("エラーが発生しました: " + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("エラーが発生しました: " + ex.Message);
                }

            }
        }
        #region イベント一覧

        #region 検索イベント
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

            DataTable result = dbHelper.ExecuteQuery(con, sql);

            // データグリッドビュー列作成
            DataGridView();

            // データ表示用の列を作成
            DataColumn();

            // DataTableをリストに変換（マッピング）
            var list = DataRef(result);

            // sql を初期化
            var aa = list.AsEnumerable();

            // 名前
            if (!string.IsNullOrEmpty(txtUser.Text))
            {
                aa = aa.Where(n => n.UserName == txtUser.Text);
            }

            // 作成日
            if (checkBox1.Checked)
            {
                aa = aa.Where(n => n.CreatedAt.Date == dateTimePicker.Value.Date);
            }

            // DataGridViewにデータをバインド
            dataGridView1.DataSource = aa.ToList();
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
            return string.Empty;
        }
        #endregion

        #region DataGridViewイベント
        /// <summary>
        /// DataGridViewボタン作成メソッド
        /// </summary>
        private void DataGridView()
        {
            // 更新ボタン列を作成
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "更新", // 列のヘッダーテキスト
                Name = "ActionColumn", // 列の名前
                Text = "編集", // ボタンに表示されるテキスト
                UseColumnTextForButtonValue = true //全てのボタンに"編集"と表示されます
            };

            // 削除ボタン列を作成
            DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn
            {
                HeaderText = "データ削除", // 列のヘッダーテキスト
                Name = "deleteBtn", // 列の名前
                Text = "削除", // ボタンに表示されるテキスト
                UseColumnTextForButtonValue = true // 全てのボタンに"削除"と表示されます
            };

            // 権限ボタン列を作成
            DataGridViewButtonColumn additionBtn = new DataGridViewButtonColumn
            {
                HeaderText = "権限付与", // 列のヘッダーテキスト
                Name = "additionBtn", // 列の名前
                Text = "権限", // ボタンに表示されるテキスト
                UseColumnTextForButtonValue = true // 全てのボタンに"付与"と表示されます
            };

            // dataGridView1の最初の列としてボタン列を追加
            dataGridView1.Columns.Insert(0, buttonColumn);

            // dataGridView1の二番目の列としてボタン列を追加
            dataGridView1.Columns.Insert(1, deleteBtn);

            // dataGridView1の三番目の列としてボタン列を追加
            dataGridView1.Columns.Insert(2, additionBtn);

        }

        /// <summary>
        /// DataGridView列名表示メソッド
        /// </summary>
        private void DataColumn()
        {
            {
                // ユーザーID列を作成
                DataGridViewTextBoxColumn userIdColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "UserId", // データソースのカラム名
                    HeaderText = "ユーザーID", // 列のヘッダーテキスト
                    Name = "UserId" // 列の名前
                };

                // ユーザー名列を作成
                DataGridViewTextBoxColumn userNameColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "UserName", // データソースのカラム名
                    HeaderText = "ユーザー名", // 列のヘッダーテキスト
                    Name = "UserName" // 列の名前
                };

                // 作成日列を作成
                DataGridViewTextBoxColumn createdAtColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "CreatedAt", // データソースのカラム名
                    HeaderText = "作成日", // 列のヘッダーテキスト
                    Name = "CreatedAt" // 列の名前
                };

                // 更新日列を作成
                DataGridViewTextBoxColumn updatedAtColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "UpdateAt", // データソースのカラム名
                    HeaderText = "更新日", // 列のヘッダーテキスト
                    Name = "UpdateAt" // 列の名前
                };

                // 権限ID列を作成
                DataGridViewTextBoxColumn permisUserIdColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PermisUserId", // データソースのカラム名
                    HeaderText = "権限ID", // 列のヘッダーテキスト
                    Name = "PermisUserId" // 列の名前
                };

                // 権限名列を作成
                DataGridViewTextBoxColumn permisNameColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PermisName", // データソースのカラム名
                    HeaderText = "権限名", // 列のヘッダーテキスト
                    Name = "PermisName" // 列の名前
                };

                // DataGridViewに列を追加
                dataGridView1.Columns.Add(userIdColumn);
                dataGridView1.Columns.Add(userNameColumn);
                dataGridView1.Columns.Add(createdAtColumn);
                dataGridView1.Columns.Add(updatedAtColumn);
                dataGridView1.Columns.Add(permisUserIdColumn);
                dataGridView1.Columns.Add(permisNameColumn);
            }
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
        #endregion

        #region DataGridViewボタンイベント
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

        /// <summary>
        /// 権限追加イベント
        /// </summary>
        private void AdditionBtn(DataGridViewCellEventArgs e)
        {
            // 押された行のユーザー名、IDを取得する。
            var userId = dataGridView1.Rows[e.RowIndex].Cells["UserId"].Value;
            var username = dataGridView1.Rows[e.RowIndex].Cells["UserName"].Value;

            Authority Authority = new Authority(userId.ToString(), username.ToString());
            Authority.Show();
        }
    }
    #endregion

    #endregion
}