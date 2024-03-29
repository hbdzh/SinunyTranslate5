﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SinunyTranslate_Lite.Common;
using SinunyTranslate_Lite.Model;
using SinunyTranslate_Lite.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml;

namespace SinunyTranslate_Lite.ViewModel
{
    internal class OcrViewModel : ObservableObject
    {
        public ICommand SelectImageCommand { get; set; }

        private OcrModel ocrM;
        public OcrModel OcrM
        {
            get { return ocrM; }
            set { SetProperty(ref ocrM, value); }
        }
        public OcrViewModel()
        {
            InitTessdataLanguagePack();
            OcrM = new OcrModel
            {
                OcrEngineList = AppConfig.AllOcrEngine,
                SelectOcrEngine = "Tesseract"
            };
            SelectImageCommand = new RelayCommand(SelectImage);
            OcrM.OcrResultShow = Visibility.Collapsed;
        }
        private async void InitTessdataLanguagePack()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalCacheFolder;
            StorageFolder packFolder = await storageFolder.CreateFolderAsync("LanguagePack", CreationCollisionOption.OpenIfExists);
            StorageFolder tessdataFolder = await packFolder.CreateFolderAsync("tessdata", CreationCollisionOption.OpenIfExists);
            string jsonCode = await DownloadFile("https://download.meixiapp.com/SinunyTranslate/5_0_0/LanguagePack/tessdata/list.json");
            IReadOnlyList<StorageFile> storageFile = await tessdataFolder.GetFilesAsync();
            if (storageFile.Count > 0)//大于0说明本地有语言包文件
            {
                foreach (var item in storageFile)
                {
                    if (JsonConvert.DeserializeObject(jsonCode) is JObject jo)
                    {
                        //检查Dictionary中有没有这个key
                        if (!AppConfig.AllOcrLanguage.ContainsKey(jo[item.DisplayName].ToString()))
                        {
                            AppConfig.AllOcrLanguage.Add(jo[item.DisplayName].ToString(), item.DisplayName);
                        }
                    }
                }
            }
            OcrM.OcrLanguageList = AppConfig.AllOcrLanguage.Keys.ToList();
            OcrM.SelectOcrLang = "简体中文";
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns>读取的json</returns>
        internal static async Task<string> DownloadFile(string fileUrl)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, fileUrl);
            var resp = await httpClient.SendAsync(httpRequestMessage);
            using (Stream stream = await resp.Content.ReadAsStreamAsync())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// 选择图片的按钮点击事件
        /// </summary>
        public async void SelectImage()
        {
            string lang;
            switch (OcrM.SelectOcrEngine)
            {
                case "WindowsOcr":
                    lang = AppConfig.WindowsOcrLanguage[OcrM.SelectOcrLang];
                    string[] result = await Ocr_WindowsOcr.ImageOcr(lang);
                    OcrM.ImageText = result[1];
                    break;
            }
            OcrM.OcrResultShow = Visibility.Visible;
        }
    }
}
