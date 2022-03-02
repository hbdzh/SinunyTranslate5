namespace SinunyTranslate.Common
{
    internal class TransCodeConvert
    {
        /// <summary>
        /// 百度翻译代码转换
        /// </summary>
        /// <param name="selectLanguage"></param>
        /// <returns></returns>
        internal static string BaiduLanguageConvert(string selectLanguage)
        {
            switch (selectLanguage)
            {
                default:
                    return "auto";
                case "中文（简体）":
                    return "zh";
                case "中文（繁体）":
                    return "cht";
                case "中文（粤语）":
                    return "yue";
                case "中文（文言文）":
                    return "wyw";
                case "苗语":
                    return "hmn";
                case "英语":
                    return "en";
                case "日语":
                    return "jp";
                case "韩语":
                    return "kor";
                case "法语":
                    return "fra";
                case "西班牙语":
                    return "spa";
                case "葡萄牙语":
                    return "pt";
                case "意大利语":
                    return "it";
                case "俄语":
                    return "ru";
                case "德语":
                    return "de";
                case "阿拉伯语":
                    return "ara";
                case "土耳其语":
                    return "tr";
                case "越南语":
                    return "vie";
                case "泰语":
                    return "th";
                case "马来语":
                    return "may";
                case "孟加拉语":
                    return "ben";
                case "印地语":
                    return "hi";
                case "旁遮普语":
                    return "pan";
                case "泰卢固语":
                    return "tel";
                case "马拉地语":
                    return "mar";
                case "泰米尔语":
                    return "tam";
                case "乌尔都语":
                    return "urd";
                case "荷兰语":
                    return "nl";
                case "希腊语":
                    return "el";
                case "波兰语":
                    return "pl";
                case "波斯语":
                    return "per";
                case "捷克语":
                    return "cs";
                case "丹麦语":
                    return "dan";
                case "芬兰语":
                    return "fin";
                case "瑞典语":
                    return "swe";
                case "匈牙利语":
                    return "hu";
                case "爱沙尼亚语":
                    return "est";
                case "罗马尼亚语":
                    return "rom";
                case "斯洛文尼亚语":
                    return "slo";
            }
        }
        /// <summary>
        /// 有道翻译代码转换
        /// </summary>
        /// <param name="selectLanguage"></param>
        /// <returns></returns>
        internal static string YoudaoLanguageConvert(string selectLanguage)
        {
            switch (selectLanguage)
            {
                default:
                    return "auto";
                case "中文（简体）":
                    return "zh-CHS";
                case "中文（粤语）":
                    return "yue";
                case "苗语":
                    return "mww";
                case "英语":
                    return "en";
                case "日语":
                    return "ja";
                case "韩语":
                    return "ko";
                case "法语":
                    return "fr";
                case "西班牙语":
                    return "es";
                case "葡萄牙语":
                    return "pt";
                case "意大利语":
                    return "it";
                case "俄语":
                    return "ru";
                case "德语":
                    return "de";
                case "阿拉伯语":
                    return "ar";
                case "土耳其语":
                    return "tr";
                case "越南语":
                    return "vi";
                case "泰语":
                    return "th";
                case "马来语":
                    return "ms";
                case "孟加拉语":
                    return "bn";
                case "蒙古语":
                    return "mn";
                case "印地语":
                    return "hi";
                case "旁遮普语":
                    return "pa";
                case "泰卢固语":
                    return "te";
                case "马拉地语":
                    return "mr";
                case "泰米尔语":
                    return "ta";
                case "乌尔都语":
                    return "ur";
                case "荷兰语":
                    return "nl";
                case "希腊语":
                    return "el";
                case "波兰语":
                    return "pl";
                case "波斯语":
                    return "fa";
                case "捷克语":
                    return "cs";
                case "丹麦语":
                    return "da";
                case "芬兰语":
                    return "fi";
                case "瑞典语":
                    return "sv";
                case "匈牙利语":
                    return "hu";
                case "爱沙尼亚语":
                    return "et";
                case "罗马尼亚语":
                    return "ro";
                case "斯洛文尼亚语":
                    return "sl";
            }
        }
        /// <summary>
        /// 有道翻译免费版代码转换
        /// </summary>
        /// <param name="selectLanguage"></param>
        /// <returns></returns>
        internal static string YoudaoFreeLanguageConvert(string selectLanguage)
        {
            switch(selectLanguage)
            {
                case "中文（简体）":
                    return "ZH_CN";
                case "英语":
                    return "EN";
                case "日语":
                    return "JA";
                case "韩语":
                    return "KR";
                case "法语":
                    return "FR";
                case "俄语":
                    return "RU";
                case "西班牙语":
                    return "SP";
                case "自动检测":
                    return "AUTO";
                default:
                    return "ERROR";
            }
        }
    }
}
