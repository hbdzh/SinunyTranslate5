using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SinunyTranslate.Common;
using SinunyTranslate.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace SinunyTranslate.ViewModel
{
    internal class TessdataManageViewModel : ObservableObject
    {
        public ICommand PackDoubleTappedCommand { get; set; }
        private TessdataManageModel settingM;
        public TessdataManageModel SettingM
        {
            get { return settingM; }
            set { SetProperty(ref settingM, value); }
        }
        public TessdataManageViewModel()
        {
            SettingM = new TessdataManageModel();
            SettingM.PackList = new List<PackList>();
            PackDoubleTappedCommand = new RelayCommand<object>(new Action<object>(PackDoubleTapped));
            AppConfig.AllOcrLanguage_Tesseract = new Dictionary<string, string> { { "简体中文", "chi_sim" } };
            InitPackList();
        }
        /// <summary>
        /// 双击下载/删除语言包
        /// </summary>
        private async void PackDoubleTapped(object process)
        {
            string selectPack = SettingM.SelectPack.Code;
            StorageFolder storageFolder = ApplicationData.Current.LocalCacheFolder;
            StorageFolder packFolder = await storageFolder.CreateFolderAsync("LanguagePack", CreationCollisionOption.OpenIfExists);
            StorageFolder tessdataFolder = await packFolder.CreateFolderAsync("tessdata", CreationCollisionOption.OpenIfExists);
            StorageFile sampleFile = await tessdataFolder.CreateFileAsync(selectPack + ".traineddata", CreationCollisionOption.ReplaceExisting);
            if (SettingM.SelectPack.Status == "未下载")
            {
                FileDownload fileDownload = new FileDownload((ProgressBar)process, "https://download.meixiapp.com/SinunyTranslate/5_0_0/LanguagePack/tessdata/packs/" + selectPack + ".traineddata", sampleFile);
                fileDownload.worker.RunWorkerAsync();
            }
            else
            {
                await sampleFile.DeleteAsync();
            }
            InitPackList();
        }
        /// <summary>
        /// 初始化语言列表
        /// </summary>
        private async void InitPackList()
        {
            List<PackList> packList = new List<PackList>();
            string jsonCode = await DownloadFile("https://download.meixiapp.com/SinunyTranslate/5_0_0/LanguagePack/tessdata/list.json");
            if (JsonConvert.DeserializeObject(jsonCode) is JObject jo)
            {
                foreach (var item in jo)
                {
                    packList.Add(new PackList { Name = item.Value.ToString(), Code = item.Key.ToString(), Status = PackInstallStatus(item.Key.ToString()) });
                }
                SettingM.PackList = packList;
            }
        }
        /// <summary>
        /// 语言包下载状态
        /// </summary>
        /// <param name="packCode"></param>
        /// <returns></returns>
        private string PackInstallStatus(string packCode)
        {
            string packFile = ApplicationData.Current.LocalCacheFolder.Path + "\\LanguagePack\\tessdata\\" + packCode + ".traineddata";
            if (File.Exists(packFile))
            {
                return "已下载";
            }
            else
            {
                return "未下载";
            }
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
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
    }
}
