using System.Text;

namespace SinunyTranslate.Utility.Auxiliary
{
    internal class AreaConverter
    {
        /// <summary>
        /// 根据平方毫米转换
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        internal static StringBuilder FromSquareMillimeter(double area)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(area + " 平方毫米");
            sb.AppendLine(area / 100 + " 平方厘米");
            sb.AppendLine(area / 10000 + " 平方分米");
            sb.AppendLine(area / 1000000 + " 平方米");
            sb.AppendLine(area / 1000000000000 + " 平方千米");
            sb.Append((area / 1000000000000 * 1500) + " 亩");
            return sb;
        }
        /// <summary>
        /// 根据平方厘米转换
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        internal static StringBuilder FromSquareCentimeter(double area)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(area * 100 + " 平方毫米");
            sb.AppendLine(area + " 平方厘米");
            sb.AppendLine(area / 100 + " 平方分米");
            sb.AppendLine(area / 10000 + " 平方米");
            sb.AppendLine(area / 10000000000 + " 平方千米");
            sb.Append((area / 10000000000 * 1500) + " 亩");
            return sb;
        }
        /// <summary>
        /// 根据平方分米转换
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        internal static StringBuilder FromSquareDecimeter(double area)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(area * 10000 + " 平方毫米");
            sb.AppendLine(area * 100 + " 平方厘米");
            sb.AppendLine(area + " 平方分米");
            sb.AppendLine(area / 100 + " 平方米");
            sb.AppendLine(area / 100000000 + " 平方千米");
            sb.Append((area / 100000000 * 1500) + " 亩");
            return sb;
        }
        /// <summary>
        /// 根据平方米转换
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        internal static StringBuilder FromSquareMeter(double area)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(area * 1000000 + " 平方毫米");
            sb.AppendLine(area * 10000 + " 平方厘米");
            sb.AppendLine(area * 100 + " 平方分米");
            sb.AppendLine(area + " 平方米");
            sb.AppendLine(area / 1000000 + " 平方千米");
            sb.Append((area / 1000000 * 1500) + " 亩");
            return sb;
        }
        /// <summary>
        /// 根据平方千米转换
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        internal static StringBuilder FromSquareKilometer(double area)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(area * 1000000000000 + " 平方毫米");
            sb.AppendLine(area * 10000000000 + " 平方厘米");
            sb.AppendLine(area * 100000000 + " 平方分米");
            sb.AppendLine(area * 1000000 + " 平方米");
            sb.AppendLine(area + " 平方千米");
            sb.Append((area * 1500) + " 亩");
            return sb;
        }
        /// <summary>
        /// 根据亩转换
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        internal static StringBuilder FromMu(double area)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(area * 666666666.66 + " 平方毫米");
            sb.AppendLine(area * 6666666.66 + " 平方厘米");
            sb.AppendLine(area * 66666.66 + " 平方分米");
            sb.AppendLine(area * 666.66 + " 平方米");
            sb.AppendLine(area * 0.00066 + " 平方千米");
            sb.Append(area + " 亩");
            return sb;
        }
    }
}
