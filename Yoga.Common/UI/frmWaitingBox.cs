using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yoga.Common.UI
{
    public partial class frmWaitingBox : Form
    {
        #region Properties
        private int _MaxWaitTime;
        private int _WaitTime;
        private bool _CancelEnable;
        private IAsyncResult _AsyncResult;
        public EventHandler<EventArgs> AsyncMethod;

        public string Message { get; private set; }
        public int TimeSpan { get; set; }
        public bool FormEffectEnable { get; set; }
        #endregion

        #region frmWaitingBox
        public frmWaitingBox(EventHandler<EventArgs> method, int maxWaitTime, string waitMessage, bool cancelEnable, bool timerVisable)
        {
            maxWaitTime *= 1000;
            Initialize(method, maxWaitTime, waitMessage, cancelEnable, timerVisable);
        }

        public frmWaitingBox(int maxWaitTime, string waitMessage, bool cancelEnable, bool timerVisable)
        {
            maxWaitTime *= 1000;
            Initialize(null, maxWaitTime, waitMessage, cancelEnable, timerVisable);
        }

        public frmWaitingBox(EventHandler<EventArgs> method)
        {
            int maxWaitTime = 60 * 1000;
            string waitMessage = "正在处理数据，请稍后...";
            bool cancelEnable = true;
            bool timerVisable = true;
            Initialize(method, maxWaitTime, waitMessage, cancelEnable, timerVisable);
        }
        public frmWaitingBox(EventHandler<EventArgs> method, string waitMessage)
        {
            int maxWaitTime = 60 * 1000;
            bool cancelEnable = true;
            bool timerVisable = true;
            Initialize(method, maxWaitTime, waitMessage, cancelEnable, timerVisable);
        }
        public frmWaitingBox(EventHandler<EventArgs> method, bool cancelEnable, bool timerVisable)
        {
            int maxWaitTime = 60 * 1000;
            string waitMessage = "正在处理数据，请稍后...";
            Initialize(method, maxWaitTime, waitMessage, cancelEnable, timerVisable);
        }
        #endregion

        #region Initialize
        private void Initialize(EventHandler<EventArgs> method, int maxWaitTime, string waitMessage, bool cancelEnable, bool timerVisable)
        {
            InitializeComponent();
            //initialize form
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            Color[] c = GetRandColor();
            this.panel1.BackColor = c[0];
            this.BackColor = c[1];
            this.labMessage.Text = waitMessage;


            FormEffectEnable = true;
            //para
            TimeSpan = 500;
            Message = string.Empty;
            _CancelEnable = cancelEnable;
            _MaxWaitTime = maxWaitTime;
            _WaitTime = 0;
            AsyncMethod = method;
            this.timer1.Interval = TimeSpan;
            this.timer1.Start();
        }
        #endregion

        #region Color
        private Color[] GetRandColor()
        {
            int rMax = 248;
            int rMin = 204;
            int gMax = 250;
            int gMin = 215;
            int bMax = 250;
            int bMin = 240;
            Random r = new Random(DateTime.Now.Millisecond);
            int r1 = r.Next(rMin, rMax);
            int r2 = r1 + 5;
            int g1 = r.Next(gMin, gMax);
            int g2 = g1 + 5;
            int b1 = r.Next(bMin, bMax);
            int b2 = b1 + 5;
            Color c1 = Color.FromArgb(r1, g1, b1);
            Color c2 = Color.FromArgb(r2, g2, b2);
            Color[] c = { c1, c2 };
            return c;
        }
        #endregion

        #region Events
        private void timer1_Tick(object sender, EventArgs e)
        {
            _WaitTime += TimeSpan;
            if (this._AsyncResult==null)
            {
                return;
            }
            if (!this._AsyncResult.IsCompleted)
            {               
                if (_WaitTime > _MaxWaitTime && _MaxWaitTime > 0)
                {
                    Message = string.Format("处理数据超时{0}秒，结束当前操作！", _MaxWaitTime / 1000);
                    this.Close();
                }
            }
            else
            {
                timer1.Enabled = false;
                
                this.Message = string.Empty;
                this.Close();
                AsyncMethod.EndInvoke(this._AsyncResult);
            }

        }

        private void frmWaitingBox_Shown(object sender, EventArgs e)
        {
            _AsyncResult = AsyncMethod.BeginInvoke(null, null, null, null);

        }
        #endregion
        private object lockObj = new object();

        bool haveNGData = false;

        private delegate void showMessageDelegate(string message, bool isOk);
        public void ShowStatus(string message, bool isOk)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new showMessageDelegate(ShowStatus), new object[] { message, isOk });
                return;
            }

            try
            {
                dataGridViewMessage.CurrentCell = null;
                DataGridViewRowCollection rows = dataGridViewMessage.Rows;

                // 1行追加
                rows.Add();

                // 最終行
                DataGridViewCellCollection cells = rows[rows.Count - 1].Cells;

                cells[0].Value = message;

                if (isOk)
                {
                    rows[rows.Count - 1].DefaultCellStyle.ForeColor = Color.Green; //字体颜色

                }
                else
                {
                    haveNGData = true;
                    rows[rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red; //字体颜色
                    rows[rows.Count - 1].DefaultCellStyle.SelectionForeColor = Color.Red; //字体颜色
                }
                dataGridViewMessage.FirstDisplayedScrollingRowIndex = rows.Count - 1;
            }
            catch (Exception)
            {
                ;
            }

        }

        private void frmWaitingBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer1.Stop();

#if !DEBUG

            if (haveNGData)
            {
                MessageBox.Show("发生异常,请检查设置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#endif

        }

        private void dataGridViewMessage_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.Selected = false;
        }
    }
}
