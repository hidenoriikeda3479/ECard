using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECard.Common;
using ECard.Management.Image;

namespace ECard.View.Management.Image
{
    public partial class ImageRegistration : Form
    {
        /// <summary>
        /// 選択ファイル情報取得のグローバル変数宣言
        /// </summary>
        private string selectFile;

        public ImageRegistration()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画面参照イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //ファイル選択画像取得メソッド呼び出し
            SelectFileAcquisition();
        }
       
        /// <summary>
        /// 画像登録実行イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //SQL新規登録メソッド呼び出し
            SqlInsert();
        }
        
        /// <summary>
        /// SQL新規登録メソッド
        /// </summary>
        private void SqlInsert()
        {
            // 接続情報を渡す
            var dbHelper = new DatabaseHelper();

            // 接続を開く
            var SqlServerOpen = dbHelper.OpenConnection();

            //Bitmapの引数に画像の場所を指定
            Bitmap bmp = new Bitmap(selectFile);

            //メモリの初期化
            MemoryStream ms = new MemoryStream();

            //バイナリーデータへ変換
            bmp.Save(ms, ImageFormat.Png);

            //バイナリーデータの抽出
            byte[] binaryData = ms.ToArray();

            //SQLserverへ登録
            string sql = "INSERT INTO" +
                          " images " +
                          "(image_data , description , created_at , update_at)" +
                          " VALUES" +
                         $"('{Convert.ToBase64String(binaryData)}' , '{textBox1.Text}' , '{DateTime.Now}' , '{DateTime.Now}')";

            //SQL実行結果を取得
            dbHelper.ExecuteQuery(SqlServerOpen, sql);

            //登録完了メッセージ表示
            MessageBox.Show("画像登録完了しました");

            //新規登録画面閉じる
            this.Close();
        }
        /// <summary>
        /// ファイル選択画像取得メソッド
        /// </summary>
        private void SelectFileAcquisition()
        {
            //ファイルダイヤログの初期化
            var openfiledialog = new OpenFileDialog();

            //検索画面のタイトル
            openfiledialog.Title = "ファイルを開く";

            //対象の拡張子設定
            openfiledialog.Filter = "画像ファイル|*.jpg;*.jpeg;*.png";

            //JPEGファイル選択完了できたら以下の条件を処理
            if (openfiledialog.ShowDialog() == DialogResult.OK)
            {
                //選択情報を取得する
                selectFile = openfiledialog.FileName;

                //選択した画像をピクチャーボックスに表示する
                pictureBox1.ImageLocation = selectFile;

                //ピクチャーボックスのサイズに画像を調整
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
