using System;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace SinunyTranslate.Utility
{
    internal class Ocr_WindowsOcr
    {
        /// <summary>
        /// 识别图片中的文字
        /// </summary>
        /// <returns></returns>
        internal static async Task<string[]> ImageOcr(string langCode)
        {
            string[] ocrText = new string[2];
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
                ocrText[0] = imgFile.Path;
                using (IRandomAccessStreamWithContentType inStream = await imgFile.OpenReadAsync())
                {
                    //解码图片
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(inStream);
                    //获取图像
                    using (SoftwareBitmap swbmp = await decoder.GetSoftwareBitmapAsync())
                    {
                        //准备识别
                        Language lang = new Language(langCode);
                        //判断是否支持简体中文识别
                        if (OcrEngine.IsLanguageSupported(lang))
                        {
                            OcrEngine engine = OcrEngine.TryCreateFromLanguage(lang);
                            if (engine != null)
                            {
                                OcrResult result = await engine.RecognizeAsync(swbmp);
                                if (result != null)
                                {
                                    ocrText[1] = result.Text;
                                    return ocrText;
                                }
                                else
                                {
                                    ocrText[1] = "识别错误";
                                    return ocrText;
                                }
                            }
                            else
                            {
                                ocrText[1] = "识别引擎启动失败";
                                return ocrText;
                            }
                        }
                        else
                        {
                            ocrText[1] = "不支持" + lang.DisplayName + "的识别，可能是因为你的系统没有安装此语言包，到设置->语言&区域，安装相应的系统语言包之后，就可以使用了";
                            return ocrText;
                        }
                    }
                }
            }
            else
            {
                ocrText[1] = "";
                return ocrText;
            }
        }
    }
}