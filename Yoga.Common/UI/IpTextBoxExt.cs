using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;

namespace Yoga.Common.UI
{
    public enum IPType : byte { A, B, C, D, E };

    public partial class IpTextBoxExt : UserControl
    {

        public IpTextBoxExt()
        {
            InitializeComponent();
            tb_ip1.GotFocus += new EventHandler(IpTextBox_GotFocus);
            tb_ip2.GotFocus += new EventHandler(IpTextBox_GotFocus);
            tb_ip3.GotFocus += new EventHandler(IpTextBox_GotFocus);
            tb_ip4.GotFocus += new EventHandler(IpTextBox_GotFocus);
        }

        private void IPv4TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char KeyChar = e.KeyChar;
            TextBox temptextbox = (TextBox)sender;
            int TextLength = temptextbox.TextLength; //这里的长度具有初始值，根据最初的值获得

            if (Regex.Match(KeyChar.ToString(), "[0-9]").Success)
            {
                if (TextLength == 2)
                {
                    if (int.Parse(temptextbox.Text + e.KeyChar.ToString()) > 255)
                    {
                        temptextbox.Text = "255";
                        e.Handled = true;
                    }
                }

            }
            else
            {   // 回删操作  
                if (KeyChar == '\b')
                {
                    if (TextLength == 0)
                    {
                        if (temptextbox != tb_ip1)
                        {   // 回退到上一个文本框  
                            SendKeys.Send("+{TAB}{End}");
                        }
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>  
        /// string类型的IP地址  
        /// </summary>  
        public override string Text
        {
            get
            {
                return this.Value.ToString();
            }
            set
            {
                IPAddress address;
                if (IPAddress.TryParse(value, out address))
                {
                    byte[] bytes = address.GetAddressBytes();
                    for (int i = 1; i <= 4; i++)
                    {
                        Control[] controls = this.Controls.Find("tb_ip" + i.ToString(), true);
                        ((TextBox)controls[0]).Text = bytes[i - 1].ToString("D"); //十进制
                    }
                }
            }
        }

        /// <summary>  
        /// IP地址  
        /// </summary>  
        public IPAddress Value
        {
            get
            {
                IPAddress address;

                string ipString = dealInputIPAddress();

                if (IPAddress.TryParse(ipString, out address))
                {
                    return address;
                }
                else
                {
                    return new IPAddress(0);
                }
            }
            set
            {
                byte[] bytes = value.GetAddressBytes();
                for (int i = 1; i <= 4; i++)
                {
                    Control[] controls = this.Controls.Find("tb_ip" + i.ToString(), true);
                    ((TextBox)controls[0]).Text = bytes[i - 1].ToString("D"); //十进制
                }
            }
        }

        /// <summary>  
        /// IP地址分类  
        /// </summary>  
        public IPType Type
        {
            get
            {
                byte[] bytes = this.Value.GetAddressBytes();
                int FirstByte = bytes[0];

                if (FirstByte < 128)
                {
                    return IPType.A;
                }
                else if (FirstByte < 192)
                {
                    return IPType.B;
                }
                else if (FirstByte < 224)
                {
                    return IPType.C;
                }
                else if (FirstByte < 240)
                {
                    return IPType.D;
                }
                else
                {
                    return IPType.E;    // 保留
                }
            }
        }

        public bool ValidateIP()
        {
            IPAddress address;

            string ipString = dealInputIPAddress();

            return IPAddress.TryParse(ipString, out address);
        }

        private void tb_ip1_TextChanged(object sender, EventArgs e)
        {
            if (tb_ip1.Text.Length == 3 && tb_ip1.Text.Length > 0 && tb_ip1.SelectionLength == 0)
            {
                tb_ip2.Focus();
                tb_ip2.Select(0, tb_ip2.Text.Length);
            }
        }

        private void tb_ip2_TextChanged(object sender, EventArgs e)
        {
            if (tb_ip2.Text.Length == 3 && tb_ip2.Text.Length > 0 && tb_ip2.SelectionLength == 0)
            {
                tb_ip3.Focus();
                tb_ip3.Select(0, tb_ip3.Text.Length);
            }
        }

        private void tb_ip3_TextChanged(object sender, EventArgs e)
        {
            if (tb_ip3.Text.Length == 3 && tb_ip3.Text.Length > 0 && tb_ip3.SelectionLength == 0)
            {
                tb_ip4.Focus();
                tb_ip4.Select(0, tb_ip4.Text.Length);
            }
        }

        //通过下面两个事件保证tab键和鼠标左键进入时，全选

        private void IpTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            TextBox temptextbox = (TextBox)sender;
            //如果鼠标左键操作并且标记存在，则执行全选             
            if (e.Button == MouseButtons.Left && (bool)temptextbox.Tag == true)
            {
                temptextbox.SelectAll();
            }

            //取消全选标记              
            temptextbox.Tag = false;
        }


        private void IpTextBox_GotFocus(object sender, EventArgs e)
        {
            TextBox temptextbox = (TextBox)sender;
            temptextbox.Tag = true;    //设置标记              
            temptextbox.SelectAll();   //注意1         
        }

        /// <summary>
        /// 对输入ip地址进行处理
        /// </summary>
        /// <returns></returns>
        private string dealInputIPAddress()
        {

            //对输入ip前端有0的进行判断
            string tempip1 = tb_ip1.Text.Trim();
            string tempip2 = tb_ip2.Text.Trim();
            string tempip3 = tb_ip3.Text.Trim();
            string tempip4 = tb_ip4.Text.Trim();

            string ip1 = (tempip1.Length == 0 ? "" : Convert.ToInt32(tempip1).ToString());
            string ip2 = (tempip2.Length == 0 ? "" : Convert.ToInt32(tempip2).ToString());
            string ip3 = (tempip3.Length == 0 ? "" : Convert.ToInt32(tempip3).ToString());
            string ip4 = (tempip4.Length == 0 ? "" : Convert.ToInt32(tempip4).ToString());

            string ipString = ip1 + "." + ip2 + "." + ip3 + "." + ip4;

            return ipString;
        }


    }
}
