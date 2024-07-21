using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECard.Model
{/// <summary>
/// SQL登録情報クラス
/// </summary>
    public  class SaveImageCardDataModel
    {
        //皇帝登録：画像ID
        public static int KingCardTypeImageId;

        //皇帝登録:画像データ
        public static System.Drawing.Image KingCardTypeImage;

        //皇帝登録:カードタイプ
        public static int KingCardTypeNo;

        //皇帝登録：説明
        public static string KingCardTypeDescription;

        //貧民登録:画像ID
        public static int PoorpeopleCardTypeImageId;

        //貧民登録:画像データ
        public static System.Drawing.Image PoorpeopleCardTypeImage;

        //貧民登録:カードタイプ
        public static int PoorpeopleCardTypeNo;

        //貧民登録：説明
        public static string PoorpeopleCardTypeDescription;

        //平民登録:画像ID
        public static int CommonerCardTypeImageId;

        //平民登録:画像データ
        public static System.Drawing.Image CommonerCardTypeImage;

        //平民登録:カードタイプ
        public static int CommonerCardTypeNo;

        //平民登録：説明
        public static string CommonerCardTypeDescription;

        //更新前SQLの画像ID情報
        public static int SaveImageId;

        //更新前SQLのカードタイプ
        public static int SaveCardType;

        //更新対象SQLの画像ID
        public static int UpdataImageId;

        //更新対象SQLのカードタイプ
        public static int UpdataCardType;

        //皇帝更新チェックフラグ
        public static int KingCardTypeCheckFlag;

        //貧民更新チェックフラグ
        public static int PoorpeopleCardCheckFlag;

        //平民更新チェックフラグ
        public static int CommonerCardCheckFlag;
    }
}
