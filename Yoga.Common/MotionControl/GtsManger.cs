using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Yoga.Common.MotionControl
{
    public class GtsManger
    {
        public static GtsManger Instance = new GtsManger();
        bool[] en = new bool[8];
        bool[] isHome = new bool[8];

        //轴号
        const short AXIS_X = 1;
        const short AXIS_Y = 2;
        gts.mc.TTrapPrm trap;
        public bool GetHomeStatus()
        {
            if (isHome[0] && isHome[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Init()
        {
            gts.mc.GT_Open(0, 1);//打开运动控制卡
            gts.mc.GT_LoadConfig("GT800_test.cfg");//下载配置文件
            gts.mc.GT_ClrSts(1, 8);//清除各轴报警和限位
            for (int i = 0; i < 2; i++)
            {
                short axis = (short)(i + 1);
                if (!en[axis - 1])
                {
                    gts.mc.GT_AxisOn(axis);//上伺服
                }
                //if (en[axis - 1])
                //{
                //    gts.mc.GT_AxisOff(axis);//下伺服
                //}
                en[axis - 1] = !en[axis - 1];
            }
        }
        public void Move2Pos(short axis, int pos, double speed)
        {
            trap.acc = 0.5;
            trap.dec = 0.5;
            trap.smoothTime = 0;
            trap.velStart = 0;

            gts.mc.GT_SetTrapPrm(axis, ref trap);//设置点位运动参数
            gts.mc.GT_SetVel(axis, speed);//设置目标速度
            gts.mc.GT_SetPos(axis, pos);//设置目标位置
            gts.mc.GT_Update(1 << (axis - 1));//更新轴运动
        }
        public void WaitMoved(short axis)
        {
            int sts;
            uint clk;
            // 等待轴进入误差带
            do
            {
                gts.mc.GT_GetSts(axis, out sts, 1, out clk);
                System.Windows.Forms.Application.DoEvents();

            } while (0x800 != (sts & 0x800));

        }
        public bool GetDi(int index)
        {
            int lGpiValue;
            // 读取EXI3输入值
            gts.mc.GT_GetDi(gts.mc.MC_GPI, out lGpiValue);

            // 如果为高电平，说明按键正在被按下，则不检测，返回1
            bool status = ((lGpiValue & (1 << index)) != 0);
            return status;
        }
        public void HomeAll()
        {
            for (int i = 0; i < 2; i++)
            {
                Task.Run(() =>
                {
                    short axis = (short)(i + 1);
                    double speed = 1;
                    isHome[axis - 1] = false;
                    //去一个端点
                    Move2Pos(axis, -2000000, speed);
                    WaitMoved(axis);

                    double pos1;
                    uint clk;
                    gts.mc.GT_GetPrfPos(AXIS_X, out pos1, 1, out clk);
                    //后退小段
                    pos1 += 100;
                    Move2Pos(axis, (int)pos1, speed);
                    WaitMoved(axis);
                    //慢速到达端点
                    Move2Pos(axis, -2000000, speed / 10.0);
                    //轴状态清零
                    gts.mc.GT_ZeroPos(1, 8);
                    isHome[axis - 1] = true;
                });
            }
        }
        public void GetPos(out double pos1, out double pos2)
        {
            pos1 = 0;
            pos2 = 0;
            uint clk;
            if (en[0])
            {

                gts.mc.GT_GetPrfPos(AXIS_X, out pos1, 1, out clk);
            }
            if (en[1])
            {

                gts.mc.GT_GetPrfPos(AXIS_X, out pos2, 2, out clk);
            }
        }
    }
}
