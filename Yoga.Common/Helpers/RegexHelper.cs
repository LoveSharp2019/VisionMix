﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Yoga.Common.Helpers
{
    /// <summary>
    /// 正则帮助类。含大量常用正则表达式。
    /// </summary>
    public class RegexHelper
    {
        /// <summary>
        /// 是否手机号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(string str)
        {
            return Regex.IsMatch(str, @"(^0?1[3|4|5|7|8][0-9]\d{8}$)");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64String(string str)
        {
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }
        /// <summary>
        /// 判断字符串中是否包含中文
        /// </summary>
        /// <param name="str">需要判断的字符串</param>
        /// <returns>判断结果</returns>
        public static bool HasChinese(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }
        public static bool IsDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return false;
            }
            DateTime minValue = DateTime.MinValue;
            return DateTime.TryParse(date, out minValue);
        }

        public static bool IsDate(string date, string format)
        {
            return IsDate(date, format, null, DateTimeStyles.None);
        }

        public static bool IsDate(string date, string format, IFormatProvider provider, DateTimeStyles styles)
        {
            if (string.IsNullOrEmpty(date))
            {
                return false;
            }
            DateTime minValue = DateTime.MinValue;
            return DateTime.TryParseExact(date, format, provider, styles, out minValue);
        }

        public static bool IsEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            string pattern = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
            return Regex.IsMatch(email.Trim(), pattern);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool IsGuid(string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                return false;
            }
            return Regex.IsMatch(guid, "[A-F0-9]{8}(-[A-F0-9]{4}){3}-[A-F0-9]{12}|[A-F0-9]{32}", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        public static bool IsIdCard(string idCard)
        {
            if (string.IsNullOrEmpty(idCard))
            {
                return false;
            }
            if (idCard.Length == 15)
            {
                return Regex.IsMatch(idCard, @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$");
            }
            return ((idCard.Length == 0x12) && Regex.IsMatch(idCard, @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[A-Z])$", RegexOptions.IgnoreCase));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsInt(object number)
        {
            if (IsNullOrEmpty(number))
            {
                return false;
            }
            return IsInt(number.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsInt(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            int result = 0;
            return int.TryParse(number, out result);
        }

        public static bool IsIP(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return false;
            }
            string pattern = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";
            return Regex.IsMatch(ip.Trim(), pattern);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(object data)
        {
            return ((data == null) || (((data.GetType() == typeof(string)) && string.IsNullOrEmpty(data.ToString().Trim())) || (data.GetType() == typeof(DBNull))));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsNumber(object number)
        {
            if (IsNullOrEmpty(number))
            {
                return false;
            }
            return IsNumber(number.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            decimal result = 0M;
            return decimal.TryParse(number, out result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public static bool IsUrl(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 判断是否是mac地址
        /// </summary>
        /// <Param name="mac">mac地址字符串</Param>
        /// <returns></returns>
        public static bool IsMacAddress(string mac)
        {
            return Regex.IsMatch(mac, "^([0-9A-F]{2}-){5}[0-9A-F]{2}$") || Regex.IsMatch(mac, "^[0-9A-F]{12}$");
        }

        /// <summary>
        /// 获取字节数
        /// str：需要获取的字符串
        /// </summary>
        public static int Length(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return 0;
            }
            int j = 0;
            CharEnumerator ce = str.GetEnumerator();
            while (ce.MoveNext())
            {
                j += (ce.Current > 0 && ce.Current < 255) ? 1 : 2;
            }
            return j;
        }
        /// <summary>
        /// 按字节数截取指定字节
        /// </summary>
        /// <Param name="str">需要获取的字符串</Param>
        /// <Param name="length">获取的字节长度</Param>
        /// <returns></returns>
        [Obsolete("该方法已移至StringHelper")]
        public static string SubString(string str, int length)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            string result = str;
            int j = 0, k = 0;
            CharEnumerator ce = str.GetEnumerator();
            while (ce.MoveNext())
            {
                j += (ce.Current > 0 && ce.Current < 255) ? 1 : 2;

                if (j <= length)
                {
                    k++;
                }
                else
                {
                    result = str.Substring(0, k);
                    break;
                }
            }
            return result;
        }
    }
}
