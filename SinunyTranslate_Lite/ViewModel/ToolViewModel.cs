using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SinunyTranslate_Lite.Model;
using System.Windows.Input;
using Windows.UI.Xaml;
using Microsoft.Toolkit.Uwp.Helpers;
using System.Globalization;
using System;
using SinunyTranslate_Lite.Utility.Auxiliary;

namespace SinunyTranslate_Lite.ViewModel
{
    internal class ToolViewModel : ObservableObject
    {
        private ToolModel toolBoxM;
        public ToolModel ToolBoxM
        {
            get { return toolBoxM; }
            set { SetProperty(ref toolBoxM, value); }
        }
        public ICommand QueryJinyiCommand { get; set; }
        public ICommand QueryFanyiCommand { get; set; }
        public ICommand QueryWordsCommand { get; set; }
        public ICommand QueryIdiomsCommand { get; set; }
        public ICommand ArgbToHexCommand { get; set; }
        public ICommand HexToArgbCommand { get; set; }
        public ICommand LengthUnitCommand { get; set; }
        public ICommand AreaUnitCommand { get; set; }
        public ICommand MassUnitCommand { get; set; }
        public ICommand TimeUnitCommand { get; set; }
        public ToolViewModel()
        {
            ToolBoxM = new ToolModel();
            QueryJinyiCommand = new RelayCommand(QueryJinyi);
            QueryFanyiCommand = new RelayCommand(QueryFanyi);
            QueryWordsCommand = new RelayCommand(QueryWords);
            QueryIdiomsCommand = new RelayCommand(QueryIdioms);
            ArgbToHexCommand = new RelayCommand(ArgbToHex);
            HexToArgbCommand = new RelayCommand(HexToArgb);
            LengthUnitCommand = new RelayCommand<object>(new Action<object>(LengthUnit));
            AreaUnitCommand = new RelayCommand<object>(new Action<object>(AreaUnit));
            MassUnitCommand = new RelayCommand<object>(new Action<object>(MassUnit));
            TimeUnitCommand = new RelayCommand<object>(new Action<object>(TimeUnit));
            ToolBoxM.ResultShow = Visibility.Collapsed;
        }
        /// <summary>
        /// 查询近义词
        /// </summary>
        public async void QueryJinyi()
        {
            if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
            {
                if (IsChinese(ToolBoxM.QueryContent))
                {
                    string jsonCode = await QueryApi.GetJson("http://api.jisuapi.com/jinyifanyi/word?appkey=57f22dc38f471349&word=", ToolBoxM.QueryContent);
                    ToolBoxM.QueryResult = QueryApi.JinyiQuery(jsonCode);
                    ToolBoxM.ResultShow = Visibility.Visible;
                }
            }
        }
        /// <summary>
        /// 查询反义词
        /// </summary>
        public async void QueryFanyi()
        {
            if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
            {
                if (IsChinese(ToolBoxM.QueryContent))
                {
                    string jsonCode = await QueryApi.GetJson("http://api.jisuapi.com/jinyifanyi/word?appkey=57f22dc38f471349&word=", ToolBoxM.QueryContent);
                    ToolBoxM.QueryResult = QueryApi.FanyiQuery(jsonCode);
                    ToolBoxM.ResultShow = Visibility.Visible;
                }
            }
        }
        /// <summary>
        /// 查询词语
        /// </summary>
        public async void QueryWords()
        {
            if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
            {
                if (IsChinese(ToolBoxM.QueryContent))
                {
                    string jsonCode = await QueryApi.GetJson("http://api.tianapi.com/txapi/lexicon/index?key=5a08864eba64801f1d2d2336db119e9b&word=", ToolBoxM.QueryContent);
                    ToolBoxM.QueryResult = QueryApi.WordsQuery(jsonCode);
                    ToolBoxM.ResultShow = Visibility.Visible;
                }
            }
        }
        /// <summary>
        /// 查询成语
        /// </summary>
        public async void QueryIdioms()
        {
            if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
            {
                if (IsChinese(ToolBoxM.QueryContent))
                {
                    string jsonCode = await QueryApi.GetJson("http://api.tianapi.com/chengyu/index?key=5a08864eba64801f1d2d2336db119e9b&word=", ToolBoxM.QueryContent);
                    ToolBoxM.QueryResult = QueryApi.IdiomsQuery(jsonCode);
                    ToolBoxM.ResultShow = Visibility.Visible;
                }
            }
        }
        /// <summary>
        /// ARGB转十六进制
        /// </summary>
        public void ArgbToHex()
        {
            if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
            {
                string[] getAry = ToolBoxM.QueryContent.Split(',');//单个字符分割
                if (getAry.Length > 4)//是ARGB
                {
                    Windows.UI.Color color = Windows.UI.Color.FromArgb(byte.Parse(getAry[0]), byte.Parse(getAry[1]), byte.Parse(getAry[2]), byte.Parse(getAry[3]));
                    ToolBoxM.QueryResult = ColorHelper.ToHex(color);
                    ToolBoxM.ResultShow = Visibility.Visible;
                }
            }
        }
        /// <summary>
        /// 十六进制转RGB
        /// </summary>
        public void HexToArgb()
        {
            if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
            {
                int argb = int.Parse(ToolBoxM.QueryContent.Replace("#", ""), NumberStyles.HexNumber);
                Windows.UI.Color color = Windows.UI.Color.FromArgb((byte)((argb & -16777216) >> 0x18),
                                      (byte)((argb & 0xff0000) >> 0x10),
                                      (byte)((argb & 0xff00) >> 8),
                                      (byte)(argb & 0xff));
                ToolBoxM.QueryResult = color.A + "," + color.R + "," + color.G + "," + color.B;
                ToolBoxM.ResultShow = Visibility.Visible;
            }
        }
        /// <summary>
        /// 长度单位换算
        /// </summary>
        public void LengthUnit(object unit)
        {
            if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
            {
                if (IsNumber(ToolBoxM.QueryContent))
                {
                    double length = double.Parse(ToolBoxM.QueryContent);
                    switch (unit)
                    {
                        case "毫米":
                            ToolBoxM.QueryResult = LengthConverter.FromMillimeter(length).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "厘米":
                            ToolBoxM.QueryResult = LengthConverter.FromCentimeter(length).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "分米":
                            ToolBoxM.QueryResult = LengthConverter.FromDecimeter(length).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "米":
                            ToolBoxM.QueryResult = LengthConverter.FromMeter(length).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "千米":
                            ToolBoxM.QueryResult = LengthConverter.FromKilometer(length).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "英寸":
                            ToolBoxM.QueryResult = LengthConverter.FromInch(length).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 面积单位换算
        /// </summary>
        /// <param name="unit"></param>
        public void AreaUnit(object unit)
        {
            if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
            {
                if (IsNumber(ToolBoxM.QueryContent))
                {
                    double area = double.Parse(ToolBoxM.QueryContent);
                    switch (unit)
                    {
                        case "平方毫米":
                            ToolBoxM.QueryResult = AreaConverter.FromSquareMillimeter(area).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "平方厘米":
                            ToolBoxM.QueryResult = AreaConverter.FromSquareCentimeter(area).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "平方分米":
                            ToolBoxM.QueryResult = AreaConverter.FromSquareDecimeter(area).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "平方米":
                            ToolBoxM.QueryResult = AreaConverter.FromSquareMeter(area).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "平方千米":
                            ToolBoxM.QueryResult = AreaConverter.FromSquareKilometer(area).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "亩":
                            ToolBoxM.QueryResult = AreaConverter.FromMu(area).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 质量单位换算
        /// </summary>
        /// <param name="unit"></param>
        public void MassUnit(object unit)
        {
            if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
            {
                if (IsNumber(ToolBoxM.QueryContent))
                {
                    double mass = double.Parse(ToolBoxM.QueryContent);
                    switch (unit)
                    {
                        case "吨":
                            ToolBoxM.QueryResult = MassConverter.FromTon(mass).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "千克":
                            ToolBoxM.QueryResult = MassConverter.FromKilogram(mass).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "克":
                            ToolBoxM.QueryResult = MassConverter.FromGram(mass).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "毫克":
                            ToolBoxM.QueryResult = MassConverter.FromMilligram(mass).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "担":
                            ToolBoxM.QueryResult = MassConverter.FromDan(mass).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "斤":
                            ToolBoxM.QueryResult = MassConverter.FromJin(mass).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "两":
                            ToolBoxM.QueryResult = MassConverter.FromLiang(mass).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "钱":
                            ToolBoxM.QueryResult = MassConverter.FromQian(mass).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 时间单位换算
        /// </summary>
        /// <param name="unit"></param>
        public void TimeUnit(object unit)
        {
            if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
            {
                if (IsNumber(ToolBoxM.QueryContent))
                {
                    double time = double.Parse(ToolBoxM.QueryContent);
                    switch (unit)
                    {
                        case "年":
                            ToolBoxM.QueryResult = TimeConverter.FromYear(time).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "月":
                            ToolBoxM.QueryResult = TimeConverter.FromMonth(time).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "周":
                            ToolBoxM.QueryResult = TimeConverter.FromWeek(time).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "天":
                            ToolBoxM.QueryResult = TimeConverter.FromDay(time).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "小时":
                            ToolBoxM.QueryResult = TimeConverter.FromHour(time).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "分钟":
                            ToolBoxM.QueryResult = TimeConverter.FromMinute(time).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "秒钟":
                            ToolBoxM.QueryResult = TimeConverter.FromSecond(time).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                        case "毫秒":
                            ToolBoxM.QueryResult = TimeConverter.FromMillisecond(time).ToString();
                            ToolBoxM.ResultShow = Visibility.Visible;
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 判断输入是否为数字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool IsNumber(string str)
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
        private bool IsChinese(string str)
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
