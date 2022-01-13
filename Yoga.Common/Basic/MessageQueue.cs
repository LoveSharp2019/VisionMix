using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoga.Common.Basic
{
    public class MessageQueue
    {
        private ConcurrentQueue<string> ListQueue = new ConcurrentQueue<string>();

        TextBox txtShow;
        object objLock = new object();

        int messageCount = 0;
        private int InShowMessage = 0;

        public MessageQueue(TextBox txtShow)
        {
            this.txtShow = txtShow;
        }
        //int x;
        async void UIDoWork()
        {
            ShowMessage();
            await Task.Delay(10);

            Interlocked.Exchange(ref InShowMessage, 0);
            if ((ListQueue.IsEmpty == false))
            {
                TriggerShow();
            }
        }

        private void ShowMessage()
        {
            StringBuilder str = new StringBuilder();

            while (ListQueue.IsEmpty == false)
            {
                try
                {
                    string message = string.Empty;

                    //从队列中取出  
                    if (ListQueue.TryDequeue(out message) == false)
                    {
                        continue;
                    }

                    if (message == string.Empty)
                    {
                        continue;
                    }
                    messageCount++;
                    str.Append(message);
                    str.Append(Environment.NewLine);
                }
                catch (Exception /*ex*/)
                {
                }

            }
            txtShow.AppendText(str.ToString());
            //超过1000行就删除500行
            if (messageCount > 1000)
            {
                string[] lines = txtShow.Lines;
                List<string> a = lines.ToList();
                a.RemoveRange(0, 500);
                txtShow.Lines = a.ToArray();
                messageCount = 500;
            }
            txtShow.ScrollToCaret();
        }


        private void TriggerShow()
        {
            if (Interlocked.CompareExchange(ref InShowMessage, 1, 0) == 0)
            {
                //启动显示
                if (txtShow.InvokeRequired)
                {
                    txtShow.BeginInvoke(new Action(UIDoWork));
                }
                else
                {
                    UIDoWork();
                }

            }
        }
        public void ShowMessage(string message)
        {
            //AddCount();
            ListQueue.Enqueue($"{DateTime.Now.ToString("HH:mm:ss")} {message}");
            TriggerShow();
        }
    }
}
