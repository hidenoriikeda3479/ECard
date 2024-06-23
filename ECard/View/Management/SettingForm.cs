using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECard.Management.Image;

namespace ECard.View.Management
{
    public partial class SettingForm : Form
    {
        /// <summary>
        /// ユーザーログインのグローバル変数宣言
        /// </summary>
        private int UserLogin = 1;
        public SettingForm()
        {
            InitializeComponent();
        }

        private void btnImageForm_Click(object sender, EventArgs e)
        {
            

            // 画像設定画面を初期化
            var userForm = new ImageForm(UserLogin);

            // 画面をモードレスで表示
            userForm.Show();
        }
    }
}
