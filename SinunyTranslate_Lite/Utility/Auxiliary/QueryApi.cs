using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SinunyTranslate_Lite.Utility.Auxiliary
{
    internal class QueryApi
    {
        /// <summary>
        /// 获取Json代码
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        internal static async Task<string> GetJson(string url, string word)
        {
            //对多余字符进行处理
            Regex reg = new Regex(@"[^\u4e00-\u9fa5]+");
            word = reg.Replace(word, "");
            Uri uri = new Uri(url + word);
            using (HttpClient httpClient = new HttpClient())
            {
                string response = await httpClient.GetStringAsync(uri);
                return response;
            }
        }
        /// <summary>
        /// 近义词查询
        /// </summary>
        /// <param name="jsonCode"></param>
        /// <returns></returns>
        internal static string JinyiQuery(string jsonCode)
        {
            JObject resultChild = JObject.Parse(JObject.Parse(jsonCode)["result"].ToString());
            return resultChild["jin"].ToString().Replace("<br>", "\r\n").Replace("[", "").Replace("]", "").Trim().Replace
                (" ", "").Replace("\"", "");
        }
        /// <summary>
        /// 反义词查询
        /// </summary>
        /// <param name="jsonCode"></param>
        /// <returns></returns>
        internal static string FanyiQuery(string jsonCode)
        {
            JObject resultChild = JObject.Parse(JObject.Parse(jsonCode)["result"].ToString());
            return resultChild["fan"].ToString().Replace("<br>", "\r\n").Replace("[", "").Replace("]", "").Trim().Replace
                    (" ", "").Replace("\"", "");
        }
        /// <summary>
        /// 成语查询
        /// </summary>
        /// <param name="jsonCode">Json代码</param>
        /// <returns>返回查询的内容</returns>
        internal static string IdiomsQuery(string jsonCode)
        {
            JObject resultChild = JObject.Parse(JObject.Parse(jsonCode)["result"].ToString());
            //返回翻译后的目标结果，1：拼音 2：成语解释 3：出处 4：例子 5：语法
            StringBuilder sb = new StringBuilder();
            sb.Append("拼音：");
            sb.AppendLine((string)resultChild["pinyin"]);
            sb.Append("成语解释：");
            sb.AppendLine((string)resultChild["chengyujs"]);
            sb.Append("出处：");
            sb.AppendLine((string)resultChild["from_"]);
            sb.Append("例子：");
            sb.AppendLine((string)resultChild["example"]);
            sb.Append("语法：");
            sb.AppendLine((string)resultChild["yufa"]);
            return sb.ToString();
        }
        /// <summary>
        /// 词语查询
        /// </summary>
        /// <param name="jsonCode">Json代码</param>
        /// <returns>返回字符串</returns>
        internal static string WordsQuery(string jsonCode)
        {
            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonCode);
            if (jsonCode.Contains("250"))
            {
                return "没有查到该词";
            }
            else
            {
                return jo["newslist"][0]["content"].ToString().Replace("。", "。\r\n");
            }
        }
    }
}
