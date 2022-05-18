using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace SinunyTranslate_Lite.Model
{
    internal class DictModel : ObservableObject
    {
        private List<string> queryAutoList;
        /// <summary>
        /// 要查询的词列表
        /// </summary>
        public List<string> QueryAutoList
        {
            get => queryAutoList;
            set => SetProperty(ref queryAutoList, value);
        }
        private string queryResult;
        /// <summary>
        /// 词典查询结果
        /// </summary>
        public string QueryResult
        {
            get => queryResult;
            set => SetProperty(ref queryResult, value);
        }
        private List<string> dictionaryList;
        /// <summary>
        /// 词典列表
        /// </summary>
        public List<string> DictionaryList
        {
            get => dictionaryList;
            set => SetProperty(ref dictionaryList, value);
        }
        private string selectDictionary;
        /// <summary>
        /// 选择的词典
        /// </summary>
        public string SelectDictionary
        {
            get => selectDictionary;
            set => SetProperty(ref selectDictionary, value);
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
