using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SinunyTranslate.Model;
using SinunyTranslate.Utility;
using System.Windows.Input;
using Windows.UI.Xaml;

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
        public ToolViewModel()
        {
            ToolBoxM = new ToolModel();
            QueryJinyiCommand = new RelayCommand(QueryJinyi);
            QueryFanyiCommand = new RelayCommand(QueryFanyi);
            QueryWordsCommand = new RelayCommand(QueryWords);
            QueryIdiomsCommand = new RelayCommand(QueryIdioms);
            ToolBoxM.ResultShow = Visibility.Collapsed;
        }
        /// <summary>
        /// 查询近义词
        /// </summary>
        public async void QueryJinyi()
        {
            string jsonCode = await FuncTools.GetJson("http://api.jisuapi.com/jinyifanyi/word?appkey=57f22dc38f471349&word=", ToolBoxM.QueryContent);
            ToolBoxM.QueryResult = FuncTools.JinyiQuery(jsonCode);
            ToolBoxM.ResultShow = Visibility.Visible;
        }
        /// <summary>
        /// 查询反义词
        /// </summary>
        public async void QueryFanyi()
        {
            string jsonCode = await FuncTools.GetJson("http://api.jisuapi.com/jinyifanyi/word?appkey=57f22dc38f471349&word=", ToolBoxM.QueryContent);
            ToolBoxM.QueryResult = FuncTools.FanyiQuery(jsonCode);
            ToolBoxM.ResultShow = Visibility.Visible;
        }
        /// <summary>
        /// 查询词语
        /// </summary>
        public async void QueryWords()
        {
            string jsonCode = await FuncTools.GetJson("http://api.tianapi.com/txapi/lexicon/index?key=5a08864eba64801f1d2d2336db119e9b&word=", ToolBoxM.QueryContent);
            ToolBoxM.QueryResult = FuncTools.WordsQuery(jsonCode);
            ToolBoxM.ResultShow = Visibility.Visible;
        }
        /// <summary>
        /// 查询成语
        /// </summary>
        public async void QueryIdioms()
        {
            string jsonCode = await FuncTools.GetJson("http://v.juhe.cn/chengyu/query?key=2e476981841b96c5f88bd2697adb0461&word=", ToolBoxM.QueryContent);
            ToolBoxM.QueryResult = FuncTools.IdiomsQuery(jsonCode);
            ToolBoxM.ResultShow = Visibility.Visible;
        }
    }
}
