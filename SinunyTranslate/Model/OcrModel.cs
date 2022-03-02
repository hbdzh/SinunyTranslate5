using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace SinunyTranslate.Model
{
    internal class OcrModel : ObservableObject
    {
        private string imagePath;
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImagePath
        {
            get => imagePath;
            set => SetProperty(ref imagePath, value);
        }
        private string imageText;
        /// <summary>
        /// 识别到的图片文字
        /// </summary>
        public string ImageText
        {
            get => imageText;
            set => SetProperty(ref imageText, value);
        }
        private List<string> ocrLanguageList;
        /// <summary>
        /// Ocr支持的语言列表
        /// </summary>
        public List<string> OcrLanguageList
        {
            get => ocrLanguageList;
            set => SetProperty(ref ocrLanguageList, value);
        }
        private string selectOcrLang;
        /// <summary>
        /// 选择的Ocr语言
        /// </summary>
        public string SelectOcrLang
        {
            get => selectOcrLang;
            set => SetProperty(ref selectOcrLang, value);
        }
        private List<string> ocrEngineList;
        /// <summary>
        /// Ocr支持的引擎
        /// </summary>
        public List<string> OcrEngineList
        {
            get => ocrEngineList;
            set => SetProperty(ref ocrEngineList, value);
        }
        private string selectOcrEngine;
        /// <summary>
        /// 选择的Ocr引擎
        /// </summary>
        public string SelectOcrEngine
        {
            get => selectOcrEngine;
            set => SetProperty(ref selectOcrEngine, value);
        }
        private Visibility ocrResultShow;
        /// <summary>
        /// 控制识别结果显示与否
        /// </summary>
        public Visibility OcrResultShow
        {
            get => ocrResultShow;
            set => SetProperty(ref ocrResultShow, value);
        }
    }
}