using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SinunyTranslate.Common;
using SinunyTranslate.Model;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.Storage;

namespace SinunyTranslate.ViewModel
{
    internal class SettingViewModel : ObservableObject
    {
        public ICommand TranslateApiTextChangedCommand { get; set; }
        public ICommand DefaultTranslateEngineSelectionChangedCommand { get; set; }
        public ICommand ThemeStyleSelectionChangedCommand { get; set; }
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
                ThemeStyle = new List<string>()
            };
            localSettings = ApplicationData.Current.LocalSettings;
            TranslateApiTextChangedCommand = new RelayCommand(TranslateApiTextChanged);
            DefaultTranslateEngineSelectionChangedCommand = new RelayCommand(DefaultTranslateEngineSelectionChanged);
            ThemeStyleSelectionChangedCommand = new RelayCommand(ThemeStyleSelectionChanged);
            InitThemeStyle();
            InitTranslateEngine();
            InitSetting();
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
