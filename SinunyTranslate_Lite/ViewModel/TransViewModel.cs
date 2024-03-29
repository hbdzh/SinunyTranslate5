﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SinunyTranslate_Lite.Common;
using SinunyTranslate_Lite.Model;
using SinunyTranslate_Lite.Utility;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SinunyTranslate_Lite.ViewModel
{
    internal class TransViewModel : ObservableObject
    {
        public ICommand StartTranslateCommand { get; set; }
        public ICommand PlayAudioCommand { get; set; }
        public ICommand AudioPlayer_MediaEndedCommand { get; set; }
        public ICommand VoiceInputCommand { get; set; }
        private TransModel tran;
        public TransModel Tran
        {
            get { return tran; }
            set { SetProperty(ref tran, value); }
        }
        public TransViewModel()
        {
            Tran = new TransModel
            {
                LanguageList = AppConfig.AllTranslateLanguage,
                TranEngineList = AppConfig.AllTranslateEngine,
                SourceLanguage = "自动检测",
                TargetLanguage = "自动检测",
                UseTranEngine = AppConfig.UseTranslateEngine
            };
            StartTranslateCommand = new RelayCommand(StartTranslate);
            PlayAudioCommand = new RelayCommand<object>(new Action<object>(PlayAudio));
            AudioPlayer_MediaEndedCommand = new RelayCommand(AudioPlayer_MediaEnded);
            VoiceInputCommand = new RelayCommand(VoiceInput);
            Tran.ResultShow = Visibility.Collapsed;
            Tran.ExplainsShow = Visibility.Collapsed;
            Tran.WebShow = Visibility.Collapsed;
        }
        /// <summary>
        /// 语音输入
        /// </summary>
        private async void VoiceInput()
        {
            if (Tran.IsSpeechRecognition == true)
            {
                using (SpeechRecognizer recognizer = new SpeechRecognizer())
                {
                    SpeechRecognitionCompilationResult compilationResult = await recognizer.CompileConstraintsAsync();
                    if (compilationResult.Status == SpeechRecognitionResultStatus.Success)
                    {
                        recognizer.UIOptions.IsReadBackEnabled = false;
                        recognizer.UIOptions.ShowConfirmation = true;
                        recognizer.UIOptions.AudiblePrompt = "我在听，请说...";
                        recognizer.Timeouts.InitialSilenceTimeout = TimeSpan.FromSeconds(1);
                        SpeechRecognitionResult recognitionResult = await recognizer.RecognizeWithUIAsync();
                        if (recognitionResult.Status == SpeechRecognitionResultStatus.Success)
                        {
                            Tran.TranslateContent = recognitionResult.Text;
                        }
                    }
                    Tran.IsSpeechRecognition = false;
                }
            }
        }
        /// <summary>
        /// 语音朗读
        /// </summary>
        /// <param name="status"></param>
        private async void PlayAudio(object status)
        {
            MediaElement audioPlayer = (MediaElement)status;
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                if (Tran.IsAudioPlaying == true)
                {
                    SpeechSynthesisStream stream = await synthesizer.SynthesizeTextToStreamAsync(Tran.TranslateResult);
                    // 播放生成的语音
                    audioPlayer.SetSource(stream, stream.ContentType);
                }
                else
                {
                    audioPlayer.Stop();
                }
            }
        }
        /// <summary>
        /// 朗读完成后
        /// </summary>
        private void AudioPlayer_MediaEnded()
        {
            Tran.IsAudioPlaying = false;
        }
        /// <summary>
        /// 开始翻译
        /// </summary>
        private void StartTranslate()
        {
            Task.Run(async delegate
            {
                await Task.Delay(AppConfig.UseDelayTime);//延时0.5秒翻译
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Translate();
                });
            });
        }
        /// <summary>
        /// 翻译
        /// </summary>
        private async void Translate()
        {
            if (!string.IsNullOrEmpty(Tran.SourceLanguage) && !string.IsNullOrEmpty(Tran.TargetLanguage) && !string.IsNullOrEmpty(Tran.UseTranEngine) && !string.IsNullOrEmpty(Tran.TranslateContent))
            {
                string q = Tran.TranslateContent;
                string jsonCode;
                Tran.ResultShow = Visibility.Visible;
                if (Tran.TargetLanguage == "中文（文言文）" || Tran.TargetLanguage == "中文（繁体）")
                {
                    jsonCode = await Trans_Baidu.GetJson(q, TransCodeConvert.BaiduLanguageConvert(Tran.SourceLanguage), TransCodeConvert.BaiduLanguageConvert(Tran.TargetLanguage));
                    Tran.TranslateResult = Trans_Baidu.GetResult(jsonCode).ToString();
                    Tran.ExplainsShow = Visibility.Collapsed;
                    Tran.WebShow = Visibility.Collapsed;
                    return;
                }
                else if (Tran.TargetLanguage == "蒙古语")
                {
                    jsonCode = await Trans_Youdao.GetJson(q, TransCodeConvert.YoudaoLanguageConvert(Tran.SourceLanguage), TransCodeConvert.YoudaoLanguageConvert(Tran.TargetLanguage));
                    Tran.TranslateResult = Trans_Youdao.GetResult(jsonCode)[0];
                    Tran.ExplainsShow = Visibility.Collapsed;
                    Tran.WebShow = Visibility.Collapsed;
                    return;
                }
                else
                {
                    JudgeEngine(q);
                }
            }
        }
        /// <summary>
        /// 判断使用的引擎
        /// </summary>
        /// <param name="jsonCode"></param>
        /// <param name="q"></param>
        private async void JudgeEngine(string q)
        {
            string jsonCode;
            switch (Tran.UseTranEngine)
            {
                case "有道翻译（免费版）":
                    string type = TransCodeConvert.YoudaoFreeLanguageConvert(Tran.SourceLanguage) + "2" + TransCodeConvert.YoudaoFreeLanguageConvert(Tran.TargetLanguage);
                    if (type.Contains("ERROR"))
                    {
                        Tran.TranslateResult = "有道翻译Free引擎不支持该类型的语言";
                    }
                    else
                    {
                        if (type.Contains("AUTO"))
                        {
                            string autoLanguage = DetectLanguage.GetLanguageType(q);
                            if (type.Substring(0, 4) == "AUTO")
                            {
                                type = TransCodeConvert.YoudaoFreeLanguageConvert(autoLanguage) + "2" + TransCodeConvert.YoudaoFreeLanguageConvert(Tran.TargetLanguage);
                            }
                            else
                            {
                                type = TransCodeConvert.YoudaoFreeLanguageConvert(Tran.TargetLanguage) + "2" + TransCodeConvert.YoudaoFreeLanguageConvert(autoLanguage);
                            }
                            jsonCode = await Trans_YoudaoFree.GetJson(q, type);
                            Tran.TranslateResult = Trans_YoudaoFree.GetResult(jsonCode);
                        }
                        else
                        {
                            if (type.Contains("ZH_CN"))
                            {
                                jsonCode = await Trans_YoudaoFree.GetJson(q, type);
                                Tran.TranslateResult = Trans_YoudaoFree.GetResult(jsonCode);
                            }
                            else
                            {
                                Tran.TranslateResult = "有道翻译Free引擎不支持这两个语言支持互相转换";
                            }
                        }
                    }
                    Tran.ExplainsShow = Visibility.Collapsed;
                    Tran.WebShow = Visibility.Collapsed;
                    break;
                case "有道翻译":
                    jsonCode = await Trans_Youdao.GetJson(q, TransCodeConvert.YoudaoLanguageConvert(Tran.SourceLanguage), TransCodeConvert.YoudaoLanguageConvert(Tran.TargetLanguage));
                    Tran.TranslateResult = Trans_Youdao.GetResult(jsonCode)[0];
                    if (!string.IsNullOrEmpty(Trans_Youdao.GetResult(jsonCode)[1]))
                    {
                        Tran.TranslateExplains = Trans_Youdao.GetResult(jsonCode)[1];
                        Tran.ExplainsShow = Visibility.Visible;
                    }
                    else
                    {
                        Tran.ExplainsShow = Visibility.Collapsed;
                    }
                    if (!string.IsNullOrEmpty(Trans_Youdao.GetResult(jsonCode)[2]))
                    {
                        Tran.TranslateWeb = Trans_Youdao.GetResult(jsonCode)[2];
                        Tran.WebShow = Visibility.Visible;
                    }
                    else
                    {
                        Tran.WebShow = Visibility.Collapsed;
                    }
                    break;
                case "百度翻译":
                    jsonCode = await Trans_Baidu.GetJson(q, TransCodeConvert.BaiduLanguageConvert(Tran.SourceLanguage), TransCodeConvert.BaiduLanguageConvert(Tran.TargetLanguage));
                    Tran.TranslateResult = Trans_Baidu.GetResult(jsonCode).ToString();
                    Tran.ExplainsShow = Visibility.Collapsed;
                    Tran.WebShow = Visibility.Collapsed;
                    break;
                case "必应词典":
                    string[] result = await Trans_Bing.QueryDict(q);
                    Tran.TranslateResult = result[0];
                    Tran.ExplainsShow = Visibility.Collapsed;
                    if (!string.IsNullOrEmpty(result[1]))
                    {
                        Tran.TranslateWeb = result[1];
                        Tran.WebShow = Visibility.Visible;
                    }
                    else
                    {
                        Tran.WebShow = Visibility.Collapsed;
                    }
                    break;
            }
        }
    }
}