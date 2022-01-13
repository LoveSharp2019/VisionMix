using System;
using System.Text.RegularExpressions;

namespace Yoga.Common.Helpers
{
    /// <summary>
    /// IP帮助类
    /// </summary>
    public class IPHelper
    {
        private static bool IsIPAddress(string str)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(str) || str.Length < 7 || str.Length > 15)
                    return false;
                const string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}{1}";
                var regex = new Regex(regformat, RegexOptions.IgnoreCase);
                return regex.IsMatch(str);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
