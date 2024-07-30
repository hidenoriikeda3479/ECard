using ECard.Common;
using ECard.Model;
using ECard.View.Management.User;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;


namespace ECard.View.Management.Role
{
    /// <summary>
    /// 権限付与、解除画面
    /// </summary>
    public partial class Authority : Form
    {
        private string UserId; // ユーザーIDを保存
        public Authority(string userid, string username)
        {
            InitializeComponent();
            UserId = userid; // ユーザーIDを保存
            NameLbel.Text = username;　//ユーザー名テキスト
        }

        /// <summary>
        /// 権限選択画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Authority_Load(object sender, EventArgs e)
        {
            // dataGridViewクリア
            dataGridView1.Columns.Clear();

            // DBの接続情報
            var dbHelper = new DatabaseHelper();

            // 接続を開く
            var con = dbHelper.OpenConnection();

            // ユーザー権限情報の取得
            string userpSql = "SELECT * FROM user_permissions";
            DataTable userpTable = dbHelper.ExecuteQuery(con, userpSql);

            // 権限情報の取得
            string permissionSql = "SELECT * FROM permissions";
            DataTable permissionTable = dbHelper.ExecuteQuery(con, permissionSql);

            // データグリッドビュー列作成
            DataGridView();

            // データ表示用の列を作成
            DataColumn();

            // DataTableをリストに変換（マッピング）
            var userPermissionsList = DataUserPerm(userpTable);
            var permissionsList = DataPerm(permissionTable);

            // 中間テーブルをユーザーIDで絞り込み
            var UserPermissionsid = userPermissionsList.Where(up => up.UserId.ToString() == UserId);

            // 中間テーブル、権限マスタを結合
            var query = from p in permissionsList
                        join up in UserPermissionsid on p.PermissionId equals up.PermissionId into joined
                        from up in joined.DefaultIfEmpty()
                        select new
                        {
                            up?.UserId,
                            p.PermissionId,
                            p.PermissionName,
                            p.DescriPtion,
                            up?.CreatedAt,
                            p.UpdateAt
                        };

            // 結合結果をリストに変換
            var resultList = query.ToList();

            // DataGridViewにデータをバインド
            dataGridView1.DataSource = resultList;

            // チェックボックスの状態を設定
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var permissionId = (int)row.Cells["PermissionId"].Value;
                if (UserPermissionsid.Any(up => up.PermissionId == permissionId))
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells[0] as DataGridViewCheckBoxCell;
                    checkBoxCell.Value = true;
                }
            }
        }

        /// ユーザーに権限を付与するイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aubtn_Click(object sender, EventArgs e)
        {
            // チェックされている権限を格納
            var stPermissions = new List<int>();

            // チェックボックスがnullでは無い権限を取得
            Check(stPermissions);

            // データベース接続
            var dbHelper = new DatabaseHelper();

            // 権限付与、解除
            using (var con = dbHelper.OpenConnection())
            {
                try
                {
                    // 権限付与イベント
                    InsertEvent(con, stPermissions);

                    // SQL 
                    string selectQuery = "SELECT permission_id FROM user_permissions WHERE user_id = @UserId";

                    // ユーザーの現在の権限IDを格納
                    var acquisitionPermissions = new List<int>();

                    // ユーザーの現在の権限を取得
                    using (SqlCommand com = new SqlCommand(selectQuery, con))
                    {
                        com.Parameters.AddWithValue("@UserId", UserId);

                        // チェックが付いている権限を格納
                        StoreEvent(com, acquisitionPermissions);

                        // チェックが付いていない権限の解除 
                        DeleteEvent(acquisitionPermissions, stPermissions, con);

                        // 権限付与成功
                        MessageBox.Show("権限が更新されました。");
                    }
                }

                // 想定外エラー
                catch (Exception ex)
                {
                    MessageBox.Show("エラーが発生しました: " + ex.Message);
                }
            }
            // データグリッドビューを更新
            Authority_Load(sender, e);
        }

        #region Authorityイベント一覧

        /// <summary>
        /// 権限マスタテーブルイベント
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private List<PermissionsViewModel> DataPerm(DataTable result)
        {
            List<PermissionsViewModel> list = new List<PermissionsViewModel>();

            // データテーブルをデータグリッドビューへ反映
            foreach (DataRow row in result.Rows)
            {
                PermissionsViewModel model = new PermissionsViewModel();

                model.PermissionId = int.Parse(row["permission_id"].ToString());
                model.PermissionName = row["permission_name"]?.ToString();
                model.DescriPtion = row["description"]?.ToString();

                if (row["created_at"] != DBNull.Value)
                {
                    model.CreatedAt = DateTime.Parse(row["created_at"].ToString());
                }
                else
                {
                    model.CreatedAt = null;
                }

                // nullの有無を確認
                if (row["update_at"] != DBNull.Value)
                {
                    model.UpdateAt = DateTime.Parse(row["update_at"].ToString());
                }
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// ユーザー権限イベント
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private List<User_PermissionsViewModel> DataUserPerm(DataTable result)
        {
            List<User_PermissionsViewModel> list = new List<User_PermissionsViewModel>();

            // データテーブルをデータグリッドビューへ反映
            foreach (DataRow row in result.Rows)
            {
                User_PermissionsViewModel model = new User_PermissionsViewModel();

                model.UserId = int.Parse(row["user_id"].ToString());
                model.PermissionId = int.Parse(row["permission_id"].ToString());

                if (row["created_at"] != DBNull.Value)
                {
                    model.CreatedAt = DateTime.Parse(row["created_at"].ToString());
                }
                else
                {
                    model.CreatedAt = null;
                }

                // nullの有無を確認
                if (row["update_at"] != DBNull.Value)
                {
                    model.UpdateAt = DateTime.Parse(row["update_at"].ToString());
                }
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// DataGridViewボタン列作成メソッド
        /// </summary>
        private void DataGridView()
        {
            //CheckBox列を追加する
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn
            {
                HeaderText = "権限選択"
            };

            // dataGridView1の最初の列としてチェックを追加
            dataGridView1.Columns.Add(check);
        }

        /// <summary>
        /// DataGridView列名表示メソッド
        /// </summary>
        private void DataColumn()
        {
            // 権限ID列を作成
            DataGridViewTextBoxColumn PermissionidColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PermissionID", // データソースのカラム名
                HeaderText = "権限ID", // 列のヘッダーテキスト
                Name = "PermissionID" // 列の名前
            };

            // 権限名列を作成
            DataGridViewTextBoxColumn PermissionnameColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PermissionName", // データソースのカラム名
                HeaderText = "権限名", // 列のヘッダーテキスト
                Name = "PermissionName" // 列の名前
            };

            // 権限説明を作成
            DataGridViewTextBoxColumn DescriptionColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Description", // データソースのカラム名
                HeaderText = "説明", // 列のヘッダーテキスト
                Name = "Description" // 列の名前
            };

            // ユーザーID列を作成
            DataGridViewTextBoxColumn UserIdColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UserId", // データソースのカラム名
                HeaderText = "ユーザーID", // 列のヘッダーテキスト
                Name = "UserId" // 列の名前
            };

            // 作成日を作成
            DataGridViewTextBoxColumn CreatedAColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CreatedAt", // データソースのカラム名
                HeaderText = "作成日", // 列のヘッダーテキスト
                Name = "CreatedAt" // 列の名前
            };

            // 更新日を作成
            DataGridViewTextBoxColumn UpdateAtColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UpdateAt", // データソースのカラム名
                HeaderText = "更新日", // 列のヘッダーテキスト
                Name = "UpdateAt" // 列の名前
            };

            // データグリッドビュー列作成
            dataGridView1.Columns.Add(PermissionidColumn);
            dataGridView1.Columns.Add(PermissionnameColumn);
            dataGridView1.Columns.Add(DescriptionColumn);
            dataGridView1.Columns.Add(UserIdColumn);
            dataGridView1.Columns.Add(CreatedAColumn);
            dataGridView1.Columns.Add(UpdateAtColumn);
        }

        #endregion

        #region aubtnイベント一覧

        /// <summary>
        /// 権限を取得イベント
        /// </summary>
        /// <param name="stPermissions"></param>
        private void Check(List<int> stPermissions)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell checkBox = row.Cells[0] as DataGridViewCheckBoxCell;

                if ((bool?)checkBox.Value == true)
                {
                    stPermissions.Add((int)row.Cells["PermissionId"].Value);
                }
            }
        }

        /// <summary>
        /// INSERTイベント
        /// </summary>
        /// <param name="con"></param>
        /// <param name="stPermissions"></param>
        private void InsertEvent(SqlConnection con, List<int> stPermissions)
        {
            // SQL
            string insertQuery = "INSERT INTO user_permissions (user_id, permission_id, created_at) VALUES (@UserId, @PermissionId, @CreatedAt)";

            // 権限を付与
            foreach (int permissionId in stPermissions)
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@PermissionId", permissionId);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 権限格納イベント
        /// </summary>
        /// <param name="com"></param>
        /// <param name="acquisitionPermissions"></param>
        private void StoreEvent(SqlCommand com, List<int> acquisitionPermissions)
        {
            using (SqlDataReader dr = com.ExecuteReader())
            {
                while (dr.Read())
                {
                    acquisitionPermissions.Add(dr.GetInt32(0));
                }
                dr.Close();
            }
        }

        /// <summary>
        /// 権限の解除イベント
        /// </summary>
        /// <param name="acquisitionPermissions"></param>
        /// <param name="stPermissions"></param>
        /// <param name="con"></param>
        private void DeleteEvent(List<int> acquisitionPermissions, List<int> stPermissions, SqlConnection con)
        {
            foreach (int permissionId in acquisitionPermissions)
            {
                if (!stPermissions.Contains(permissionId))
                {
                    // SQL
                    string deleteQuery = "DELETE FROM user_permissions WHERE user_id = @UserId AND permission_id = @PermissionId";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@PermissionId", permissionId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion
    }
}