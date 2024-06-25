using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECard.View.Management.User
{

    /// <summary>
    /// ユーザー情報
    /// </summary>
    internal class UserViewModel
    {
        /// <summary>
        /// ユーザーID取得
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// ユーザー名取得
        /// </summary>
        public string UserName { get; set; }

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
