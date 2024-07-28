using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECard.Model
{/// <summary>
/// SQL登録情報クラス
/// </summary>
    public  class SaveImageCardModel
    {
        //皇帝登録：画像ID
        public int KingCardTypeImageId { get; set; }

        //皇帝登録:画像データ
        public System.Drawing.Image KingCardTypeImage { get; set; }

        //皇帝登録:カードタイプ
        public int KingCardTypeNo { get; set; }

        //皇帝登録：説明
        public string KingCardTypeDescription { get; set; }

        //貧民登録:画像ID
        public int PoorpeopleCardTypeImageId { get; set; }

        //貧民登録:画像データ
        public System.Drawing.Image PoorpeopleCardTypeImage { get; set; }

        //貧民登録:カードタイプ
        public int PoorpeopleCardTypeNo { get; set; }

        //貧民登録：説明
        public string PoorpeopleCardTypeDescription { get; set; }

        //平民登録:画像ID
        public int CommonerCardTypeImageId { get; set; }

        //平民登録:画像データ
        public System.Drawing.Image CommonerCardTypeImage { get; set; }

        //平民登録:カードタイプ
        public int CommonerCardTypeNo { get; set; }

        //平民登録：説明
        public string CommonerCardTypeDescription { get; set; }

        //更新前SQLの画像ID情報
        public int SaveImageId { get; set; }

        //更新前SQLのカードタイプ
        public int SaveCardType { get; set; }

        //更新対象SQLの画像ID
        public int UpdataImageId { get; set; }

        //更新対象SQLのカードタイプ
        public int UpdataCardType { get; set; }

        //皇帝更新チェックフラグ
        public int KingCardTypeCheckFlag { get; set; }

        //貧民更新チェックフラグ
        public int PoorpeopleCardCheckFlag { get; set; }

        //平民更新チェックフラグ
        public int CommonerCardCheckFlag { get; set; }

        //画像ID取得
        public int ImageId { get; set; }

        //皇帝・平民・貧民チェックボックスの状態表示
        public int CardType {  get; set; }

        //選択対象の説明取得
        public string description { get; set; }

        //ユーザーID情報
        public int UserLogin {  get; set; }

        //SQLコマンド
        public string sql { get; set; }

        public int CheckFlag { get; set; }
    }
}
