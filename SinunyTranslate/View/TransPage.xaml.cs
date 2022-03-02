using SinunyTranslate.ViewModel;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace SinunyTranslate.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TransPage : Page
    {
        internal TransViewModel TransVM { get; set; }
        public TransPage()
        {
            this.InitializeComponent();
            TransVM = new TransViewModel();
        }
    }
}
