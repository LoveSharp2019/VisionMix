using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Yoga.Common.MotionControl
{
    /// <summary>
    /// 固高驱动
    /// </summary>
    public class GT400
    {

        /// <summary>
        /// 立即停止当前轴的运动
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_AbptStp();
        /// <summary>
        /// 追加命令缓冲区
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_AddList();
        /// <summary>
        /// 关闭驱动器报警信号
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_AlarmOff();
        /// <summary>
        /// 打开驱动器报警信号
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_AlarmOn();
        /// <summary>
        /// XY平面圆弧插补（以圆心位置和角度为输入参数）
        /// </summary>
        /// <param name="x_center"></param>
        /// <param name="y_center"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_ArcXY(double x_center, double y_center, double angle);
        /// <summary>
        /// XY平面圆弧插补（以终点位置和半径为输入参数）
        /// </summary>
        /// <param name="x_end"></param>
        /// <param name="y_end"></param>
        /// <param name="r"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_ArcXYP(double x_end, double y_end, double r, short direction);
        /// <summary>
        /// YZ平面圆弧插补（以圆心位置和角度为输入参数）
        /// </summary>
        /// <param name="y_center"></param>
        /// <param name="z_center"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_ArcYZ(double y_center, double z_center, double angle);
        /// <summary>
        /// YZ平面圆弧插补（以终点位置和半径为输入参数）
        /// </summary>
        /// <param name="y_end"></param>
        /// <param name="z_end"></param>
        /// <param name="r"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_ArcYZP(double y_end, double z_end, double r, short direction);
        /// <summary>
        /// ZX平面圆弧插补（以圆心位置和角度为输入参数）
        /// </summary>
        /// <param name="z_center"></param>
        /// <param name="x_center"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_ArcZX(double z_center, double x_center, double angle);
        /// <summary>
        /// ZX平面圆弧插补（以终点位置和半径为输入参数）
        /// </summary>
        /// <param name="z_end"></param>
        /// <param name="x_end"></param>
        /// <param name="r"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_ArcZXP(double z_end, double x_end, double r, short direction);
        /// <summary>
        /// 设置运动出错自动停止无效
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_AuStpOff();
        /// <summary>
        /// 设置运动出错自动停止有效
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_AuStpOn();
        /// <summary>
        /// 关闭当前轴控制参数和命令自动更新
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_AuUpdtOff();
        /// <summary>
        /// 设置当前轴控制参数和命令自动更新
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_AuUpdtOn();
        /// <summary>
        /// 设置某控制轴为当前轴
        /// </summary>
        /// <param name="axis">1、2、3、4 四个数中取值，分别代表第一、二、三、四轴</param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_Axis(ushort axis);
        /// <summary>
        /// 关闭当前轴
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_AxisOff();
        /// <summary>
        /// 使当前轴处于工作状态
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_AxisOn();
        /// <summary>
        /// 清除当前轴断点并关闭断点模式
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_BrkOff();
        /// <summary>
        /// 设置允许当前轴捕获HOME 信号
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_CaptHome();
        /// <summary>
        /// 设置允许当前轴捕获INDEX 信号
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_CaptIndex();
        /// <summary>
        /// 设置探针捕获功能
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_CaptProb();
        /// <summary>
        /// 关闭运动控制器设备
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_Close();
        /// <summary>
        /// 设置当前轴为闭环伺服控制
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_CloseLp();
        /// <summary>
        /// 清零辅助编码器位置值
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_ClrEncPos(ushort number);
        /// <summary>
        /// 清除当前轴状态
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_ClrSts();
        /// <summary>
        /// 关闭坐标系运动异常自动停止方式
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_CrdAuStpOff();
        /// <summary>
        /// 打开坐标系运动异常自动停止方式
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_CrdAuStpOn();
        /// <summary>
        /// 设置当前轴控制输出为模拟量输出还是脉冲输出
        /// </summary>
        /// <param name="mode">0 表示为模拟电压输出模式，1 表示为脉冲输出模式。运动控制器默认的状态为模拟电压输出模式。</param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_CtrlMode(ushort mode);

        /// <summary>
        /// 使当前轴的伺服驱动器复位
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_DrvRst();
        /// <summary>
        /// 获取辅助编码器位置
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_EncPos(ushort axis, out int pos);
        /// <summary>
        /// 设置编码器的计数方向
        /// </summary>
        /// <param name="sense"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_EncSns(ushort sense);
        /// <summary>
        /// 获取辅助编码器速度
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="vel"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_EncVel(ushort axis, out double vel);
        /// <summary>
        /// 关闭命令缓冲区
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_EndList();
        /// <summary>
        /// 紧急停止坐标系运动
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_EStpMtn();
        /// <summary>
        /// 读取通用输入端口的状态
        /// </summary>
        /// <param name="io_input">16位对应16个输入口状态Bit0----EXI0 Bit15----EXI15</param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_ExInpt(out ushort io_input);
        /// <summary>
        /// 设置通用输出端口的状态
        /// </summary>
        /// <param name="io_output"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_ExOpt(ushort io_output);
        /// <summary>
        /// 设置通用IO 指定输出端口的输出状态（Firmware 版本为Ver2.50 或者以上的）
        /// </summary>
        /// <param name="bit"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_ExOptBit(ushort bit, ushort value);
        /// <summary>
        /// 设置当前轴原点信号触发断点模式
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_ExtBrk();
        /// <summary>
        /// 读取当前轴的命令加速度
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetAcc(out double acc);
        /// <summary>
        /// 读取当前轴加速度极限
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetAccLmt(out uint limit);
        /// <summary>
        /// 读取AD 转换结果
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="voltage"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetAdc(ushort channel, out ushort voltage);
        /// <summary>
        /// 读取当前轴的实际位置误差
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetAtlErr(out ushort error);
        /// <summary>
        ///读取当前轴的实际位置
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetAtlPos(out int pos);
        /// <summary>
        /// 获取控制器当前轴的实际速度
        /// </summary>
        /// <param name="vel"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetAtlVel(out double vel);
        /// <summary>
        /// 读取当前轴断点位置比较值
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetBrkCn(out int pos);
        /// <summary>
        /// 读取缓冲区命令执行中断时的断点位置
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetBrkPnt(out double point);
        /// <summary>
        /// 读取当前轴INDEX 或HOME 捕获位置值
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetCapt(out int pos);
        /// <summary>
        /// 读取控制器系统时钟,长度为32位,每经过一个
        /// 伺服周期累加一次,可以使用GT_Reset指令复位清零时钟
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetClock(out uint time);
        /// <summary>
        /// 读取上一条控制命令的执行状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetCmdSts(out ushort status);
        /// <summary>
        /// 读取坐标系状态字
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetCrdSts(out ushort status);
        /// <summary>
        /// 读取控制器坐标系运动的合成规划速度
        /// </summary>
        /// <param name="vel"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetCrdVel(out double vel);
        /// <summary>
        /// 获取当前控制卡的卡号
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetCurrentCardNo();
        /// <summary>
        /// 读取辅助编码器捕获值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetEncCapt(out int value);
        /// <summary>
        /// 读取辅助编码器状态
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetEncSts(out ushort value);
        /// <summary>
        /// 读取辅助编码器状态
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetEncStsEx(out uint value);
        /// <summary>
        /// 读取通用io的输出状态
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetExOpt(out ushort value);
        /// <summary>
        /// 读取各轴home开关的电平状态
        /// </summary>
        /// <param name="home"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetHomeSwt(out ushort home);
        /// <summary>
        /// 读取当前轴的伺服滤波器积分误差饱和值
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetILmt(out ushort limit);
        /// <summary>
        /// 读取当前轴的积分位置误差
        /// </summary>
        /// <param name="integral"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetIntgr(out short integral);
        /// <summary>
        /// 读取当前轴的命令加加速度
        /// </summary>
        /// <param name="jerk"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetJerk(out double jerk);
        /// <summary>
        /// 读取当前轴的伺服滤波器加速度前馈增益
        /// </summary>
        /// <param name="kaff"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetKaff(out ushort kaff);
        /// <summary>
        /// 读取当前轴的伺服滤波器微分增益
        /// </summary>
        /// <param name="kd"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetKd(out ushort kd);
        /// <summary>
        /// 读取当前轴的伺服滤波器积分增益
        /// </summary>
        /// <param name="ki"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetKi(out ushort ki);
        /// <summary>
        /// 读取当前轴的伺服滤波器比例增益
        /// </summary>
        /// <param name="kp"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetKp(out ushort kp);
        /// <summary>
        /// 读取当前轴的伺服滤波器速度前馈增益
        /// </summary>
        /// <param name="kvff"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetKvff(out ushort kvff);
        /// <summary>
        /// 读取限位开关的状态
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetLmtSwt(out ushort limit);
        /// <summary>
        /// 读取当前轴的最大命令加速度
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetMAcc(out double acc);



        /// <summary>
        /// 读取当前轴模式字
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetMode(out ushort mode);
        /// <summary>
        /// 读取缓冲区当前执行段号
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetMtnNm(out ushort number);
        /// <summary>
        /// 读取当前轴的伺服滤波器输出零点偏移值
        /// </summary>
        /// <param name="bias"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetMtrBias(out short bias);
        /// <summary>
        /// 开环方式下读取当前轴电机控制值
        /// </summary>
        /// <param name="voltage"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetMtrCmd(out short voltage);
        /// <summary>
        /// 读取当前轴的伺服滤波器输出饱和值
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetMtrLmt(out ushort limit);
        /// <summary>
        /// 读取当前轴的命令位置
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetPos(out int pos);
        /// <summary>
        /// 读取当前轴的伺服滤波器位置误差极限
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetPosErr(out ushort limit);
        /// <summary>
        /// 读取坐标系各轴坐标值
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetPrfPnt(out double point);
        /// <summary>
        /// 读取运动控制器当前轴的规划位置
        /// </summary>
        /// <param name="pos">该位置值</param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetPrfPos(out int pos);
        /// <summary>
        /// 读取控制器当前轴的规划速度
        /// </summary>
        /// <param name="vel"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetPrfVel(out double vel);
        /// <summary>
        /// 读取当前轴的电子齿轮比
        /// </summary>
        /// <param name="ratio"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetRatio(out double ratio);
        /// <summary>
        /// 读取控制器伺服采样周期
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetSmplTm(out double time);
        /// <summary>
        /// 读取当前轴状态字
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetSts(out ushort status);
        /// <summary>
        /// 读取当前轴状态字（Firmware 版本为Ver2.50 或者以上的）
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetStsEx(out uint status);
        /// <summary>
        /// 读取当前轴的命令速度
        /// </summary>
        /// <param name="vel"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetVel(out double vel);
        /// <summary>
        /// 硬件复位运动控制器
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_HardRst();
        /// <summary>
        /// 设置各轴home信号捕捉的触发沿
        /// </summary>
        /// <param name="sense"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_HomeSns(ushort sense);
        /// <summary>
        /// 设置限位开关的有效电平
        /// </summary>
        /// <param name="sense"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_LmtSns(ushort sense);
        /// <summary>
        /// 关闭当前轴的限位开关
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_LmtsOff();
        /// <summary>
        /// 使当前轴的限位开关有效
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_LmtsOn();
        /// <summary>
        /// 两维直线插补
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_LnXY(double x, double y);
        /// <summary>
        /// 三维直线插补
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_LnXYZ(double x, double y, double z);
        /// <summary>
        /// 四维直线插补
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_LnXYZA(double x, double y, double z, double a);
        /// <summary>
        /// 映射坐标系，即建立电机轴与坐标系之间的关系，
        /// 其中包含当量的转换。
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_MapAxis(ushort axis, ref double map);
        /// <summary>
        /// 坐标系映射
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_MltiUpdt(ushort mask);
        /// <summary>
        /// 设置当前轴运动到位触发断点模式
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_MtnBrk();
        /// <summary>
        /// 定位缓冲区命令起点（二维）
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_MvXY(double x, double y, double vel, double acc);
        /// <summary>
        /// 定位缓冲区命令起点（三维）
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_MvXYZ(double x, double y, double z, double vel, double acc);
        /// <summary>
        /// 定位缓冲区命令起点（四维）
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="a"></param>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_MvXYZA(double x, double y, double z, double a, double vel, double acc);
        /// <summary>
        /// 设置当前轴负向位置断点模式
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_NegBrk();
        /// <summary>
        /// 打开运动控制器设备
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_Open();
        /// <summary>
        /// 设置当前轴为开环控制
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_OpenLp();
        /// <summary>
        /// 设置当前轴正向位置断点模式
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_PosBrk();
        /// <summary>
        /// 设置当前轴的运动模式为电子齿轮模式
        /// </summary>
        /// <param name="master"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_PrflG(ushort master);
        /// <summary>
        /// 设置当前轴的运动模式为S-曲线模式
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_PrflS();
        /// <summary>
        /// 设置当前轴的运动模式为梯形曲线模式
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_PrflT();
        /// <summary>
        /// 设置当前轴的运动模式为速度控制模式
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_PrflV();
        /// <summary>
        /// 关闭探针急停功能。成功调用该指令以后，当探针信号触发时，置起捕获
        /// 触发标志，各轴运动不受影响
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_ProbStopOff();
        /// <summary>
        /// 打开探针急停功能。成功调用该指令以后，当探针信号触发时，自动急停
        /// 所有轴，并置起捕获触发标志
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_ProbStopOn();
        /// <summary>
        /// 复位运动控制器
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_Reset();
        /// <summary>
        /// 清除当前轴状态
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_RstSts(ushort mask);
        /// <summary>
        /// 设置当前轴的加速度（梯形曲线模式、速度控制模式）
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetAcc(double acc);
        /// <summary>
        /// 设置当前轴的加速度极限（坐标运动模式）
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetAccLmt(double limit);
        /// <summary>
        /// 设置当前轴的实际位置
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetAtlPos(int pos);
        /// <summary>
        /// 设置当前轴断点位置比较值（与正向或负向位置断点模式一起使用）
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetBrkCn(int pos);
        /// <summary>
        /// 设置辅助编码器INDEX 捕获
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetEncCapt();
        /// <summary>
        /// 设置当前轴的伺服滤波器误差积分饱和值
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetILmt(ushort limit);
        /// <summary>
        /// 设置当前轴的加加速度（S-曲线模式）
        /// </summary>
        /// <param name="jerk"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetJerk(double jerk);
        /// <summary>
        /// 设置当前轴的伺服滤波器加速度前馈增益
        /// </summary>
        /// <param name="kaff"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetKaff(ushort kaff);
        /// <summary>
        /// 设置当前轴的伺服滤波器加速度前馈增益
        /// </summary>
        /// <param name="kaff"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetKaffEx(double kaff);
        /// <summary>
        /// 设置当前轴的伺服滤波器微分增益
        /// </summary>
        /// <param name="kd"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetKd(ushort kd);
        /// <summary>
        /// 设置当前轴的伺服滤波器微分增益
        /// </summary>
        /// <param name="kd"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetKdEx(double kd);
        /// <summary>
        /// 设置当前轴的伺服滤波器积分增益
        /// </summary>
        /// <param name="ki"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetKi(ushort ki);
        /// <summary>
        /// 设置当前轴的伺服滤波器积分增益
        /// </summary>
        /// <param name="ki"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetKiEx(double ki);
        /// <summary>
        /// 设置当前轴的伺服滤波器比例增益
        /// </summary>
        /// <param name="kp"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetKp(ushort kp);
        /// <summary>
        /// 设置当前轴的伺服滤波器比例增益
        /// </summary>
        /// <param name="kp"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetKpEx(double kp);
        /// <summary>
        /// 设置当前轴的伺服滤波器速度前馈增益
        /// </summary>
        /// <param name="kvff"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetKvff(ushort kvff);
        /// <summary>
        /// 设置当前轴的伺服滤波器速度前馈增益
        /// </summary>
        /// <param name="kvff"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetKvffEx(double kvff);
        /// <summary>
        /// 设置当前轴的最大加速度（S-曲线模式）
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetMAcc(double acc);
        /// <summary>
        /// 设置当前轴的伺服滤波器输出零点偏移值
        /// </summary>
        /// <param name="bias"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetMtrBias(short bias);
        /// <summary>
        /// 开环方式下设置当前轴电机控制值
        /// </summary>
        /// <param name="voltage"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetMtrCmd(short voltage);
        /// <summary>
        /// 设置当前轴的伺服滤波器输出饱和值
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetMtrLmt(ushort limit);
        /// <summary>
        /// 设置当前轴的目标位置（S-曲线模式、梯形曲线模式）
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetPos(int pos);
        /// <summary>
        /// 设置当前轴的伺服滤波器位置误差极限
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetPosErr(ushort limit);
        /// <summary>
        /// 设置当前轴的电子齿轮比（电子齿轮模式）
        /// </summary>
        /// <param name="ratio"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetRatio(double ratio);
        /// <summary>
        /// 设置控制器伺服采样周期
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetSmplTm(double time);
        /// <summary>
        /// 设置多轴协调运动轨迹切线加速度
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetSynAcc(double acc);
        /// <summary>
        /// 设置多轴协调运动轨迹切线速度
        /// </summary>
        /// <param name="vel"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetSynVel(double vel);
        /// <summary>
        /// 设置当前轴的目标速度（S-曲线模式、梯形曲线模式、速度控制模式）
        /// </summary>
        /// <param name="vel">目标速度值。在梯形曲线模式和S-曲线模式下速度取值范围为：0 到16384。
        /// 在速度控制模式下，速度取值范围为：-16384～16384。速度值单位是脉冲数/控制周期。
        /// 需调用 GT_Update()或GT_MltiUpdt()函数后，才能使新设置的参数有效。</param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetVel(double vel);
        /// <summary>
        /// 平滑停止当前轴的运动
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SmthStp();
        /// <summary>
        /// 设置当前轴在脉冲输出模式下的输出方式为“脉冲＋ 方向”方式
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_StepDir();
        /// <summary>
        /// 设置当前轴在脉冲输出模式下的输出方式为“正负脉冲”方式
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_StepPulse();
        /// <summary>
        /// 平滑停止坐标系运动
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_StpMtn();
        /// <summary>
        /// 打开命令缓冲区
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_StrtList();
        /// <summary>
        /// 开始缓冲区命令执行
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_StrtMtn();
        /// <summary>
        /// 切换当前卡
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SwitchtoCardNo(ushort number);
        /// <summary>
        /// 设置当前轴的目标位置等于实际位置
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SynchPos();
        /// <summary>
        /// 当前轴参数更新
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_Update();
        /// <summary>
        /// 当前轴实际位置和目标位置清零
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_ZeroPos();
        /// <summary>
        /// 设置当前轴中断屏蔽字
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetIntrMsk(ushort mask);
        /// <summary>
        /// 获得GT_SetIntrMsk ()函数设置的当前轴的中断屏蔽字。
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetIntrMsk(out ushort mask);
        /// <summary>
        /// 设置控制器对主机的中断为定时中断
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_TmrIntr();
        /// <summary>
        /// 设置控制器定时中断的时间常数。控制器定时中断时间由控制器的
        /// 控制周期和该函数设置值Timer 共同确定
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetIntrTm(ushort time);
        /// <summary>
        /// 获得GT_SetIntrTm()函数设置的运动控制器定时中断的时间常数
        /// </summary>
        /// <param name="time">时间常数</param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetIntrTm(out ushort time);
        /// <summary>
        /// 设置控制器对主机的中断为轴事件中断
        /// </summary>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_EvntIntr();
        /// <summary>
        /// 运动控制器中断设置同步事件
        /// </summary>
        /// <param name="hEvent"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_SetIntSyncEvent(uint hEvent);
        /// <summary>
        /// 获得产生中断申请轴的状态字。如果执行这个命令但运动控制器又
        ///  没有产生中断申请时，GT_GetIntr()获得当前轴的状态字。
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_GetIntr(out ushort status);
        /// <summary>
        /// 清除当前轴的中断标志位。当控制器向主机申请中断后，主机应在
        /// 中断服务程序结束时执行GT_RstIntr()函数清除控制器的中断标志，使运动控制器可
        /// 再次向主机申请中断。
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        [DllImport("gt400.dll")]
        public static extern short GT_RstIntr(ushort mask);

    }
}
