using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SinunyTranslate_Lite.Model;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml;

namespace SinunyTranslate_Lite.ViewModel
{
    internal class DictViewModel : ObservableObject
    {
        public ICommand AutoSuggesTextChangedCommand { get; set; }
        public ICommand AutoSuggesQuerySuggestionChosenCommand { get; set; }
        public ICommand ToggleDictionaryCommand { get; set; }

        private DictModel dictM;
        public DictModel DictM
        {
            get { return dictM; }
            set { SetProperty(ref dictM, value); }
        }
        List<string> DictWords { get; set; }
        bool isChoose = false;
        public DictViewModel()
        {
            AutoSuggesTextChangedCommand = new RelayCommand<object>(new Action<object>(AutoSuggesTextChanged));
            AutoSuggesQuerySuggestionChosenCommand = new RelayCommand<object>(new Action<object>(AutoSuggesQuerySuggestionChosen));
            ToggleDictionaryCommand = new RelayCommand<object>(new Action<object>(ToggleDictionary));
            DictM = new DictModel
            {
                QueryAutoList = new List<string>(),
                DictionaryList = new List<string>(),
                ResultShow = Visibility.Collapsed
            };
            DictWords = new List<string>();
            InitDictList();
        }
        /// <summary>
        /// 初始化词典列表
        /// </summary>
        private async void InitDictList()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalCacheFolder;
            StorageFolder dictFolder = await storageFolder.CreateFolderAsync("Dict", CreationCollisionOption.OpenIfExists);
            IReadOnlyList<StorageFile> storageFiles = await dictFolder.GetFilesAsync();
            DictM.DictionaryList.Clear();
            List<string> newDict = new List<string>();
            foreach (var item in storageFiles)
            {
                newDict.Add(item.DisplayName);
            }
            DictM.DictionaryList = newDict;
        }

        /// <summary>
        /// 当自动完成文本框内容发生改变时
        /// </summary>
        /// <param name="obj"></param>
        public void AutoSuggesTextChanged(object obj)
        {
            DictM.QueryResult = "";
            if (!isChoose)
            {
                DictM.QueryAutoList.Clear();
                List<string> subItems = new List<string>();
                if (obj is string queryWord)
                {
                    foreach (var item in DictWords)
                    {
                        if (item.Contains(queryWord))
                        {
                            subItems.Add(item);
                        }
                    }
                    if (subItems.Count == 0)
                    {
                        subItems.Add("没有结果");
                    }
                    DictM.QueryAutoList = subItems;
                }
            }
            isChoose = false;
        }

        /// <summary>
        /// 当用户确认查询内容时
        /// </summary>
        /// <param name="obj"></param>
        public async void AutoSuggesQuerySuggestionChosen(object obj)
        {
            isChoose = true;
            if (!string.IsNullOrEmpty(DictM.SelectDictionary))
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalCacheFolder;
                StorageFolder dictFolder = await storageFolder.CreateFolderAsync("Dict", CreationCollisionOption.OpenIfExists);
                StorageFile storageFile = await dictFolder.GetFileAsync(DictM.SelectDictionary + ".json");
                string jsonCode = await FileIO.ReadTextAsync(storageFile);
                if (JsonConvert.DeserializeObject(jsonCode) is JObject jo)
                {
                    if (obj is string queryWord)
                    {
                        if (DictWords.Contains(queryWord))
                        {
                            DictM.QueryResult = jo[queryWord].ToString();
                            DictM.ResultShow = Visibility.Visible;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 切换词典
        /// </summary>
        public async void ToggleDictionary(object obj)
        {
            if (obj is string selectDict)
            {
                DictM.SelectDictionary = selectDict;
                StorageFolder storageFolder = ApplicationData.Current.LocalCacheFolder;
                StorageFolder dictFolder = await storageFolder.CreateFolderAsync("Dict", CreationCollisionOption.OpenIfExists);
                StorageFile storageFile = await dictFolder.GetFileAsync(selectDict + ".json");
                string jsonCode = await FileIO.ReadTextAsync(storageFile);
                DictM.QueryAutoList.Clear();
                DictWords.Clear();
                if (JsonConvert.DeserializeObject(jsonCode) is JObject jo)
                {
                    foreach (var item in jo)
                    {
                        DictM.QueryAutoList.Add(item.Key);
                        DictWords.Add(item.Key);
                    }
                }
            }
        }

    }
}
