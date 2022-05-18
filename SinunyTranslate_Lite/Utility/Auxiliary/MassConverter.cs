using System.Text;

namespace SinunyTranslate_Lite.Utility.Auxiliary
{
    internal class MassConverter
    {
        /// <summary>
        /// 根据吨转换
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        internal static StringBuilder FromTon(double mass)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(mass + " 吨");
            sb.AppendLine(mass * 1000 + " 千克");
            sb.AppendLine(mass * 1000000 + " 克");
            sb.AppendLine(mass * 1000000000 + " 毫克");
            sb.AppendLine(mass * 20 + " 担");
            sb.AppendLine(mass * 2000 + " 斤");
            sb.AppendLine(mass * 20000 + " 两");
            sb.Append(mass * 200000 + " 钱");
            return sb;
        }
        /// <summary>
        /// 根据千克转换
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        internal static StringBuilder FromKilogram(double mass)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(mass * 0.001 + " 吨");
            sb.AppendLine(mass + " 千克");
            sb.AppendLine(mass * 1000 + " 克");
            sb.AppendLine(mass * 1000000 + " 毫克");
            sb.AppendLine(mass * 0.02 + " 担");
            sb.AppendLine(mass * 2 + " 斤");
            sb.AppendLine(mass * 20 + " 两");
            sb.Append(mass * 200 + " 钱");
            return sb;
        }
        /// <summary>
        /// 根据克转换
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        internal static StringBuilder FromGram(double mass)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(mass * 0.000001 + " 吨");
            sb.AppendLine(mass * 0.001 + " 千克");
            sb.AppendLine(mass + " 克");
            sb.AppendLine(mass * 1000 + " 毫克");
            sb.AppendLine(mass * 0.00002 + " 担");
            sb.AppendLine(mass * 0.002 + " 斤");
            sb.AppendLine(mass * 0.02 + " 两");
            sb.Append(mass * 0.2 + " 钱");
            return sb;
        }
        /// <summary>
        /// 根据毫克转换
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        internal static StringBuilder FromMilligram(double mass)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(mass * 0.000000001 + " 吨");
            sb.AppendLine(mass * 0.000001 + " 千克");
            sb.AppendLine(mass * 0.001 + " 克");
            sb.AppendLine(mass + " 毫克");
            sb.AppendLine(mass * 0.00000002 + " 担");
            sb.AppendLine(mass * 0.000002 + " 斤");
            sb.AppendLine(mass * 0.00002 + " 两");
            sb.Append(mass * 0.0002 + " 钱");
            return sb;
        }
        /// <summary>
        /// 根据担转换
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        internal static StringBuilder FromDan(double mass)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(mass * 0.05 + " 吨");
            sb.AppendLine(mass * 50 + " 千克");
            sb.AppendLine(mass * 50000 + " 克");
            sb.AppendLine(mass * 50000000 + " 毫克");
            sb.AppendLine(mass + " 担");
            sb.AppendLine(mass * 100 + " 斤");
            sb.AppendLine(mass * 1000 + " 两");
            sb.Append(mass * 10000 + " 钱");
            return sb;
        }
        /// <summary>
        /// 根据斤转换
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        internal static StringBuilder FromJin(double mass)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(mass * 0.0005 + " 吨");
            sb.AppendLine(mass * 0.5 + " 千克");
            sb.AppendLine(mass * 500 + " 克");
            sb.AppendLine(mass * 500000 + " 毫克");
            sb.AppendLine(mass * 0.01 + " 担");
            sb.AppendLine(mass + " 斤");
            sb.AppendLine(mass * 10 + " 两");
            sb.Append(mass * 100 + " 钱");
            return sb;
        }
        /// <summary>
        /// 根据两转换
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        internal static StringBuilder FromLiang(double mass)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(mass * 0.00005 + " 吨");
            sb.AppendLine(mass * 0.05 + " 千克");
            sb.AppendLine(mass * 50 + " 克");
            sb.AppendLine(mass * 50000 + " 毫克");
            sb.AppendLine(mass * 0.001 + " 担");
            sb.AppendLine(mass * 0.1 + " 斤");
            sb.AppendLine(mass + " 两");
            sb.Append(mass * 10 + " 钱");
            return sb;
        }
        /// <summary>
        /// 根据钱转换
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        internal static StringBuilder FromQian(double mass)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(mass * 0.000005 + " 吨");
            sb.AppendLine(mass * 0.005 + " 千克");
            sb.AppendLine(mass * 5 + " 克");
            sb.AppendLine(mass * 5000 + " 毫克");
            sb.AppendLine(mass * 0.0001 + " 担");
            sb.AppendLine(mass * 0.01 + " 斤");
            sb.AppendLine(mass * 0.1 + " 两");
            sb.Append(mass + " 钱");
            return sb;
        }
    }
}
