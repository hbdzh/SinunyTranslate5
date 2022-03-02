using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace SinunyTranslate.Model
{
    internal class TransModel : ObservableObject
    {
        private List<string> languageList;
        /// <summary>
        /// 语言列表
        /// </summary>
        public List<string> LanguageList
        {
            get => languageList;
            set => SetProperty(ref languageList, value);
        }
        private List<string> tranEngineList;
        /// <summary>
        /// 翻译引擎列表
        /// </summary>
        public List<string> TranEngineList
        {
            get => tranEngineList;
            set => SetProperty(ref tranEngineList, value);
        }
        private string useTranEngine;
        /// <summary>
        /// 要使用的翻译引擎
        /// </summary>
        public string UseTranEngine
        {
            get => useTranEngine;
            set => SetProperty(ref useTranEngine, value);
        }
        private string sourceLanguage;
        /// <summary>
        /// 选择的源语言
        /// </summary>
        public string SourceLanguage
        {
            get => sourceLanguage;
            set => SetProperty(ref sourceLanguage, value);
        }
        private string targetLanguage;
        /// <summary>
        /// 选择的目标语言
        /// </summary>
        public string TargetLanguage
        {
            get => targetLanguage;
            set => SetProperty(ref targetLanguage, value);
        }
        private string translateContent;
        /// <summary>
        /// 要翻译的内容
        /// </summary>
        public string TranslateContent
        {
            get => translateContent;
            set => SetProperty(ref translateContent, value);
        }
        private string translateResult;
        /// <summary>
        /// 翻译后的结果
        /// </summary>
        public string TranslateResult
        {
            get => translateResult;
            set => SetProperty(ref translateResult, value);
        }
        private string translateExplains;
        /// <summary>
        /// 翻译后的解释
        /// </summary>
        public string TranslateExplains
        {
            get => translateExplains;
            set => SetProperty(ref translateExplains, value);
        }
        private string translateWeb;
        /// <summary>
        /// 翻译后的网络释义
        /// </summary>
        public string TranslateWeb
        {
            get => translateWeb;
            set => SetProperty(ref translateWeb, value);
        }
        private Visibility resultShow;
        /// <summary>
        /// 是否显示翻译结果
        /// </summary>
        public Visibility ResultShow
        {
            get => resultShow;
            set => SetProperty(ref resultShow, value);
        }
        private Visibility explainsShow;
        /// <summary>
        /// 是否显示翻译解释
        /// </summary>
        public Visibility ExplainsShow
        {
            get => explainsShow;
            set => SetProperty(ref explainsShow, value);
        }
        private Visibility webShow;
        /// <summary>
        /// 是否显示翻译网络释义
        /// </summary>
        public Visibility WebShow
        {
            get => webShow;
            set => SetProperty(ref webShow, value);
        }
    }
}