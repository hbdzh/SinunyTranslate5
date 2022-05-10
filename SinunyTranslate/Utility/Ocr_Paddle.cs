using PaddleOCRSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace SinunyTranslate.Utility
{
    internal class Ocr_Paddle
    {
        internal static PaddleOCREngine engine;
        /// <summary>
        /// 初始化引擎（建议程序全局初始化一次即可，不必每次识别都初始化，容易报错）
        /// </summary>
        internal static void InitEngine()
        {
            OCRModelConfig config = null;
            OCRParameter oCRParameter = new OCRParameter();
            engine = new PaddleOCREngine(config, oCRParameter);
        }
        /// <summary>
        /// 启动引擎开始识别
        /// </summary>
        /// <returns>识别内容</returns>
        internal async Task<string> StartEngine()
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".bmp");
            picker.FileTypeFilter.Add(".tiff");
            picker.FileTypeFilter.Add(".gif");
            //选择文件
            StorageFile selectFile = await picker.PickSingleFileAsync();
            string result = "";
            if (selectFile != null)
            {
                using (IRandomAccessStream randomStream = await selectFile.OpenAsync(FileAccessMode.ReadWrite, StorageOpenOptions.None))
                {
                    //将IRandomAccessStream转为byte[]
                    Stream stream = WindowsRuntimeStreamExtensions.AsStreamForRead(randomStream.GetInputStreamAt(0));
                    MemoryStream ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    byte[] bytes = ms.ToArray();
                    OCRResult ocrResult;
                    ocrResult = engine.DetectText(bytes);
                    if (ocrResult != null)
                    {
                        result = ocrResult.Text;
                    }
                }
            }
            return result;
        }
    }
}
