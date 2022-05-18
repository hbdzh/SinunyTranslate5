using System.Text;

namespace SinunyTranslate_Lite.Utility.Auxiliary
{
    internal class LengthConverter
    {
        /// <summary>
        /// 根据毫米转换
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        internal static StringBuilder FromMillimeter(double length)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(length + " 毫米");
            sb.AppendLine(length / 10 + " 厘米");
            sb.AppendLine(length / 100 + " 分米");
            sb.AppendLine(length / 1000 + " 米");
            sb.AppendLine(length / 1000000 + " 千米");
            sb.Append((length / 1000 * 39.37) + " 英寸");
            return sb;
        }
        /// <summary>
        /// 根据厘米转换
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        internal static StringBuilder FromCentimeter(double length)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(length * 10 + " 毫米");
            sb.AppendLine(length + " 厘米");
            sb.AppendLine(length / 10 + " 分米");
            sb.AppendLine(length / 100 + " 米");
            sb.AppendLine(length / 100000 + " 千米");
            sb.Append((length / 100 * 39.37) + " 英寸");
            return sb;
        }
        /// <summary>
        /// 根据分米转换
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        internal static StringBuilder FromDecimeter(double length)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(length * 100 + " 毫米");
            sb.AppendLine(length * 10 + " 厘米");
            sb.AppendLine(length + " 分米");
            sb.AppendLine(length / 10 + " 米");
            sb.AppendLine(length / 10000 + " 千米");
            sb.Append((length / 10 * 39.37) + " 英寸");
            return sb;
        }
        /// <summary>
        /// 根据米转换
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        internal static StringBuilder FromMeter(double length)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(length * 1000 + " 毫米");
            sb.AppendLine(length * 100 + " 厘米");
            sb.AppendLine(length * 10 + " 分米");
            sb.AppendLine(length + " 米");
            sb.AppendLine(length / 1000 + " 千米");
            sb.Append((length * 39.37) + " 英寸");
            return sb;
        }
        /// <summary>
        /// 根据千米转换
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        internal static StringBuilder FromKilometer(double length)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(length * 1000000 + " 毫米");
            sb.AppendLine(length * 100000 + " 厘米");
            sb.AppendLine(length * 10000 + " 分米");
            sb.AppendLine(length * 1000 + " 米");
            sb.AppendLine(length + " 千米");
            sb.Append((length * 1000 * 39.37) + " 英寸");
            return sb;
        }
        /// <summary>
        /// 根据英寸转换
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        internal static StringBuilder FromInch(double length)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(length * 25.4 + " 毫米");
            sb.AppendLine(length / 10 * 25.4 + " 厘米");
            sb.AppendLine(length / 100 * 25.4 + " 分米");
            sb.AppendLine(length / 1000 * 25.4 + " 米");
            sb.AppendLine(length / 1000000 * 25.4 + " 千米");
            sb.Append(length + " 英寸");
            return sb;
        }
    }
}
