using System;
using System.Windows.Forms;

namespace Yoga.VisionMix.Frame
{
    public partial class frmLogin : Form
    {
        private string password = "";
        private string checkerPassword = "";
        public frmLogin()
        {
            InitializeComponent();
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            string passwordInput = Text_PassWord.Text.Trim();
            if (drpOperator.SelectedIndex == 1 
                && (passwordInput == password.Trim()|| passwordInput==checkerPassword.Trim()))
            {
                if (passwordInput == checkerPassword.Trim())
                {
                    StatusManger.Instance.IsChecker = true;
                    Common.Util.Notify("可以开始点检");
                }
                this.DialogResult = DialogResult.OK;
            }
            else if (drpOperator.SelectedIndex == 0)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                MessageBox.Show("账户或密码错误！");
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            drpOperator.SelectedIndex = 1;
            password = IniStatus.Instance.Password;
            checkerPassword = IniStatus.Instance.CheckPassword;
        }

        private void Text_PassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Btn_Ok_Click(null, null);
            }
        }
    }
}