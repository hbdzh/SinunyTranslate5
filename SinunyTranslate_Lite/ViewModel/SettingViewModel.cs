using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using SinunyTranslate_Lite.Common;
using SinunyTranslate_Lite.Model;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.Services.Maps;
using Windows.Storage;

namespace SinunyTranslate_Lite.ViewModel
{
    internal class SettingViewModel : ObservableObject
    {
        public ICommand TranslateApiTextChangedCommand { get; set; }
        public ICommand DefaultTranslateEngineSelectionChangedCommand { get; set; }
        public ICommand ThemeStyleSelectionChangedCommand { get; set; }
        public ICommand DelayTimeSelectionChangedCommand { get; set; }
        public ICommand NavModeSelectionChangedCommand { get; set; }
        private SettingModel settingM;
        public SettingModel SettingM
        {
            get { return settingM; }
            set { SetProperty(ref settingM, value); }
        }
        ApplicationDataContainer localSettings;
        public SettingViewModel()
        {
            SettingM = new SettingModel
            {
                TranslateList = new List<string>(),
                ThemeStyle = new List<string>(),
                DelayTimeList = new List<double>(),
                NavModeList = new List<NavigationViewPaneDisplayMode>()
            };
            TranslateApiTextChangedCommand = new RelayCommand(TranslateApiTextChanged);
            DefaultTranslateEngineSelectionChangedCommand = new RelayCommand(DefaultTranslateEngineSelectionChanged);
            ThemeStyleSelectionChangedCommand = new RelayCommand(ThemeStyleSelectionChanged);
            DelayTimeSelectionChangedCommand = new RelayCommand(DelayTimeSelectionChanged);
            NavModeSelectionChangedCommand = new RelayCommand(NavModeSelectionChanged);
            localSettings = ApplicationData.Current.LocalSettings;
            InitThemeStyle();
            InitTranslateEngine();
            InitSetting();
            InitDelayTime();
            InitNavMode();
        }
        private void InitTranslateEngine()
        {
            SettingM.TranslateList.Add("百度翻译");
            SettingM.TranslateList.Add("有道翻译");
            SettingM.TranslateList.Add("有道翻译（免费版）");
            SettingM.TranslateList.Add("必应词典");
            if (localSettings.Values.ContainsKey("DefaultEngine"))
            {
                SettingM.UseTranslateEngine = (string)localSettings.Values["DefaultEngine"];
            }
            else
            {
                SettingM.UseTranslateEngine = "百度翻译";
            }
        }
        private void InitDelayTime()
        {
            SettingM.DelayTimeList.Add(0);
            SettingM.DelayTimeList.Add(0.5);
            SettingM.DelayTimeList.Add(1);
            SettingM.DelayTimeList.Add(1.5);
            SettingM.DelayTimeList.Add(2);
            SettingM.DelayTimeList.Add(2.5);
            SettingM.DelayTimeList.Add(3);
            if (localSettings.Values.ContainsKey("DelayTime"))
            {
                SettingM.DelayTime = (double)localSettings.Values["DelayTime"];
            }
        }
        private void InitNavMode()
        {
            SettingM.NavModeList.Add(NavigationViewPaneDisplayMode.Top);
            SettingM.NavModeList.Add(NavigationViewPaneDisplayMode.Left);
            SettingM.NavModeList.Add(NavigationViewPaneDisplayMode.LeftCompact);
            SettingM.NavModeList.Add(NavigationViewPaneDisplayMode.LeftMinimal);
            if (localSettings.Values.ContainsKey("NavMode"))
            {
                SettingM.NavMode = AppConfig.UseNavMode;
            }
        }
        private void InitSetting()
        {
            if (localSettings.Values.ContainsKey("YoudaoAppID"))
            {
                SettingM.YoudaoAppID = (string)localSettings.Values["YoudaoAppID"];
            }
            if (localSettings.Values.ContainsKey("YoudaoAppSecret"))
            {
                SettingM.YoudaoAppSecret = (string)localSettings.Values["YoudaoAppSecret"];
            }
            if (localSettings.Values.ContainsKey("BaiduAppID"))
            {
                SettingM.BaiduAppID = (string)localSettings.Values["BaiduAppID"];
            }
            if (localSettings.Values.ContainsKey("BaiduAppSecret"))
            {
                SettingM.BaiduAppSecret = (string)localSettings.Values["BaiduAppSecret"];
            }
        }
        private void InitThemeStyle()
        {
            SettingM.ThemeStyle.Add("Mica");
            SettingM.ThemeStyle.Add("Acrylic");
            if (localSettings.Values.ContainsKey("ThemeStyle"))
            {
                SettingM.UseThemeStyle = (string)localSettings.Values["ThemeStyle"];
            }
            else
            {
                SettingM.UseThemeStyle = "Mica";
            }
        }
        private void NavModeSelectionChanged()
        {
            if (SettingM.NavMode == NavigationViewPaneDisplayMode.Top)
            {
                localSettings.Values["NavMode"] = "Top";
            }
            else if (SettingM.NavMode == NavigationViewPaneDisplayMode.LeftCompact)
            {
                localSettings.Values["NavMode"] = "LeftCompact";
            }
            else if (SettingM.NavMode == NavigationViewPaneDisplayMode.LeftMinimal)
            {
                localSettings.Values["NavMode"] = "LeftMinimal";
            }
            else
            {
                localSettings.Values["NavMode"] = "Left";
            }
        }
        private void DefaultTranslateEngineSelectionChanged()
        {
            if (SettingM.UseTranslateEngine != null)
            {
                localSettings.Values["DefaultEngine"] = SettingM.UseTranslateEngine;
            }
        }
        private void ThemeStyleSelectionChanged()
        {
            if (SettingM.UseThemeStyle != null)
            {
                localSettings.Values["ThemeStyle"] = SettingM.UseThemeStyle;
            }
        }
        private void DelayTimeSelectionChanged()
        {
            if (SettingM.DelayTime > 0 || SettingM.DelayTime <= 3)
            {
                localSettings.Values["DelayTime"] = SettingM.DelayTime;
            }
        }
        private void TranslateApiTextChanged()
        {
            if (SettingM.YoudaoAppID != null)
            {
                ApiSign.YoudaoAppID = SettingM.YoudaoAppID;
            }
            if (SettingM.YoudaoAppSecret != null)
            {
                ApiSign.YoudaoAppSecret = SettingM.YoudaoAppSecret;
            }
            if (SettingM.BaiduAppID != null)
            {
                ApiSign.BaiduAppID = SettingM.BaiduAppID;
            }
            if (SettingM.BaiduAppSecret != null)
            {
                ApiSign.BaiduAppSecret = SettingM.BaiduAppSecret;
            }
        }
    }
}
