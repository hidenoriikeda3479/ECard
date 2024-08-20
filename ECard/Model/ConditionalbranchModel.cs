using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECard.Model
{
    /// <summary>
    /// 条件分岐の定数クラス
    /// </summary>
    public class ConditionalbranchModel
    {
        //画像一覧画面の定数クラス

        /// <summary>
        /// ユーザーログイン
        /// </summary>
        public const int UserLoginID = 1;

        /// <summary>
        /// 管理者ログイン
        /// </summary>
        public const int AdministratorLoginID = 2;

        //画像カード情報の定数クラス

        /// <summary>
        /// Sql更新フラグ
        /// </summary>
        public const int SqlUpdateFlag = 1;

        /// <summary>
        /// Sql登録フラグ
        /// </summary>
        public const int SqlRegistrationFlag = 0;

        /// <summary>
        /// 皇帝カード
        /// </summary>
        public const int KingCard = 3;

        /// <summary>
        /// 貧民カード
        /// </summary>
        public const int PoorpeopleCard = 2;

        /// <summary>
        /// 平民カード
        /// </summary>
        public const int CommonerCard = 1;

    }
}
