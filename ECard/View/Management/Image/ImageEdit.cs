using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECard.Common;

namespace ECard.View.Management.Image
{
    public partial class ImageEdit : Form
    {
        /// <summary>
        /// 画像ID取得のグローバル変数宣言
        /// </summary>
        private int ImageId;

        /// <summary>
        /// 選択対象の画像、説明の表示イベント
        /// </summary>
        /// <param name="ImageDateColumn"></param>
        /// <param name="DescriptionColumn"></param>
        public ImageEdit(object ImageDateColumn, string DescriptionColumn, int ImageIdColumn)
        {
            InitializeComponent();

            //選択対象の画像・説明表示メソッド呼び出し
            ImageAcquisition(ImageDateColumn, DescriptionColumn, ImageIdColumn);
        }
        
        /// <summary>
        /// 画面参照イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            //Sql更新実行メソッド呼び出し
            SqlUpdate();
        }

        /// <summary>
        /// //選択対象の画像・説明表示メソッド
        /// </summary>
        /// <param name="ImageDateColumn"></param>
        /// <param name="DescriptionColumn"></param>
        /// <param name="ImageIdColumn"></param>
        private void ImageAcquisition(object ImageDateColumn, string DescriptionColumn, int ImageIdColumn)
        {
            //参照行のID情報を取得
            ImageId = ImageIdColumn;

            //画像クラス機能の宣言
            System.Drawing.Image image = (System.Drawing.Image)ImageDateColumn;

            //画像データを取得された条件を満たした処理
            if (image is Bitmap bitmap)
            {
                //画像の表示
                pictureBox1.Image = bitmap;

                //ピクチャーボックスのサイズに画像を調整
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                //説明の表示
                textBox1.Text = DescriptionColumn;

               
            }
        }
        /// <summary>
        /// Sql更新実行メソッド
        /// </summary>
        private void SqlUpdate()
        {
            // 接続情報を渡す
            var dbHelper = new DatabaseHelper();

            // 接続を開く
            var SqlServerOpen = dbHelper.OpenConnection();

            //SQLの更新
            string sql = $"UPDATE images  SET Description = '{textBox1.Text}' WHERE image_id = '{ImageId}'";

            //SQL実行結果を取得
            dbHelper.ExecuteQuery(SqlServerOpen, sql);

            //編集完了メッセージ表示
            MessageBox.Show("編集完了しました");

            //編集画面閉じる
            this.Close();
        }
    }
}
