using HtmlAgilityPack;
using SinunyTranslate.Common;
using System;
using System.Text;

namespace SinunyTranslate.Utility.Auxiliary
{
    internal class QueryApi
    {
        /// <summary>
        /// 近义词查询
        /// </summary>
        /// <param name="queryContent"></param>
        /// <returns></returns>
        internal static string JinyiQuery(string queryContent)
        {
            if (JudgeText.IsChinese(queryContent))
            {
                string url = "https://www.fantiz5.com/jinyici/cha.asp?font=" + queryContent;
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@class='zuciall']/ul/li");
                if (node != null)
                {
                    return node.InnerText.Replace("、", "；" + Environment.NewLine) + Environment.NewLine + Environment.NewLine + "查询结果来自 www.fantiz5.com";
                }
                else
                {
                    return "没有查到近义词";
                }
            }
            else
            {
                return "检测到非中文字符";
            }
        }
        /// <summary>
        /// 反义词查询
        /// </summary>
        /// <param name="queryContent"></param>
        /// <returns></returns>
        internal static string FanyiQuery(string queryContent)
        {
            if (JudgeText.IsChinese(queryContent))
            {
                string url = "https://m.cilin.org/fyc/w_" + queryContent + ".html";
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                var nodes = doc.DocumentNode.SelectNodes("//div[@class='markdown-body']/p[@class='aboutwords']/a[@class='linktowords']");
                if (nodes != null)
                {
                    StringBuilder result = new StringBuilder();
                    foreach (var item in nodes)
                    {
                        result.AppendLine(item.InnerText + "；");
                    }
                    return result.ToString() + Environment.NewLine + "查询结果来自 m.cilin.org";
                }
                else
                {
                    return "没有查到反义词";
                }
            }
            else
            {
                return "检测到非中文字符";
            }
        }
        /// <summary>
        /// 成语查询
        /// </summary>
        /// <param name="queryContent">要查询的成语</param>
        /// <returns>返回查询的内容</returns>
        internal static string IdiomsQuery(string queryContent)
        {
            if (JudgeText.IsChinese(queryContent))
            {
                string url = "https://www.cyjl123.com/p/" + queryContent;
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='q-card__section q-card__section--vert']/div[@class='text-body2']");
                StringBuilder result = new StringBuilder();
                if (nodes != null)
                {
                    foreach (HtmlNode item in nodes)
                    {
                        result.AppendLine(item.InnerText);
                    }
                    return result.ToString() + Environment.NewLine + Environment.NewLine + "查询结果来自 www.cyjl123.com";
                }
                else
                {
                    return "查询不到该成语";
                }
            }
            else
            {
                return "检测到非中文字符";
            }
        }
        /// <summary>
        /// 词语查询
        /// </summary>
        /// <param name="queryContent">要查询的词语</param>
        /// <returns>返回字符串</returns>
        internal static string WordsQuery(string queryContent)
        {
            if (JudgeText.IsChinese(queryContent))
            {
                string url = "https://dict.baidu.com/s?wd=" + queryContent;
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                var node = doc.DocumentNode.SelectSingleNode("//div[@class='content means imeans']/div[@class='tab-content']");
                if (node != null)
                {
                    return node.InnerText.Replace(" ", "").Replace("\n\n\n", "\n").Replace("\n\n", "\n") + Environment.NewLine + "查询结果来自 dict.baidu.com";
                }
                else
                {
                    return "没有查到相关词语";
                }
            }
            else
            {
                return "检测到非中文字符";
            }
        }
    }
}
