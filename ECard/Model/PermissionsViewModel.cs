using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECard.Model
{
    internal class PermissionsViewModel
    {
        /// <summary>
        /// 権限ID取得
        /// </summary>
        public int PermissionId { get; set; }
        
        /// <summary>
        /// 権限名 
        /// </summary>
        public string PermissionName { get; set; }
        
        /// <summary>
        /// 説明
        /// </summary>
        public string DescriPtion { get; set; }

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime? CreatedAt { get; set; }
        
        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime UpdateAt { get; set; }
    }
}
