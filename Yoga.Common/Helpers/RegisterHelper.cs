using System;
using System.Management;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

namespace Yoga.Common.Helpers
{
    public class RegisterHelper
    {
        // 取得设备硬盘的卷标号
        public static string GetDiskVolumeSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                String strHardDiskID = null;
                foreach (ManagementObject mo in searcher.Get())
                {
                    strHardDiskID = Regex.Replace(mo["SerialNumber"].ToString().Trim(), "[^0-9A-Za-z]", ""); ;
                    break;
                }
                return strHardDiskID;
            }
            catch
            {
                return "";
            }
        }
        //获得CPU的序列号
        public static string getCpu()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuConnection)
            {
                strCpu = Regex.Replace(myObject.Properties["Processorid"].Value.ToString(), "[^0-9A-Za-z]", "");
                break;
            }         
            return strCpu;
        }
        /// <summary>
        /// 机器码
        /// </summary>
        /// <returns></returns>
        public static string getMNum()
        {
            string strNum = getCpu();// + GetDiskVolumeSerialNumber();//获得24位Cpu和硬盘序列号
           // string strMNum = strNum.Substring(strNum.Length - 24, 24);//从生成的字符串中取出前24个字符做为机器码
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(Encoding.Default.GetBytes(strNum));
            string dat = BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文本

            return dat;
        }
        public static bool CheckMnum(string code)
        {
            string dat= Yoga.Wrapper.Fun.EncryptString(code);
            string datOK= Yoga.Wrapper.Fun.EncryptString(getMNum());
            return dat == datOK;
        }
        public static int[] intCode = new int[127];//存储密钥
        public static int[] intNumber = new int[25];//存机器码的Ascii值
        public static char[] Charcode = new char[25];//存储机器码字
        /// <summary>
        /// 
        /// </summary>
        public static void setIntCode()
        {
            for (int i = 1; i < intCode.Length; i++)
            {
                intCode[i] = i % 9;
            }
        }
        /// <summary>
        /// 生成注册码 
        /// </summary>
        /// <returns></returns>
        public static string getRNum()
        {
            setIntCode();//初始化127位数组
            for (int i = 1; i < Charcode.Length; i++)//把机器码存入数组中
            {
                Charcode[i] = Convert.ToChar(getMNum().Substring(i - 1, 1));
            }
            for (int j = 1; j < intNumber.Length; j++)//把字符的ASCII值存入一个整数组中。
            {
                intNumber[j] = intCode[Convert.ToInt32(Charcode[j])] + Convert.ToInt32(Charcode[j]);
            }
            string strAsciiName = "";//用于存储注册码
            for (int j = 1; j < intNumber.Length; j++)
            {
                if (intNumber[j] >= 48 && intNumber[j] <= 57)//判断字符ASCII值是否0－9之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 65 && intNumber[j] <= 90)//判断字符ASCII值是否A－Z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 97 && intNumber[j] <= 122)//判断字符ASCII值是否a－z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else//判断字符ASCII值不在以上范围内
                {
                    if (intNumber[j] > 122)//判断字符ASCII值是否大于z
                    {
                        strAsciiName += Convert.ToChar(intNumber[j] - 10).ToString();
                    }
                    else
                    {
                        strAsciiName += Convert.ToChar(intNumber[j] - 9).ToString();
                    }
                }
            }
            return strAsciiName;
        }
    }
}
