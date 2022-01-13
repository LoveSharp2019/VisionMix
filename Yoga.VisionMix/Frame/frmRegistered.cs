using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yoga.Common.Helpers;

namespace Yoga.VisionMix.Frame
{
    public partial class frmRegistered : Form
    {
        public frmRegistered()
        {
            InitializeComponent();
            this.ControlBox = false;   // 设置不出现关闭按钮
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string str = RegisterHelper.getMNum();
            if (tbSoftKey.Text==str)
            {
                MessageBox.Show("注册成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                UserSetting.Instance.SoftKey = str;
                UserSetting.Instance.SaveSetting();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("注册失败,机器码错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // 终止此进程并为基础操作系统提供指定的退出代码。
                System.Environment.Exit(1);
            }
        }
    }
}
