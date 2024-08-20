using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECard.Model
{
    /// <summary>
    /// 画像カード情報
    /// </summary>
    public class ImageCardViewModel
    {
        // <summary>
        /// ユーザーID取得
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 画像ID取得
        /// </summary>
        public int ImageId { get; set; }

        /// <summary>
        /// カードタイプ取得
        /// </summary>
        public int CardType {  get; set; }

        /// <summary>
        /// 説明取得
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///画像データ取得 
        /// </summary>
        public System.Drawing.Image ImageDate { get; set; }

        /// <summary>
        /// 登録日取得
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新日取得
        /// </summary>
        public DateTime? UpdateAt { get; set; }
    }
}
