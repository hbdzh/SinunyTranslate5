using System.Text;

namespace SinunyTranslate.Utility.Auxiliary
{
    internal class TimeConverter
    {
        /// <summary>
        /// 根据年转换
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        internal static StringBuilder FromYear(double time)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(time + " 年");
            sb.AppendLine(time * 12 + " 月");
            sb.AppendLine(time * 52 + " 周");
            sb.AppendLine(time * 365 + " 日");
            sb.AppendLine(time * 8760 + " 时");
            sb.AppendLine(time * 525600 + " 分");
            sb.AppendLine(time * 31536000 + " 秒");
            sb.Append(time * 31536000000 + " 毫秒");
            return sb;
        }
        /// <summary>
        /// 根据月转换
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        internal static StringBuilder FromMonth(double time)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(time * 0.0832876 + " 年");
            sb.AppendLine(time + " 月");
            sb.AppendLine(time * 4.34 + " 周");
            sb.AppendLine(time * 30 + " 日");
            sb.AppendLine(time * 729.6 + " 时");
            sb.AppendLine(time * 43776 + " 分");
            sb.AppendLine(time * 2626560 + " 秒");
            sb.Append(time * 2626560000 + " 毫秒");
            return sb;
        }
        /// <summary>
        /// 根据周转换
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        internal static StringBuilder FromWeek(double time)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(time * 0.0191781 + " 年");
            sb.AppendLine(time * 0.2302631 + " 月");
            sb.AppendLine(time + " 周");
            sb.AppendLine(time * 7 + " 日");
            sb.AppendLine(time * 168 + " 时");
            sb.AppendLine(time * 10080 + " 分");
            sb.AppendLine(time * 604800 + " 秒");
            sb.Append(time * 604800000 + " 毫秒");
            return sb;
        }
        /// <summary>
        /// 根据天转换
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        internal static StringBuilder FromDay(double time)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(time * 0.0027397 + " 年");
            sb.AppendLine(time * 0.0328947 + " 月");
            sb.AppendLine(time * 0.1428571 + " 周");
            sb.AppendLine(time + " 日");
            sb.AppendLine(time * 24 + " 时");
            sb.AppendLine(time * 1440 + " 分");
            sb.AppendLine(time * 86400 + " 秒");
            sb.Append(time * 86400000 + " 毫秒");
            return sb;
        }
        /// <summary>
        /// 根据小时转换
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        internal static StringBuilder FromHour(double time)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(time * 0.0001142 + " 年");
            sb.AppendLine(time * 0.0013706 + " 月");
            sb.AppendLine(time * 0.0059524 + " 周");
            sb.AppendLine(time * 0.0416667 + " 日");
            sb.AppendLine(time + " 时");
            sb.AppendLine(time * 60 + " 分");
            sb.AppendLine(time * 3600 + " 秒");
            sb.Append(time * 3600000 + " 毫秒");
            return sb;
        }
        /// <summary>
        /// 根据分钟转换
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        internal static StringBuilder FromMinute(double time)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(time * 0.0000019025875190 + " 年");
            sb.AppendLine(time * 0.0000228435672515 + " 月");
            sb.AppendLine(time * 0.0000992 + " 周");
            sb.AppendLine(time * 0.0006944 + " 日");
            sb.AppendLine(time * 0.0166667 + " 时");
            sb.AppendLine(time + " 分");
            sb.AppendLine(time * 60 + " 秒");
            sb.Append(time * 60000 + " 毫秒");
            return sb;
        }
        /// <summary>
        /// 根据秒钟转换
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        internal static StringBuilder FromSecond(double time)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(time * 0.0000000317097919838 + " 年");
            sb.AppendLine(time * 0.000000380726120858 + " 月");
            sb.AppendLine(time * 0.00000165343915344 + " 周");
            sb.AppendLine(time * 0.0000116 + " 日");
            sb.AppendLine(time * 0.0002778 + " 时");
            sb.AppendLine(time * 0.0166667 + " 分");
            sb.AppendLine(time + " 秒");
            sb.Append(time * 1000 + " 毫秒");
            return sb;
        }
        /// <summary>
        /// 根据毫秒转换
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        internal static StringBuilder FromMillisecond(double time)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(time * 0.0000000000317097919838 + " 年");
            sb.AppendLine(time * 0.000000000380726120858 + " 月");
            sb.AppendLine(time * 0.00000000165343915344 + " 周");
            sb.AppendLine(time * 0.0000000115740740741 + " 日");
            sb.AppendLine(time * 0.000000277777777778 + " 时");
            sb.AppendLine(time * 0.0000166666666667 + " 分");
            sb.AppendLine(time * 0.001 + " 秒");
            sb.Append(time + " 毫秒");
            return sb;
        }
    }
}
