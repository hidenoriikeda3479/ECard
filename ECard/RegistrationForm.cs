using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECard
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 登録ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLan_Click(object sender, EventArgs e)
        {

            // 情報が入力されているか
        if(txtUser.Text == "" || txtPswrd.Text == "")
            {
                MessageBox.Show("必要な情報が入力されていません");
            }
            return;

           // MessageBox.Show("登録が完了しました。");
        }
    }
}
