using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SinunyTranslate.Utility
{
    internal class Trans_YoudaoFree
    {
        /// <summary>
        /// 获取JSON代码
        /// </summary>
        /// <param name="q">要翻译的内容</param>
        /// <param name="type">语言代码</param>
        /// <returns></returns>
        internal static async Task<string> GetJson(string q, string type)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("http://fanyi.youdao.com/translate?&doctype=json&");
            sb.Append("type=" + type);
            sb.Append("&i=" + q);
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
        internal static string GetResult(string jsonCode)
        {
            if (JsonConvert.DeserializeObject(jsonCode) is JObject jo)
            {
                if ((string)jo["errorCode"] == "0")
                {
                    return (string)jo["translateResult"][0][0]["tgt"];
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}
