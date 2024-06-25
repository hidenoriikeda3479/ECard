using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECard.View.Management.Image
{
    /// <summary>
    /// モデムクラス
    /// </summary>
    internal class ImageViewModel
    {
        public int ImageId { get; set; }

        public System.Drawing.Image ImageDate {  get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt {  get; set; }

        public DateTime UpdateAt {  get; set; }
    }
}
