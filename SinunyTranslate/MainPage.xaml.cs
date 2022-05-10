using Microsoft.UI.Xaml.Controls;
using SinunyTranslate.Common;
using SinunyTranslate.View;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace SinunyTranslate
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //调整窗口大小
            ApplicationView.PreferredLaunchViewSize = new Size(950, 650);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            //自定义TitleBar
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            ApplicationViewTitleBar appTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            appTitleBar.ButtonBackgroundColor = Colors.Transparent;
            appTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            UpdateTitleBarLayout();

            // Set XAML element as a draggable region.
            Window.Current.SetTitleBar(AppTitleBar);

            LoadThemeStyle();
            LoadSetting();
            ContentFrame.Navigate(typeof(TransPage));
        }
        private void UpdateTitleBarLayout()
        {
            // Get the size of the caption controls area and back button 
            // (returned in logical pixels), and move your content around as necessary.
            TitleBar.Margin = new Thickness(15, 15, 0, 10);
        }

        private void AppNav_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            //先判断是否选中了setting
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingPage));
            }
            else
            {
                //选中项的内容
                switch (args.InvokedItem)
                {
                    case "翻译":
                        ContentFrame.Navigate(typeof(TransPage));
                        break;
                    case "词典":
                        ContentFrame.Navigate(typeof(DictPage));
                        break;
                    case "工具箱":
                        ContentFrame.Navigate(typeof(ToolPage));
                        break;
                    case "文字识别":
                        ContentFrame.Navigate(typeof(OcrPage));
                        break;
                    case "管理":
                        ContentFrame.Navigate(typeof(ManagePage));
                        break;
                }
            }
            SaveSetting();
            LoadThemeStyle();
        }
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        /// <summary>
        /// 加载App的设置
        /// </summary>
        private void LoadSetting()
        {
            ApiSign.YoudaoAppID = (string)localSettings.Values["YoudaoAppID"];
            ApiSign.YoudaoAppSecret = (string)localSettings.Values["YoudaoAppSecret"];
            ApiSign.BaiduAppID = (string)localSettings.Values["BaiduAppID"];
            ApiSign.BaiduAppSecret = (string)localSettings.Values["BaiduAppSecret"];
        }
        /// <summary>
        /// 保存App设置
        /// </summary>
        private void SaveSetting()
        {
            localSettings.Values["YoudaoAppID"] = ApiSign.YoudaoAppID;
            localSettings.Values["YoudaoAppSecret"] = ApiSign.YoudaoAppSecret;
            localSettings.Values["BaiduAppID"] = ApiSign.BaiduAppID;
            localSettings.Values["BaiduAppSecret"] = ApiSign.BaiduAppSecret;
        }
        /// <summary>
        /// 加载储存的主题样式
        /// </summary>
        private void LoadThemeStyle()
        {
            if ((string)localSettings.Values["ThemeStyle"] == "Mica")
            {
                BackdropMaterial.SetApplyToRootOrPageBackground(this, true);
            }
            else if((string)localSettings.Values["ThemeStyle"] == "Acrylic")
            {
                BackdropMaterial.SetApplyToRootOrPageBackground(this, false);
                Background = (Windows.UI.Xaml.Media.Brush)Application.Current.Resources["AcrylicBackgroundFillColorDefaultBrush"];
            }
            else
            {
                BackdropMaterial.SetApplyToRootOrPageBackground(this, true);
            }
        }
    }
}
