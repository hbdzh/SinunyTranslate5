using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SinunyTranslate.Common;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SinunyTranslate.Utility
{
    internal class Trans_Youdao
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
            q = q.Replace("\r", Environment.NewLine);//将\r换行符替换成C#能识别的
            StringBuilder sb = new StringBuilder();
            sb.Append("https://openapi.youdao.com/api?");
            sb.Append("appKey=" + ApiSign.YoudaoAppid);
            sb.Append("&q=" + Uri.EscapeDataString(q));
            sb.Append("&from=" + from);
            sb.Append("&to=" + to);
            sb.Append("&sign=" + ApiSign.GenerateSign(ApiSign.YoudaoAppid, q, ApiSign.Salt, ApiSign.YoudaoAppSecret));
            sb.Append("&salt=" + ApiSign.Salt);
            Uri uri = new Uri(sb.ToString());
            using (HttpClient httpClient = new HttpClient())
            {
                string response = await httpClient.GetStringAsync(uri);
                return response;
            }
        }
        /// <summary>
        /// 解析Json代码
        /// </summary>
        /// <param name="jsonCode"></param>
        /// <returns></returns>
        internal static string[] GetResult(string jsonCode)
        {
            string[] result = new string[3];
            if (JsonConvert.DeserializeObject(jsonCode) is JObject jo)
            {
                if ((string)jo["errorCode"] == "0")
                {
                    result[0] = (string)jo["translation"][0];
                    if (jsonCode.Contains("\"basic\":{") && jsonCode.Contains("\"explains\":"))//如果json数组里有这两个东西，说明该词有例子，可以执行代码
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in jo["basic"]["explains"])
                        {
                            sb.AppendLine(item.ToString());
                        }
                        result[1] = sb.ToString();
                    }
                    if (jsonCode.Contains("\"web\":[") && jsonCode.Contains("\"value\":"))//同上，不过这个是解释
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in jo["web"][1]["value"])
                        {
                            sb.AppendLine(item.ToString());
                        }
                        result[2] = sb.ToString();
                    }
                    return result;
                }
                else
                {
                    result[0] = "";
                    result[1] = "";
                    result[2] = "";
                    return result;
                }
            }
            else
            {
                result[0] = "";
                result[1] = "";
                result[2] = "";
                return result;
            }
        }
    }
}
