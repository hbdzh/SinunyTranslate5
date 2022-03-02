using Microsoft.Toolkit.Mvvm.ComponentModel;
using Windows.UI.Xaml;

namespace SinunyTranslate.Model
{
    internal class ToolModel : ObservableObject
    {
        private string queryContent;
        /// <summary>
        /// 要查询的内容
        /// </summary>
        public string QueryContent
        {
            get => queryContent;
            set => SetProperty(ref queryContent, value);
        }
        private string queryResult;
        /// <summary>
        /// 查询结果
        /// </summary>
        public string QueryResult
        {
            get => queryResult;
            set => SetProperty(ref queryResult, value);
        }
        private Visibility resultShow;
        /// <summary>
        /// 显示查询结果
        /// </summary>
        public Visibility ResultShow
        {
            get => resultShow;
            set => SetProperty(ref resultShow, value);
        }
    }
}
