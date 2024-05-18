using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECard.Common;
using ECard.User;
using ECard.View.Management.Image;

namespace ECard.Management.Image
{
    public partial class ImageForm : Form
    {
        public ImageForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 新規登録クリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //画面登録の初期化
            var ImageEntry = new ImageRegistration();

            // 画面をモードレスで表示
            ImageEntry.Show();
        }

        //SQLコマンドグローバル変数宣言
        private string sql;
       
        /// <summary>
        /// sqlデータベース取得実行イベント
        /// </summary>
        /// <returns></returns>
        private void SqlProcess()
        {
            // 接続情報を渡す
            var dbHelper = new DatabaseHelper();

            // 接続を開く
            var bbb = dbHelper.OpenConnection();

            //検索チェックが押された条件を満たした処理
            if (checkBox1.Checked == true)
            {

                //登録日から検索
                sql = "SELECT * " +
                " FROM images " +
                "WHERE " +
                "CONVERT (" +
                "date," +
               $" created_at) = '{dateTimePicker1.Value.ToString("yyyy/MM/dd")}'";
            }
            else
            {
                //全登録取得
                sql = " SELECT * " +
                      " FROM images ";
            }

            //SQL実行結果を取得
            DataTable result = dbHelper.ExecuteQuery(bbb, sql);

            //モデムクラスの初期化
            List<ImageViewModel> list = new List<ImageViewModel>();

            //データテーブル=データグリッドビューへ結果反映
            foreach (DataRow row in result.Rows)

            {

                // 画像データをバイト配列として取得

                byte[] imageData = Convert.FromBase64String((string)row["image_data"]);

                MemoryStream ms = new MemoryStream(imageData);

                System.Drawing.Image Image = System.Drawing.Image.FromStream(ms);

                // DataGridViewに表示するためのモデルにデータをセット

                ImageViewModel model = new ImageViewModel

                {

                    ImageId = int.Parse(row["image_id"].ToString()),

                    ImageDate = Image, // 画像データをセット

                    Description = row["description"].ToString(),

                    CreatedAt = DateTime.Parse(row["created_at"].ToString()),

                    UpdateAt = DateTime.Parse(row["update_at"].ToString()),

                };

                list.Add(model);

            }

            //データソースへ情報取得
            dataGridView1.DataSource = list;
        }
        
        // 検索ボタンクリックフラグ
        private bool ButtonClick2 = false;

        /// <summary>
        /// 検索ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            // 接続情報を渡す
            var dbHelper = new DatabaseHelper();

            // 接続を開く
            var bbb = dbHelper.OpenConnection();

            
            //sql実行メソッド呼び出し
            SqlProcess();
            

            //初回検索ボタンクリックイベント時
            if (ButtonClick2 == false)
            {
                //編集ボタン列を作成
                DataGridViewButtonColumn buttonColumn1 = new DataGridViewButtonColumn();
                buttonColumn1.HeaderText = "Edit";//列のヘッダーテキスト
                buttonColumn1.Name = "EditColumn";//列の名前
                buttonColumn1.Text = "編集";//ボタンに表示されるテキスト
                buttonColumn1.UseColumnTextForButtonValue = true;

                dataGridView1.Columns.Insert(0, buttonColumn1);

                //削除ボタン列を作成
                DataGridViewButtonColumn buttonColumn2 = new DataGridViewButtonColumn();
                buttonColumn2.HeaderText = "Delete";//列のヘッダーテキスト
                buttonColumn2.Name = "DeleteColumn";//列の名前
                buttonColumn2.Text = "削除";//ボタンに表示されるテキスト
                buttonColumn2.UseColumnTextForButtonValue = true;

                dataGridView1.Columns.Insert(1, buttonColumn2);

                ButtonClick2 = true;
            }

        }
        /// <summary>
        /// 編集ボタン、削除ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 接続情報を渡す
            var dbHelper = new DatabaseHelper();

            // 接続を開く
            var bbb = dbHelper.OpenConnection();

            //選択カラムの行情報
            var ColumnData = dataGridView1.CurrentRow?.Index;

            //編集ボタンが押された条件を満たした処理
            if (dataGridView1.Columns[e.ColumnIndex].Name == "EditColumn") 
            {
                //選択された行の画像データ取得
                var ImageDateColumn = dataGridView1.Rows[e.RowIndex].Cells["ImageDate"].Value;

                //選択された行のid情報を取得
                var ImageIdColumn = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ImageId"].Value);

                //選択された行の説明取得
                var DescriptionColumn = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Description"].Value);

                //画像編集コードへデータ移動
                var ImageEdit = new ImageEdit(ImageDateColumn , DescriptionColumn , ImageIdColumn);

                //対象画像の画像編集画面に遷移
                ImageEdit.Show();
            }
            //削除ボタンが押された条件を満たした処理
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "DeleteColumn") 
            {
                //選択された行のid情報を取得
                var ImageIdColumn = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ImageId"].Value);

                //選択された行の削除
                string DeleteSql =
                    "DELETE " +
                    "images " +
                    "WHERE " +
                    "image_id = " +
                    $"{ImageIdColumn}";

                //SQL実行結果を取得
                dbHelper.ExecuteQuery(bbb, DeleteSql);

                //sql実行メソッド呼び出し
                SqlProcess();
            }
         
        }
    }
}
