using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SinunyTranslate.Model;
using System.Windows.Input;
using Windows.UI.Xaml;
using Microsoft.Toolkit.Uwp.Helpers;
using System.Globalization;
using System;
using SinunyTranslate.Utility.Auxiliary;
using Windows.UI.Xaml.Controls;
using SinunyTranslate.Common;

namespace SinunyTranslate.ViewModel
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
        public void QueryJinyi()
        {
            if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
            {
                ToolBoxM.QueryResult = QueryApi.JinyiQuery(ToolBoxM.QueryContent);
                ToolBoxM.ResultShow = Visibility.Visible;
            }
        }
        /// <summary>
        /// 查询反义词
        /// </summary>
        public void QueryFanyi()
        {
            if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
            {
                ToolBoxM.QueryResult = QueryApi.FanyiQuery(ToolBoxM.QueryContent);
                ToolBoxM.ResultShow = Visibility.Visible;
            }
        }
        /// <summary>
        /// 查询词语
        /// </summary>
        public void QueryWords()
        {
            if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
            {
                ToolBoxM.QueryResult = QueryApi.WordsQuery(ToolBoxM.QueryContent);
                ToolBoxM.ResultShow = Visibility.Visible;
            }
        }
        /// <summary>
        /// 查询成语
        /// </summary>
        public void QueryIdioms()
        {
            if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
            {
                ToolBoxM.QueryResult = QueryApi.IdiomsQuery(ToolBoxM.QueryContent);
                ToolBoxM.ResultShow = Visibility.Visible;
            }
        }
        /// <summary>
        /// ARGB转十六进制
        /// </summary>
        public async void ArgbToHex()
        {
            try
            {
                if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
                {
                    string[] getAry = ToolBoxM.QueryContent.Replace('，', ',').Split(',');//单个字符分割
                    if (getAry.Length == 4)//是ARGB
                    {
                        Windows.UI.Color color = Windows.UI.Color.FromArgb(byte.Parse(getAry[0]), byte.Parse(getAry[1]), byte.Parse(getAry[2]), byte.Parse(getAry[3]));
                        ToolBoxM.QueryResult = ColorHelper.ToHex(color);
                    }
                    else if (getAry.Length == 3)//是RGB
                    {
                        Windows.UI.Color color = Windows.UI.Color.FromArgb(255, byte.Parse(getAry[0]), byte.Parse(getAry[1]), byte.Parse(getAry[2]));
                        ToolBoxM.QueryResult = ColorHelper.ToHex(color).Remove(1, 2);
                    }
                    ToolBoxM.ResultShow = Visibility.Visible;
                }
            }
            catch
            {
                ContentDialog contentDialog = new ContentDialog
                {
                    Title = "注意",
                    Content = "可能存在无效值",
                    IsSecondaryButtonEnabled = false,
                    PrimaryButtonText = "确定"
                };
                await contentDialog.ShowAsync();
            }
        }
        /// <summary>
        /// 十六进制转ARGB
        /// </summary>
        public async void HexToArgb()
        {
            try
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
            catch
            {
                ContentDialog contentDialog = new ContentDialog
                {
                    Title = "注意",
                    Content = "可能存在无效值",
                    IsSecondaryButtonEnabled = false,
                    PrimaryButtonText = "确定"
                };
                await contentDialog.ShowAsync();
            }
        }
        /// <summary>
        /// 长度单位换算
        /// </summary>
        public void LengthUnit(object unit)
        {
            if (!string.IsNullOrEmpty(ToolBoxM.QueryContent))
            {
                if (JudgeText.IsNumber(ToolBoxM.QueryContent))
                {
                    double length = double.Parse(ToolBoxM.QueryContent);
                    switch (unit)
                    {
                        case "毫米":
                            ToolBoxM.QueryResult = LengthConverter.FromMillimeter(length).ToString();
                            break;
                        case "厘米":
                            ToolBoxM.QueryResult = LengthConverter.FromCentimeter(length).ToString();
                            break;
                        case "分米":
                            ToolBoxM.QueryResult = LengthConverter.FromDecimeter(length).ToString();
                            break;
                        case "米":
                            ToolBoxM.QueryResult = LengthConverter.FromMeter(length).ToString();
                            break;
                        case "千米":
                            ToolBoxM.QueryResult = LengthConverter.FromKilometer(length).ToString();
                            break;
                        case "英寸":
                            ToolBoxM.QueryResult = LengthConverter.FromInch(length).ToString();
                            break;
                    }
                    ToolBoxM.ResultShow = Visibility.Visible;
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
                if (JudgeText.IsNumber(ToolBoxM.QueryContent))
                {
                    double area = double.Parse(ToolBoxM.QueryContent);
                    switch (unit)
                    {
                        case "平方毫米":
                            ToolBoxM.QueryResult = AreaConverter.FromSquareMillimeter(area).ToString();
                            break;
                        case "平方厘米":
                            ToolBoxM.QueryResult = AreaConverter.FromSquareCentimeter(area).ToString();
                            break;
                        case "平方分米":
                            ToolBoxM.QueryResult = AreaConverter.FromSquareDecimeter(area).ToString();
                            break;
                        case "平方米":
                            ToolBoxM.QueryResult = AreaConverter.FromSquareMeter(area).ToString();
                            break;
                        case "平方千米":
                            ToolBoxM.QueryResult = AreaConverter.FromSquareKilometer(area).ToString();
                            break;
                        case "亩":
                            ToolBoxM.QueryResult = AreaConverter.FromMu(area).ToString();
                            break;
                    }
                    ToolBoxM.ResultShow = Visibility.Visible;
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
                if (JudgeText.IsNumber(ToolBoxM.QueryContent))
                {
                    double mass = double.Parse(ToolBoxM.QueryContent);
                    switch (unit)
                    {
                        case "吨":
                            ToolBoxM.QueryResult = MassConverter.FromTon(mass).ToString();
                            break;
                        case "千克":
                            ToolBoxM.QueryResult = MassConverter.FromKilogram(mass).ToString();
                            break;
                        case "克":
                            ToolBoxM.QueryResult = MassConverter.FromGram(mass).ToString();
                            break;
                        case "毫克":
                            ToolBoxM.QueryResult = MassConverter.FromMilligram(mass).ToString();
                            break;
                        case "担":
                            ToolBoxM.QueryResult = MassConverter.FromDan(mass).ToString();
                            break;
                        case "斤":
                            ToolBoxM.QueryResult = MassConverter.FromJin(mass).ToString();
                            break;
                        case "两":
                            ToolBoxM.QueryResult = MassConverter.FromLiang(mass).ToString();
                            break;
                        case "钱":
                            ToolBoxM.QueryResult = MassConverter.FromQian(mass).ToString();
                            break;
                    }
                    ToolBoxM.ResultShow = Visibility.Visible;
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
                if (JudgeText.IsNumber(ToolBoxM.QueryContent))
                {
                    double time = double.Parse(ToolBoxM.QueryContent);
                    switch (unit)
                    {
                        case "年":
                            ToolBoxM.QueryResult = TimeConverter.FromYear(time).ToString();
                            break;
                        case "月":
                            ToolBoxM.QueryResult = TimeConverter.FromMonth(time).ToString();
                            break;
                        case "周":
                            ToolBoxM.QueryResult = TimeConverter.FromWeek(time).ToString();
                            break;
                        case "天":
                            ToolBoxM.QueryResult = TimeConverter.FromDay(time).ToString();
                            break;
                        case "小时":
                            ToolBoxM.QueryResult = TimeConverter.FromHour(time).ToString();
                            break;
                        case "分钟":
                            ToolBoxM.QueryResult = TimeConverter.FromMinute(time).ToString();
                            break;
                        case "秒钟":
                            ToolBoxM.QueryResult = TimeConverter.FromSecond(time).ToString();
                            break;
                        case "毫秒":
                            ToolBoxM.QueryResult = TimeConverter.FromMillisecond(time).ToString();
                            break;
                    }
                    ToolBoxM.ResultShow = Visibility.Visible;
                }
            }
        }
    }
}
