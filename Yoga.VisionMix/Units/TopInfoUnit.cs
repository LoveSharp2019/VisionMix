using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Yoga.VisionMix.Frame;

namespace Yoga.VisionMix.Units
{

    public partial class TopInfoUnit : UserControl
    {
        FrmMain _pMainFrame;
        private StatusManger statusManger;
        private delegate void changeStatusDelegate(object sender, EventArgs e);
        public TopInfoUnit(FrmMain mainframe)
        {
            _pMainFrame = mainframe;
            InitializeComponent();
            statusManger = StatusManger.Instance;
            statusManger.StatusEvent += StatusManger_StatusEvent;
        }

        private void StatusManger_StatusEvent(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new changeStatusDelegate(StatusManger_StatusEvent), new object[] { sender,e });
                return;
            }
            UpdateAutoState();
        }

        private void StatusShow_Tick(object sender, EventArgs e)
        {
            //UpdateAutoState();
        }
        
        private void UpdateAutoState()
        {
            lbStatus.Text = statusManger.RuningStatus.ToString();

            if (statusManger.RuningStatus == RuningStatus.系统异常)
            {
                lbResult.BackColor = Color.Red;
            }
            else
            {
                lbResult.BackColor = Color.Green;
            }
            //if (statusManger.RuningResult == true)
            //{
            //    lbResult.Text = "OK";
            //    lbResult.BackColor = Color.Green;
            //}
            //else
            //{
            //    lbResult.Text = "NG";
            //    lbResult.BackColor = Color.Red;
            //}
        }
        private void TopInfoFrame_Load(object sender, EventArgs e)
        {
            label1.Text = AssemblyTitle;
        }
        #region 程序集特性访问器

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
    }
}
