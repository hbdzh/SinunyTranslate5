namespace SinunyTranslate.Common
{
    internal class JudgeText
    {
        /// <summary>
        /// 判断输入是否为数字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static bool IsNumber(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 用Unicode编码范围判断是不是汉字
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>true是汉字，false不是汉字</returns>
        internal static bool IsChinese(string str)
        {
            bool b = false;
            foreach (char c in str)
            {
                if (c >= 0x4e00 && c <= 0x9fbb)
                {
                    b = true;
                    break;
                }
            }
            return b;
        }
    }
}
