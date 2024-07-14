using ECard.Common;
using ECard.Management.Image;
using ECard.Model;
using ECard.User;
using ECard.View.Management.User;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ECard.View.Management.Image
{
    public partial class ImageCardRegistration : Form
    {
        /// <summary>
        /// 画像ID取得のグローバル変数宣言
        /// </summary>
        private int ImageId;

        /// <summary>
        /// 皇帝・平民・貧民チェックボックスの状態表示変数宣言
        /// </summary>
        private int CardType;

        /// <summary>
        /// 選択対象の説明取得のグローバル変数宣言
        /// </summary>
        private string description;

        /// <summary>
        /// ユーザーID情報
        /// </summary>
        private int UserLogin;

        /// <summary>
        /// SQLコマンドグローバル変数宣言
        /// </summary>
        private string sql;

        /// <summary>
        /// SQL更新フラグのグローバル変数宣言
        /// </summary>
        private int CheckFlag;

        /// <summary>
        /// SQL登録状態:画像ID変数宣言
        /// </summary>
        private int SaveImageId;

        /// <summary>
        /// SQL登録状態:カードタイプ変数宣言
        /// </summary>
        private int SaveCardType;

        /// <summary>
        /// 画像カード情報画面のコンストラクタ
        /// </summary>
        /// <param name="ImageDateColumn"></param>
        /// <param name="DescriptionColumn"></param>
        /// <param name="ImageIdColumn"></param>
        /// <param name="login"></param>
        public ImageCardRegistration(object ImageDateColumn, string DescriptionColumn, int ImageIdColumn, int login)
        {
            InitializeComponent();

            //選択対象の画像・説明表示メソッド呼び出し
            ImageAcquisition(ImageDateColumn, DescriptionColumn, ImageIdColumn, login);

        }

        /// <summary>
        /// 画像カード情報呼び出しイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserCardRegistration_Load(object sender, EventArgs e)
        {
            //画像カード情報テーブル接続メソッド呼び出し
            SqlServerAccess();
        }

        /// <summary>
        /// 画像カード情報登録完了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            //チェックボックス選択メソッド
            checkboxflag();

            //画像カード情報テーブル接続メソッド呼び出し
            SqlServerAccess();

            //SQL更新フラグの条件を満たした処理
            if (CheckFlag == 1)
            {
                // 接続情報を渡す
                var dbHelper = new DatabaseHelper();

                // 接続を開く
                var SqlServerOpen = dbHelper.OpenConnection();

                //SQLの更新
                //string sql = $"UPDATE images  SET Description = '{textBox1.Text}' WHERE image_id = '{ImageId}'";
                string SqlUpdate = $"UPDATE images_card  " +
                                    $"SET  user_id = '{UserLogin}' , " +
                                    $"image_id = '{ImageId}' ," +
                                    $"card_type = '{CardType}' ," +
                                    $"description = '{textBox1.Text}' ," +
                                    $"created_at = '{DateTime.Now}' ," +
                                    $"update_at ='{DateTime.Now}' " +
                                    $"WHERE user_id = '{UserLogin}' AND " +
                                    $"image_id = '{SaveImageId}' AND " +
                                    $"card_type = '{SaveCardType}'";

                //SQL実行結果を取得
                DataTable result = dbHelper.ExecuteQuery(SqlServerOpen, SqlUpdate);

                //画像カード情報テーブル接続メソッド呼び出し
                SqlServerAccess();

            }

            //SQL登録
            if(CheckFlag == 0)
            {
                //sqlデータベース取得実行メソッド呼び出し
                SqlProcess();

                //画像カード情報テーブル接続メソッド呼び出し
                SqlServerAccess();
            }

        }

        /// <summary>
        /// sqlデータベース取得実行メソッド
        /// </summary>
        private int SqlProcess()
        {
            // 接続情報を渡す
            var dbHelper = new DatabaseHelper();

            // 接続を開く
            var SqlServerOpen = dbHelper.OpenConnection();

            //カードタイプ選択条件を満たした処理
            if(CardType > 0)
            {
                // SQLserverへ登録
                string sql = "INSERT INTO" +
                              " images_card " +
                              "(user_id , image_id , card_type , description , created_at , update_at)" +
                              " VALUES" +
                             $"('{UserLogin}' , '{ImageId}' , '{CardType}' , '{textBox1.Text}' , '{DateTime.Now}' , '{DateTime.Now}')";

                //SQL実行結果を取得
                DataTable result = dbHelper.ExecuteQuery(SqlServerOpen, sql);

                MessageBox.Show("画像カード情報の登録が完了しました");

            }
            return CheckFlag;
        }

        /// <summary>
        /// 画像カード情報テーブル接続メソッド
        /// </summary>
        private (int SaveImageId , int SaveCardType , int CheckFlag) SqlServerAccess()
        {
            // 接続情報を渡す
            var dbHelper = new DatabaseHelper();

            // 接続を開く
            var SqlServerOpen = dbHelper.OpenConnection();

            //画像カード情報、画像マスタを内部結合し取得
            sql = " SELECT images_card.user_id , images_card.image_id , images_card.card_type , images.image_data , images.description" +
                  " FROM images_card INNER JOIN images" +
                  " ON images_card.image_id = images.image_id";

            //SQL実行結果を取得
            DataTable result = dbHelper.ExecuteQuery(SqlServerOpen, sql);

            //画像カード情報モデムへデータテーブル情報を反映
            List<ImageCardViewModel> list = setImageList(result);

            //皇帝登録情報を取得
            var KingCardType = list.SingleOrDefault(n => n.UserId == 1 && n.ImageId > 0 && n.CardType == 3);

            //貧民登録情報を取得
            var PoorpeopleCardType = list.SingleOrDefault(n => n.UserId == 1 && n.ImageId > 0 && n.CardType == 2);

            //平民登録情報を取得
            var CommonerCardType = list.SingleOrDefault(n => n.UserId == 1 && n.ImageId > 0 && n.CardType == 1);

            //皇帝登録のnullチェック
            if (KingCardType != null)
            {
                //皇帝の画像データ反映
                pictureBox2.Image = KingCardType.ImageDate;

                //ピクチャーボックスのサイズに画像を調整
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                //皇帝の説明を反映
                textBox4.Text = KingCardType.Description;
                
            }

            //貧民登録のnullチェック
            if (PoorpeopleCardType != null)
            {
                //貧民の画像データ反映
                pictureBox3.Image = PoorpeopleCardType.ImageDate;

                //ピクチャーボックスのサイズに画像を調整
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                //貧民の説明を反映
                textBox2.Text = PoorpeopleCardType.Description;
            }

            //平民登録のnullチェック
            if (CommonerCardType != null)
            {
                //平民の画像データ反映
                pictureBox4.Image = CommonerCardType.ImageDate;

                //ピクチャーボックスのサイズに画像を調整
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

                //平民の説明を反映
                textBox3.Text = CommonerCardType.Description;
            }

            //皇帝更新条件を満たした処理
            if(KingCardType != null && CardType == 3)
            {
                //更新前SQLの画像ID取得
                SaveImageId = KingCardType.ImageId;

                //更新前SQLのカードタイプ取得
                SaveCardType = KingCardType.CardType;

                //SQL更新フラグON
                CheckFlag = 1;
            }

            //貧民更新条件を満たした処理
            if(PoorpeopleCardType != null && CardType == 2)
            {
                //更新前SQLの画像ID取得
                SaveImageId = PoorpeopleCardType.ImageId;

                //更新前SQLのカードタイプ取得
                SaveCardType = PoorpeopleCardType.CardType;

                //SQL更新フラグON
                CheckFlag = 1;
            }

            //平民更新条件を満たした処理
            if (CommonerCardType != null && CardType == 1)
            {
                //更新前SQLの画像ID取得
                SaveImageId = CommonerCardType.ImageId;

                //更新前SQLのカードタイプ取得
                SaveCardType = CommonerCardType.CardType;

                //SQL更新フラグON
                CheckFlag = 1;
            }
            return (SaveImageId , SaveCardType , CheckFlag);
        }


        /// <summary>
        /// チェックボックス選択メソッド
        /// </summary>
        private int checkboxflag()
        {
            //皇帝選択
            if (checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked)
            {
                //皇帝タイプ選択
                CardType = 3;

                //対象画像を初期化
                pictureBox1.Image = null;

                //対象の説明文を取得
                description = textBox1.Text;

            }
            //貧民選択
            else if (checkBox2.Checked && !checkBox1.Checked && !checkBox3.Checked)
            {
                //貧民タイプ選択
                CardType = 2;

                //対象画像を初期化
                pictureBox1.Image = null;

                //対象の説明文を取得
                description = textBox1.Text;

            }
            //平民選択
            else if (checkBox3.Checked && !checkBox1.Checked && !checkBox2.Checked)
            {

                //平民タイプ選択
                CardType = 1;

                //対象画像を初期化
                pictureBox1.Image = null;

                //対象の説明文を取得
                description = textBox1.Text;

            }
            
            //重複チェック
            else 
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                MessageBox.Show("重複しております、再設定お願いします");

            }
            return (CardType);
        }
        
        /// <summary>
        /// //選択対象の画像・説明表示メソッド
        /// </summary>
        /// <param name="ImageDateColumn"></param>
        /// <param name="DescriptionColumn"></param>
        /// <param name="ImageIdColumn"></param>
        private void ImageAcquisition(object ImageDateColumn, string DescriptionColumn, int ImageIdColumn, int login)
        {
            //参照行のID情報を取得
            ImageId = ImageIdColumn;

            //ユーザーID情報
            UserLogin = login;

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

        private List<ImageCardViewModel> setImageList(DataTable result)
        {
            //モデムクラスの初期化
            List<ImageCardViewModel> list = new List<ImageCardViewModel>();

            //データテーブル=データグリッドビューへ結果反映
            foreach (DataRow row in result.Rows)

            {

                // 画像データをバイト配列として取得

                byte[] imageData = Convert.FromBase64String((string)row["image_data"]);

                MemoryStream ms = new MemoryStream(imageData);

                System.Drawing.Image Image = System.Drawing.Image.FromStream(ms);

                // モデルにデータをセット

                ImageCardViewModel model = new ImageCardViewModel

                {
                    UserId = int.Parse(row["user_id"].ToString()),

                    ImageId = int.Parse(row["image_id"].ToString()),

                    CardType = int.Parse(row["card_type"].ToString()),

                    ImageDate = Image, // 画像データをセット

                    Description = row["description"].ToString(),

                };

                list.Add(model);

            }

            return list;
        }
    }
}
