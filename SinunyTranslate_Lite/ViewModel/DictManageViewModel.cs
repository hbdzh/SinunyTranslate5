using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SinunyTranslate_Lite.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;

namespace SinunyTranslate_Lite.ViewModel
{
    internal class DictManageViewModel : ObservableObject
    {
        public ICommand DictDoubleTappedCommand { get; set; }

        private List<DictManageModel> dictMangerListM;
        public List<DictManageModel> DictMangerListM
        {
            get { return dictMangerListM; }
            set { SetProperty(ref dictMangerListM, value); }
        }
        private DictManageModel dictMangerM;
        public DictManageModel DictMangerM
        {
            get { return dictMangerM; }
            set { SetProperty(ref dictMangerM, value); }
        }
        public DictManageViewModel()
        {
            DictMangerM = new DictManageModel();
            DictMangerListM = new List<DictManageModel>();
            DictDoubleTappedCommand = new RelayCommand(DictDoubleTapped);
            InitDictionaryList();
        }
        /// <summary>
        /// 初始化词典列表
        /// </summary>
        private async void InitDictionaryList()
        {
            List<DictManageModel> dictList = new List<DictManageModel>();
            string jsonCode = await DownloadFile("https://download.meixiapp.com/SinunyTranslate/5_0_0/Dictionary/list.json");
            if (JsonConvert.DeserializeObject(jsonCode) is JObject jo)
            {
                foreach (var item in jo["dict"])
                {
                    dictList.Add(new DictManageModel { Code = item["code"].ToString(), Name = item["name"].ToString(), Author = item["author"].ToString(), Size = item["size"].ToString(), Intro = item["intro"].ToString(), Status = DictInstallStatus(item["code"].ToString()) });
                }
                DictMangerListM = dictList;
            }
        }
        /// <summary>
        /// 词典下载状态
        /// </summary>
        /// <param name="dictCode"></param>
        /// <returns></returns>
        private string DictInstallStatus(string dictCode)
        {
            string dictFile = ApplicationData.Current.LocalCacheFolder.Path + "\\Dict\\" + dictCode + ".json";
            if (File.Exists(dictFile))
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
        /// <summary>
        /// 双击选中项
        /// </summary>
        private async void DictDoubleTapped()
        {
            string selectDict = DictMangerListM[DictMangerM.SelectRow].Code;
            StorageFolder storageFolder = ApplicationData.Current.LocalCacheFolder;
            StorageFolder dictFolder = await storageFolder.CreateFolderAsync("Dict", CreationCollisionOption.OpenIfExists);
            StorageFile sampleFile = await dictFolder.CreateFileAsync(selectDict + ".json", CreationCollisionOption.ReplaceExisting);
            if (DictMangerListM[DictMangerM.SelectRow].Status == "未下载")
            {
                string dictContent = await DownloadFile("https://download.meixiapp.com/SinunyTranslate/5_0_0/Dictionary/packs/" + selectDict + ".json");
                await FileIO.WriteTextAsync(sampleFile, dictContent);
            }
            else
            {
                await sampleFile.DeleteAsync();
            }
            InitDictionaryList();
        }
    }
}
