using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoga.VisionMix.Units
{
    public partial class IOUnit : UserControl
    {
        private PLC.PanasonicPLCComm com = null;
        public IOUnit()
        {
            InitializeComponent();
            com = PLC.PanasonicPLCComm.Instance;
        }
        /// <summary>名称 (DI)</summary>
        private List<string> textDi = null;

        /// <summary>CheckBox (DI) 集合</summary>
        private List<CheckBox> checkBoxDi = null;

        /// <summary>名称 (DO)</summary>
        private List<string> textDo = null;
        /// <summary>名称 (AI)</summary>
        private List<string> textAi = null;

        /// <summary>名称 (AO)</summary>
        private List<string> textAo = null;
        /// <summary>CheckBox (DO) 集合</summary>
        private List<CheckBox> checkBoxDo = null;
        private void IOUpdate()
        {
            // DI
            for (int i = 0; i < checkBoxDi.Count; i++)
            {
                CheckBox checkBox = checkBoxDi[i];

                // ON・OFF
                checkBox.Checked = com.GetDi(i) != 0;
               
            }

            // DO
            for (int i = 0; i < checkBoxDo.Count; i++)
            {
                CheckBox checkBox = checkBoxDo[i];

                // ON・OFF
                checkBox.Checked = com.GetDo(i) != 0;
               
            }
            // AI
            for (int i = 0; i < dataGridViewAi.Rows.Count; i++)
            {
                DataGridViewCellCollection cells = dataGridViewAi.Rows[i].Cells;

                // Value : 16bit
                int value = com.GetAi(i);

                cells["columnAiValue"].Value = checkBoxAiHex.Checked ? (value & 0xFFFF).ToString("X4") : value.ToString("D");
            }

            // AO
            for (int i = 0; i < dataGridViewAo.Rows.Count; i++)
            {
                DataGridViewCellCollection cells = dataGridViewAo.Rows[i].Cells;

                // Value : 16bit
                int value = com.GetAo(i);

                cells["columnAoValue"].Value = checkBoxAoHex.Checked ? (value & 0xFFFF).ToString("X4") : value.ToString("D");
            }

        }
        private void IOFrame_Load(object sender, EventArgs e)
        {
            #region 名称 (IN)

            textDi = new List<string>()
            {
                // 00
                "IN0 ",
                "IN1 ",
                "IN2 ",
                "IN3 ",

                "IN4 ",
                "IN5 ",
                "IN6",
                "IN7",

                "IN8 ",
                "IN9",
                "IN10",
                "IN11",

                "IN12",
                "IN13",
                "IN14",
                "IN15",

                // 01
                "IN16",
                "IN17",
                "IN18",
                "IN19",

                "IN20",
                "IN21",
                "IN22",
                "IN23",

                "IN24",
                "IN25",
                "IN26",
                "IN27",

                "IN28 ",
                "IN29",               
            };
            #endregion

            #region CheckBox (IN) 集合

            checkBoxDi = new List<CheckBox>()
            {
                // 00
                DMC_IN0,
                DMC_IN1,
                DMC_IN2,
                DMC_IN3,
                DMC_IN4,
                DMC_IN5,
                DMC_IN6,
                DMC_IN7,
                DMC_IN8,
                DMC_IN9,
                DMC_IN10,
                DMC_IN11,
                DMC_IN12,
                DMC_IN13,
                DMC_IN14,
                DMC_IN15,
             
            };

            #endregion
            #region 注册事件
            for (int i = 0; i < checkBoxDi.Count; i++)
            {
                CheckBox checkBox = checkBoxDi[i];
                checkBox.Text =textDi[i];

                // 输入状态响应
                //checkBox.Click += delegate (object checkBox_sender, EventArgs checkBox_e)
                //{
                //    for (int j = 0; j < checkBoxDi.Count; j++)
                //    {
                //        if (!checkBox_sender.Equals(checkBoxDi[j]))
                //            continue;

                //        com.SetDi(j, checkBoxDi[j].Checked ? 1 : 0);
                //    }
                //};
                checkBox.Image = imageList1.Images[0];
                checkBox.CheckedChanged+= delegate (object checkBox_sender, EventArgs checkBox_e)
                {
                    if(checkBox.Checked)
                    {
                        checkBox.Image = imageList1.Images[1];
                    }
                    else
                    {
                        checkBox.Image = imageList1.Images[0];
                    }
                } ;
        }
            #endregion
            #region 名称 (OUT)

            textDo = new List<string>()
            {
                // 00
                "OUT0 ",
                "OUT1 ",
                "OUT2 ",
                "OUT3 ",

                "OUT4 ",
                "OUT5",
                "OUT6",
                "OUT7",

                "OUT8",
                "OUT9",
                "OUT10",
                "OUT11",

                "OUT12",
                "OUT13",
                "OUT14",
                "OUT15",

                // 01
                "OUT16",
                "OUT17",
            };
            #endregion

            #region CheckBox (OUT) 集合

            checkBoxDo = new List<CheckBox>()
            {
                // 00
                DMC_OUT0,
                DMC_OUT1,
                DMC_OUT2,
                DMC_OUT3,
                DMC_OUT4,
                DMC_OUT5,
                DMC_OUT6,
                DMC_OUT7,
                DMC_OUT8,
                DMC_OUT9,
                DMC_OUT10,
                DMC_OUT11,
                DMC_OUT12,
                DMC_OUT13,
                DMC_OUT14,
                DMC_OUT15,


            };

            #endregion
            #region 注册事件
            for (int i = 0; i < checkBoxDo.Count; i++)
            {
                CheckBox checkBox = checkBoxDo[i];
                checkBox.Text =  textDo[i];

                // 事件匿名注册
                checkBox.Click += delegate (object checkBox_sender, EventArgs checkBox_e)
                {
                    for (int j = 0; j < checkBoxDo.Count; j++)
                    {
                        if (!checkBox_sender.Equals(checkBoxDo[j]))
                            continue;

                        com.SetDo(j, checkBoxDo[j].Checked ? 1 : 0);
                    }
                };
                checkBox.Image = imageList1.Images[0];
                checkBox.CheckedChanged += delegate (object checkBox_sender, EventArgs checkBox_e)
                {
                    if (checkBox.Checked)
                    {
                        checkBox.Image = imageList1.Images[1];
                    }
                    else
                    {
                        checkBox.Image = imageList1.Images[0];
                    }
                };
            }
            #endregion
            #region 名称 (AI)

            textAi = new List<string>()
            {
                // 0
                "",
                "",

                "",
                "",
                "",
                "",

                "",
                "",
                "",
                "",

            };
            for (int i = 0; i < textAi.Count; i++)
            {
                // 行追加
                dataGridViewAi.Rows.Add();

                DataGridViewCellCollection cells = dataGridViewAi.Rows[i].Cells;

                // ch.
                cells["columnAiCh"].Value = i.ToString();

                // 名称
                cells["columnAiName"].Value = textAi[i];
            }
            #endregion
            #region 名称 (AO)

            textAo = new List<string>()
            {
                // 0
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
            };
            for (int i = 0; i < textAo.Count; i++)
            {
                // 行追加
                dataGridViewAo.Rows.Add();

                DataGridViewCellCollection cells = dataGridViewAo.Rows[i].Cells;

                // ch.
                cells["columnAoCh"].Value = i.ToString();

                // 名称
                cells["columnAoName"].Value = textAo[i];
            }
            #endregion

            this.timer1.Enabled = true;
        }

        private void dataGridViewAi_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                object obj = dataGridViewAi[e.ColumnIndex, e.RowIndex].Value;

                if (obj == null)
                    return;

                string text = obj.ToString();

                // 設定値を取得
                int value;
                System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
                System.Globalization.NumberStyles style = checkBoxAiHex.Checked ?
                    System.Globalization.NumberStyles.HexNumber : System.Globalization.NumberStyles.Number;

                if (int.TryParse(text, style, provider, out value))
                {
                    // 16bit
                    if (checkBoxAiHex.Checked)
                    {
                        if (ushort.MinValue <= value && value <= ushort.MaxValue)
                            value -= 65536;
                    }

                    if (short.MinValue <= value && value <= short.MaxValue)
                    {
                        com.SetAi(e.RowIndex, value);
                    }
                }
            }
            catch
            {

            }
        }

        private void dataGridViewAo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            object obj = dataGridViewAo[e.ColumnIndex, e.RowIndex].Value;

            if (obj == null)
                return;

            string text = obj.ToString();

            // 設定値を取得
            int value;
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            System.Globalization.NumberStyles style = checkBoxAoHex.Checked ?
                System.Globalization.NumberStyles.HexNumber : System.Globalization.NumberStyles.Number;

            if (int.TryParse(text, style, provider, out value))
            {
                // 16bit
                if (checkBoxAoHex.Checked)
                {
                    if (ushort.MinValue <= value && value <= ushort.MaxValue)
                        value -= 65536;
                }

                if (short.MinValue <= value && value <= short.MaxValue)
                {
                    com.SetAo(e.RowIndex, value);
                }
                    
            }
        }

        public void TimerEvt()
        {
            this.IOUpdate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimerEvt();
        }
    }
}
