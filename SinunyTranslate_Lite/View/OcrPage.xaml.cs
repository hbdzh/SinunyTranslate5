using SinunyTranslate_Lite.ViewModel;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace SinunyTranslate_Lite.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class OcrPage : Page
    {
        internal OcrViewModel OcrVM { get; set; }
        public OcrPage()
        {
            this.InitializeComponent();
            OcrVM = new OcrViewModel();
        }
    }
}
