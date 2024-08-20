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
    /// <summary>
    /// 画像カード登録画面
    /// </summary>
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
        /// 画面カード登録情報反映イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserCardRegistration_Load(object sender, EventArgs e)
        {
            //画面カード登録情報取得メソッド呼び出し
            SqlServerAccess();

            //皇帝カード更新、登録情報メソッド呼び出し
            KingCardTypeConditionalbranch();

            //貧民カード更新、登録情報メソッド呼び出し
            PoorpeopleCardTypeConditionalbranch();

            //平民カード更新、登録情報メソッド
            CommonerCardTypeConditionalbranch();
        }

        /// <summary>
        /// 画像カード情報登録完了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {

            //画面カード登録情報取得メソッド
            SqlServerAccess();

            //SQL更新フラグの条件を満たした処理
            if (ConditionalbranchModel.SqlUpdateFlag == CheckFlag)
            {
                //画面カード登録情報取得メソッド呼び出し
                SqlServerAccess();

                //皇帝カード更新、登録情報メソッド呼び出し
                KingCardTypeConditionalbranch();

                //貧民カード更新、登録情報メソッド呼び出し
                PoorpeopleCardTypeConditionalbranch();

                //平民カード更新、登録情報メソッド
                CommonerCardTypeConditionalbranch();

                //Sql更新メソッド呼び出し
                SqlUpdateProcess();

                //画面カード登録情報取得メソッド呼び出し
                SqlServerAccess();

                //皇帝カード更新、登録情報メソッド呼び出し
                KingCardTypeConditionalbranch();

                //貧民カード更新、登録情報メソッド呼び出し
                PoorpeopleCardTypeConditionalbranch();

                //平民カード更新、登録情報メソッド
                CommonerCardTypeConditionalbranch();

            }

            //SQL登録
            if(ConditionalbranchModel.SqlRegistrationFlag == CheckFlag)
            {
                //Sql取得メソッド呼び出し
                SqlProcess();

                //画面カード登録情報取得メソッド呼び出し
                SqlServerAccess();

                //皇帝カード更新、登録情報メソッド呼び出し
                KingCardTypeConditionalbranch();

                //貧民カード更新、登録情報メソッド呼び出し
                PoorpeopleCardTypeConditionalbranch();

                //平民カード更新、登録情報メソッド
                CommonerCardTypeConditionalbranch();
            }

        }

        /// <summary>
        /// 皇帝チェックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            //貧民チェック選択禁止
            checkBox2.Enabled = false;

            //平民チェック選択禁止
            checkBox3.Enabled = false;

            //SQL更新条件を満たした処理：皇帝
            if (SaveImageCardDataModel.KingCardTypeNo == ConditionalbranchModel.KingCard && checkBox1.Checked == true)
            {
                //皇帝タイプ選択
                CardType = ConditionalbranchModel.KingCard;

                //対象の説明文を取得
                description = textBox1.Text;

                //SQL更新フラグ
                CheckFlag = ConditionalbranchModel.SqlUpdateFlag;

                //皇帝カード更新フラグ
                SaveImageCardDataModel.KingCardTypeCheckFlag = CheckFlag;
            }

            //SQL登録条件を満たした処理
            else
            {
                //皇帝タイプ選択
                CardType = ConditionalbranchModel.KingCard;

                //対象の説明文を取得
                description = textBox1.Text;

            }
        }

        /// <summary>
        /// 貧民チェックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //皇帝チェック禁止
            checkBox1.Enabled = false;

            //平民チェック禁止
            checkBox3.Enabled = false;

            //SQL更新条件を満たした処理
            if (SaveImageCardDataModel.PoorpeopleCardTypeNo == ConditionalbranchModel.PoorpeopleCard && checkBox2.Checked == true)
            {
                //貧民タイプ選択
                CardType = ConditionalbranchModel.PoorpeopleCard;

                //対象の説明文を取得
                description = textBox1.Text;

                //SQL更新フラグ
                CheckFlag = ConditionalbranchModel.SqlUpdateFlag;

                //貧民カード更新フラグ
                SaveImageCardDataModel.PoorpeopleCardCheckFlag = CheckFlag;
            }

            //SQL登録条件を満たした処理
            else
            {
                //貧民タイプ選択
                CardType = ConditionalbranchModel.PoorpeopleCard;

                //対象の説明文を取得
                description = textBox1.Text;

            }
        }

        /// <summary>
        /// 平民チェックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            //皇帝チェック禁止
            checkBox1.Enabled = false;

            //貧民チェック禁止
            checkBox2.Enabled = false;

            //SQL更新条件を満たした処理
            if (SaveImageCardDataModel.CommonerCardTypeNo == ConditionalbranchModel.CommonerCard && checkBox3.Checked == true)
            {
                //平民タイプ選択
                CardType = ConditionalbranchModel.CommonerCard;

                //対象の説明文を取得
                description = textBox1.Text;

                //SQL更新フラグ
                CheckFlag = ConditionalbranchModel.SqlUpdateFlag;

                //平民カード更新フラグ
                SaveImageCardDataModel.CommonerCardCheckFlag = CheckFlag;

            }

            //SQL登録条件を満たした処理
            else
            {
                //平民タイプ選択
                CardType = ConditionalbranchModel.CommonerCard;

                //対象の説明文を取得
                description = textBox1.Text;

            }
        }

        /// <summary>
        /// Sql更新メソッド
        /// </summary>
        private void SqlUpdateProcess()
        {
            // 接続情報を渡す
            var dbHelper = new DatabaseHelper();

            // 接続を開く
            var SqlServerOpen = dbHelper.OpenConnection();

            //SQLの更新
             string SqlUpdate = $"UPDATE images_card  " +
                                $"SET  user_id = '{UserLogin}' , " +
                                $"image_id = '{ImageId}' ," +
                                $"card_type = '{CardType}' ," +
                                $"description = '{textBox1.Text}' ," +
                                $"created_at = '{DateTime.Now}' ," +
                                $"update_at ='{DateTime.Now}' " +
                                $"WHERE user_id = '{UserLogin}' AND " +
                                $"image_id = '{SaveImageCardDataModel.UpdataImageId}' AND " +
                                $"card_type = '{SaveImageCardDataModel.UpdataCardType}'";

            //SQL実行結果を取得
            DataTable result = dbHelper.ExecuteQuery(SqlServerOpen, SqlUpdate);
        }

        /// <summary>
        /// Sql取得メソッド
        /// </summary>
        private void SqlProcess()
        {
            // 接続情報を渡す
            var dbHelper = new DatabaseHelper();

            // 接続を開く
            var SqlServerOpen = dbHelper.OpenConnection();

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

        /// <summary>
        /// 画面カード登録情報取得メソッド
        /// </summary>
        private void  SqlServerAccess()
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
                //SQL登録情報:画像ID取得
                SaveImageCardDataModel.KingCardTypeImageId = KingCardType.ImageId;

                //SQL登録情報:画像データ取得
                SaveImageCardDataModel.KingCardTypeImage = KingCardType.ImageDate;

                //SQL登録情報:カードタイプ取得
                SaveImageCardDataModel.KingCardTypeNo = KingCardType.CardType;

                //SQL登録情報:説明取得
                SaveImageCardDataModel.KingCardTypeDescription = KingCardType.Description;
            }

            //貧民登録のnullチェック
            if (PoorpeopleCardType != null)
            {
                //SQL登録情報:画像ID取得
                SaveImageCardDataModel.PoorpeopleCardTypeImageId = PoorpeopleCardType.ImageId;

                //SQL登録情報:画像データ取得
                SaveImageCardDataModel.PoorpeopleCardTypeImage = PoorpeopleCardType.ImageDate;

                //SQL登録情報:カードタイプ取得
                SaveImageCardDataModel.PoorpeopleCardTypeNo = PoorpeopleCardType.CardType;

                //SQL登録情報:説明取得
                SaveImageCardDataModel.PoorpeopleCardTypeDescription = PoorpeopleCardType.Description;

            }

            //平民登録のnullチェック
            if (CommonerCardType != null)
            {
                //SQL登録情報:画像ID取得
                SaveImageCardDataModel.CommonerCardTypeImageId = CommonerCardType.ImageId;

                //SQL登録情報:画像データ取得
                SaveImageCardDataModel.CommonerCardTypeImage = CommonerCardType.ImageDate;

                //SQL登録情報:カードタイプ取得
                SaveImageCardDataModel.CommonerCardTypeNo = CommonerCardType.CardType;

                //SQL登録情報:説明取得
                SaveImageCardDataModel.CommonerCardTypeDescription = CommonerCardType.Description;

            }

            
        }

        /// <summary>
        /// 皇帝カード更新、登録情報メソッド
        /// </summary>
        private void KingCardTypeConditionalbranch()
        {
            //皇帝更新条件を満たした処理
            if (SaveImageCardDataModel.KingCardTypeCheckFlag == ConditionalbranchModel.SqlUpdateFlag)
            {
                //更新対象SQLの画像ID取得
                SaveImageCardDataModel.UpdataImageId = SaveImageCardDataModel.KingCardTypeImageId;

                //更新対象SQLのカードタイプ取得
                SaveImageCardDataModel.UpdataCardType = SaveImageCardDataModel.KingCardTypeNo;

                //選択対象の画像表示
                pictureBox2.Image = SaveImageCardDataModel.KingCardTypeImage;

                //ピクチャーボックスのサイズに画像を調整
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                //選択対象の説明表示
                textBox4.Text = SaveImageCardDataModel.KingCardTypeDescription;
            }

            //皇帝登録情報
            else if (ConditionalbranchModel.KingCard == SaveImageCardDataModel.KingCardTypeNo)
            {
                //更新前SQLの画像ID取得
                SaveImageCardDataModel.SaveImageId = SaveImageCardDataModel.KingCardTypeImageId;

                //更新前SQLのカードタイプ取得
                SaveImageCardDataModel.SaveCardType = SaveImageCardDataModel.KingCardTypeNo;

                //選択対象の画像表示
                pictureBox2.Image = SaveImageCardDataModel.KingCardTypeImage;

                //ピクチャーボックスのサイズに画像を調整
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                //選択対象の説明表示
                textBox4.Text = SaveImageCardDataModel.KingCardTypeDescription;

            }
        }

        /// <summary>
        /// 貧民カード更新、登録情報メソッド
        /// </summary>
        private void PoorpeopleCardTypeConditionalbranch()
        {
            //貧民更新条件を満たした処理
            if (SaveImageCardDataModel.PoorpeopleCardCheckFlag == ConditionalbranchModel.SqlUpdateFlag)
            {
                //更新対象SQLの画像ID取得
                SaveImageCardDataModel.UpdataImageId = SaveImageCardDataModel.PoorpeopleCardTypeImageId;

                //更新対象SQLのカードタイプ取得
                SaveImageCardDataModel.UpdataCardType = SaveImageCardDataModel.PoorpeopleCardTypeNo;

                //選択対象の画像表示
                pictureBox3.Image = SaveImageCardDataModel.PoorpeopleCardTypeImage;

                //ピクチャーボックスのサイズに画像を調整
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                //選択対象の説明表示
                textBox2.Text = SaveImageCardDataModel.PoorpeopleCardTypeDescription;

            }

            //貧民登録情報
            else if (ConditionalbranchModel.PoorpeopleCard == SaveImageCardDataModel.PoorpeopleCardTypeNo)
            {
                //更新前SQLの画像ID取得
                SaveImageCardDataModel.SaveImageId = SaveImageCardDataModel.PoorpeopleCardTypeImageId;

                //更新前SQLのカードタイプ取得
                SaveImageCardDataModel.SaveCardType = SaveImageCardDataModel.PoorpeopleCardTypeNo;

                //選択対象の画像表示
                pictureBox3.Image = SaveImageCardDataModel.PoorpeopleCardTypeImage;

                //ピクチャーボックスのサイズに画像を調整
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                //選択対象の説明表示
                textBox2.Text = SaveImageCardDataModel.PoorpeopleCardTypeDescription;
            }
        }
        /// <summary>
        /// 平民カード更新、登録情報メソッド
        /// </summary>
        /// <returns></returns>
        private void CommonerCardTypeConditionalbranch()
        {
            //平民更新条件を満たした処理
            if(SaveImageCardDataModel.CommonerCardCheckFlag == ConditionalbranchModel.SqlUpdateFlag)
            {
                //更新対象SQLの画像ID取得
                SaveImageCardDataModel.UpdataImageId = SaveImageCardDataModel.CommonerCardTypeImageId;

                //更新対象SQLのカードタイプ取得
                SaveImageCardDataModel.UpdataCardType = SaveImageCardDataModel.CommonerCardTypeNo;

                //選択対象の画像表示
                pictureBox4.Image = SaveImageCardDataModel.CommonerCardTypeImage;

                //ピクチャーボックスのサイズに画像を調整
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

                //選択対象の説明表示
                textBox3.Text = SaveImageCardDataModel.CommonerCardTypeDescription;
            }
            
            //平民登録情報
            else if (ConditionalbranchModel.CommonerCard == SaveImageCardDataModel.CommonerCardTypeNo)
            {
                //更新前SQLの画像ID取得
                SaveImageCardDataModel.SaveImageId = SaveImageCardDataModel.CommonerCardTypeImageId;

                //更新前SQLのカードタイプ取得
                SaveImageCardDataModel.SaveCardType = SaveImageCardDataModel.CommonerCardTypeNo;

                //選択対象の画像表示
                pictureBox4.Image = SaveImageCardDataModel.CommonerCardTypeImage;

                //ピクチャーボックスのサイズに画像を調整
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

                //選択対象の説明表示
                textBox3.Text = SaveImageCardDataModel.CommonerCardTypeDescription;

            }

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
        /// <summary>
        /// 画像カードテーブル情報イベント
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
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
