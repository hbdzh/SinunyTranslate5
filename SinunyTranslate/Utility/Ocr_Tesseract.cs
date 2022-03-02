using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Tesseract;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace SinunyTranslate.Utility
{
    internal class Ocr_Tesseract
    {
        /// <summary>
        /// 识别图片流中的文字
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        internal static async Task<string> StreamToText(string lang)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".bmp");
            picker.FileTypeFilter.Add(".tiff");
            picker.FileTypeFilter.Add(".gif");
            //选择文件
            StorageFile imgFile = await picker.PickSingleFileAsync();
            if (imgFile != null)
            {
                IBuffer buffer = await FileIO.ReadBufferAsync(imgFile);
                byte[] bytes = buffer.ToArray();
                using (TesseractEngine engine = new TesseractEngine(ApplicationData.Current.LocalCacheFolder.Path + "\\LanguagePack\\tessdata", lang, EngineMode.Default))
                {
                    using (Pix img = Pix.LoadFromMemory(bytes))
                    {
                        using (Page page = engine.Process(img))
                        {
                            return page.GetText();
                        }
                    }
                }
            }
            else
            {
                return "";
            }
        }
    }
}
