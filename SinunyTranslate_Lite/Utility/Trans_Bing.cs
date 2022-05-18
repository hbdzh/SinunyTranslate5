using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SinunyTranslate_Lite.Utility
{
    internal class Trans_Bing
    {
        /// <summary>
        /// 查询词典
        /// </summary>
        /// <param name="q">要查的词</param>
        /// <returns>查询结果</returns>
        internal static async Task<string[]> QueryDict(string q)
        {
            string[] result = new string[2];
            string url = "http://cn.bing.com/dict/dict?q=" + q;
            using (HttpClient client = new HttpClient())
            {
                string html = await client.GetStringAsync(url);
                MatchCollection mc = Regex.Matches(html, @"""description""([\s\S]+?)>");
                List<string> list = new List<string>();
                list.Add(mc[0].Groups[0].Value);
                string txt = string.Join("\r\n", list).Substring(23);
                int index = txt.IndexOf('"');
                string bb = txt.Remove(index).Replace("，", Environment.NewLine).Replace("，", Environment.NewLine).Replace("；", Environment.NewLine).Replace(";", Environment.NewLine).Replace(" ", "").Replace("：", "：" + Environment.NewLine).Replace(":", ":" + Environment.NewLine).Replace("必应词典为您提供" + q + "的释义\r\n", "");
                if (bb.Contains("网络释义："))
                {
                    result[0] = bb.Substring(0, bb.IndexOf("网络释义："));
                    result[1] = bb.Substring(bb.LastIndexOf("网络释义：") + 7);
                }
                return result;
            }
        }
    }
}
