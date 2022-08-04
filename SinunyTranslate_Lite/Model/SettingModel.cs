using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace SinunyTranslate_Lite.Model
{
    internal class SettingModel : ObservableObject
    {
        private List<string> themeStyle;
        public List<string> ThemeStyle
        {
            get => themeStyle;
            set => SetProperty(ref themeStyle, value);
        }
        private string useThemeStyle;
        public string UseThemeStyle
        {
            get => useThemeStyle;
            set => SetProperty(ref useThemeStyle, value);
        }
        private List<string> translateList;
        public List<string> TranslateList
        {
            get => translateList;
            set => SetProperty(ref translateList, value);
        }
        private string useTranslateEngine;
        public string UseTranslateEngine
        {
            get => useTranslateEngine;
            set => SetProperty(ref useTranslateEngine, value);
        }
        private string baiduAppID;
        public string BaiduAppID
        {
            get => baiduAppID;
            set => SetProperty(ref baiduAppID, value);
        }
        private string baiduAppSecret;
        public string BaiduAppSecret
        {
            get => baiduAppSecret;
            set => SetProperty(ref baiduAppSecret, value);
        }
        private string youdaoAppID;
        public string YoudaoAppID
        {
            get => youdaoAppID;
            set => SetProperty(ref youdaoAppID, value);
        }
        private string youdaoAppSecret;
        public string YoudaoAppSecret
        {
            get => youdaoAppSecret;
            set => SetProperty(ref youdaoAppSecret, value);
        }
        private List<double> delayTimeList;
        public List<double> DelayTimeList
        {
            get => delayTimeList;
            set => SetProperty(ref delayTimeList, value);
        }
        private double delayTime;
        public double DelayTime
        {
            get => delayTime;
            set => SetProperty(ref delayTime, value);
        }
        private List<NavigationViewPaneDisplayMode> navModeList;
        public List<NavigationViewPaneDisplayMode> NavModeList
        {
            get => navModeList;
            set => SetProperty(ref navModeList, value);
        }
        private NavigationViewPaneDisplayMode navMode;
        public NavigationViewPaneDisplayMode NavMode
        {
            get => navMode;
            set => SetProperty(ref navMode, value);
        }
    }
}
