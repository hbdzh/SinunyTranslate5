using System;
using System.Security.Cryptography;
using System.Text;

namespace SinunyTranslate.Common
{
    internal class ApiSign
    {
        internal static string YoudaoAppID { get; set; }
        internal static string YoudaoAppSecret { get; set; }
        internal static string BaiduAppID { get; set; }
        internal static string BaiduAppSecret { get; set; }
        /// <summary>
        /// 随机数
        /// </summary>
        internal static string Salt { get; } = $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.DayOfWeek}{DateTime.Now.DayOfYear}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}";
        /// <summary>
        /// 获取签名(OAuth协议规则)  ,appid + q + salt + key
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="q"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        internal static string GenerateSign(string appid, string q, string salt, string appSecret)
        {
            MD5 md5 = MD5.Create();
            //将字符串转换成字节数组
            byte[] byteOld = Encoding.UTF8.GetBytes(appid + q + salt + appSecret);
            //调用加密方法
            byte[] byteNew = md5.ComputeHash(byteOld);
            //将加密结果转换为字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteNew)
            {
                //将字节转换成16进制表示的字符串，
                sb.Append(b.ToString("x2"));
            }
            //返回加密的字符串
            return sb.ToString().PadLeft(32, '0');
        }
    }
}
