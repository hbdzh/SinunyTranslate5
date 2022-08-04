using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using Windows.Storage;

namespace SinunyTranslate_Lite.Common
{
    internal class AppConfig
    {
        /// <summary>
        /// 支持的翻译引擎
        /// </summary>
        internal static List<string> AllTranslateEngine { get; } = new List<string>() { "有道翻译（免费版）", "有道翻译", "百度翻译", "必应词典" };
        /// <summary>
        /// 默认使用的翻译引擎
        /// </summary>
        public static string UseTranslateEngine
        {
            get
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                if (localSettings.Values.ContainsKey("DefaultEngine"))
                {
                    return (string)localSettings.Values["DefaultEngine"];
                }
                else
                {
                    return "百度翻译";
                }
            }
        }
        /// <summary>
        /// 使用的延时翻译时长
        /// </summary>
        public static int UseDelayTime
        {
            get
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                if (localSettings.Values.ContainsKey("DelayTime"))
                {
                    double delay = (double)ApplicationData.Current.LocalSettings.Values["DelayTime"] * 1000;
                    return (int)delay;
                }
                else
                {
                    return 500;
                }
            }
        }
        /// <summary>
        /// 导航栏模式
        /// </summary>
        public static NavigationViewPaneDisplayMode UseNavMode
        {
            get
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                if (localSettings.Values.ContainsKey("NavMode"))
                {
                    NavigationViewPaneDisplayMode mode;
                    if ((string)ApplicationData.Current.LocalSettings.Values["NavMode"] == "Top")
                    {
                        mode = NavigationViewPaneDisplayMode.Top;
                    }
                    else if ((string)ApplicationData.Current.LocalSettings.Values["NavMode"] == "LeftCompact")
                    {
                        mode = NavigationViewPaneDisplayMode.LeftCompact;
                    }
                    else if ((string)ApplicationData.Current.LocalSettings.Values["NavMode"] == "LeftMinimal")
                    {
                        mode = NavigationViewPaneDisplayMode.LeftMinimal;
                    }
                    else
                    {
                        mode = NavigationViewPaneDisplayMode.Left;
                    }
                    return mode;
                }
                else
                {
                    return NavigationViewPaneDisplayMode.Left;
                }
            }

        }
        /// <summary>
        /// 支持的Ocr引擎
        /// </summary>
        internal static List<string> AllOcrEngine { get; } = new List<string>() { "WindowsOcr" };
        /// <summary>
        /// 翻译功能支持的语言
        /// </summary>
        internal static List<string> AllTranslateLanguage { get; } = new List<string>() { "自动检测", "中文（简体）", "中文（繁体）", "中文（粤语）", "中文（文言文）", "苗语", "英语", "日语", "韩语", "法语", "西班牙语", "葡萄牙语", "意大利语", "俄语", "德语", "阿拉伯语", "土耳其语", "越南语", "泰语", "马来语", "孟加拉语", "蒙古语", "印地语", "旁遮普语", "泰卢固语", "马拉地语", "泰米尔语", "乌尔都语", "荷兰语", "希腊语", "波兰语", "波斯语", "捷克语", "丹麦语", "芬兰语", "瑞典语", "匈牙利语", "爱沙尼亚语", "罗马尼亚语", "斯洛文尼亚语" };
        /// <summary>
        /// TesseractOcr支持的语言
        /// </summary>
        internal static Dictionary<string, string> AllOcrLanguage { get; set; } = new Dictionary<string, string>() { { "简体中文", "chi_sim" } };
        /// <summary>
        /// WindowsOcr支持的语言
        /// </summary>
        internal static Dictionary<string, string> WindowsOcrLanguage { get; } = new Dictionary<string, string>() { { "简体中文", "zh-CN" }, { "繁体中文", "zh-TW" }, { "英语", "en-US" }, { "日语", "ja-JP" }, { "韩语", "ko-KR" } };
    }
}
