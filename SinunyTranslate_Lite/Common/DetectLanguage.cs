namespace SinunyTranslate_Lite.Common
{
    internal class DetectLanguage
    {
        /// <summary>
        /// 根据内容获取语言名称
        /// </summary>
        /// <param name="content">输入的内容</param>
        /// <returns>语言名称</returns>
        internal static string GetLanguageType(string content)
        {
            string languageType = string.Empty;
            if (IsRussian(content) == true)
            {
                languageType = "俄语";
            }
            if (IsSpanish(content) == true)
            {
                languageType = "西班牙语";
            }
            if (IsFrench(content) == true)
            {
                languageType = "法语";
            }
            if (IsKorean(content) == true)
            {
                languageType = "韩语";
            }
            if (IsJapanese(content) == true)
            {
                languageType = "日语";
            }
            if (IsEnglish(content) == true)
            {
                languageType = "英语";
            }
            if (IsChinese(content) == true)
            {
                languageType = "中文（简体）";
            }
            return languageType;
        }
        /// <summary>
        /// 用 UNICODE 编码范围判断字符是不是汉字
        /// </summary>
        /// <param name="content">待判断字符或字符串</param>
        /// <returns>真：是汉字；假：不是</returns>
        private static bool IsChinese(string content)
        {
            bool result = false;
            foreach (char t in content)
            {
                if (t >= 0x4e00 && t <= 0x9fbb)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 用 UNICODE 编码范围判断字符是不是日文
        /// </summary>
        /// <param name="content">待判断字符或字符串</param>
        /// <returns>真：是日文；假：不是</returns>
        private static bool IsJapanese(string content)
        {
            bool result = false;
            foreach (char t in content)
            {
                if (t >= 0x0800 && t <= 0x4e00)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 用 UNICODE 编码范围判断字符是不是韩文
        /// </summary>
        /// <param name="content">待判断字符或字符串</param>
        /// <returns>真：是韩文；假：不是</returns>
        private static bool IsKorean(string content)
        {
            bool result = false;
            foreach (char t in content)
            {
                if (t >= 0xac00 && t <= 0xd7ff)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 用 UNICODE 编码范围判断字符是不是英文
        /// </summary>
        /// <param name="content">待判断字符或字符串</param>
        /// <returns>真：是英文；假：不是</returns>
        private static bool IsEnglish(string content)
        {
            bool result = false;
            foreach (char t in content)
            {
                if (t >= 0x0041 && t <= 0x005A)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 用 UNICODE 编码范围判断字符是不是俄文
        /// </summary>
        /// <param name="content">待判断字符或字符串</param>
        /// <returns>真：是俄文；假：不是</returns>
        private static bool IsRussian(string content)
        {
            bool result = false;
            foreach (char t in content)
            {
                if (t >= 0x0400 && t <= 0x04ff)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 用 UNICODE 编码范围判断字符是不是法文
        /// </summary>
        /// <param name="content">待判断字符或字符串</param>
        /// <returns>真：是法文；假：不是</returns>
        private static bool IsFrench(string content)
        {
            bool result = false;
            foreach (char t in content)
            {
                if (t >= 0x00C0 && t <= 0x00FF)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 判断是不是西班牙文
        /// </summary>
        /// <param name="content">待判断字符或字符串</param>
        /// <returns>真：是西班牙文；假：不是</returns>
        private static bool IsSpanish(string content)
        {
            if (content == null || content.Length != 9 || char.IsLetter(content[8]) == false)
            {
                return false;
            }
            for (int i = 0; i < 8; i++)
            {
                if (char.IsDigit(content[i]) == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
