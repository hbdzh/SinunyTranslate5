using Newtonsoft.Json.Linq;
using SinunyTranslate.Common;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SinunyTranslate.Utility
{
    internal class Trans_Baidu
    {
        /// <summary>
        /// 获取JSON代码
        /// </summary>
        /// <param name="q">要翻译的内容</param>
        /// <param name="from">源语言代码</param>
        /// <param name="to">目标语言代码</param>
        /// <returns></returns>
        internal static async Task<string> GetJson(string q, string from, string to)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("https://fanyi-api.baidu.com/api/trans/vip/translate?");
            sb.Append("q=" + Uri.EscapeDataString(q));
            sb.Append("&from=" + from);
            sb.Append("&to=" + to);
            sb.Append("&appid=" + ApiSign.BaiduAppID);
            sb.Append("&salt=" + ApiSign.Salt);
            sb.Append("&sign=" + ApiSign.GenerateSign(ApiSign.BaiduAppID, q, ApiSign.Salt, ApiSign.BaiduAppSecret));
            Uri uri = new Uri(sb.ToString());
            using (HttpClient httpClient = new HttpClient())
            {
                string response = await httpClient.GetStringAsync(uri);
                return response;
            }
        }
        /// <summary>
        ///解析Json代码
        /// </summary>
        /// <param name="jsonCode">Json代码</param>
        /// <returns>返回翻译的字符串</returns>
        internal static StringBuilder GetResult(string jsonCode)
        {
            StringBuilder sb = new StringBuilder();
            string keyWord = "dst";//要判断的出现在json中的子串
            if (jsonCode.Contains(keyWord))//判断是否存在keyword
            {
                int count = (jsonCode.Length - jsonCode.Replace(keyWord, null).Length) / keyWord.Length;//存储keyword出现次数的int
                for (int j = 0; j < count; j++)
                {
                    sb.AppendLine((string)JObject.Parse(jsonCode)["trans_result"][j][keyWord]);//将翻译结果添加到集合
                }
            }
            return sb;//返回添加满翻译结果的list
        }
    }
}
