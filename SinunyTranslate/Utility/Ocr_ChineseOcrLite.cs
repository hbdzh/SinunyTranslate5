using SinunyTranslate.Utility.ChineseOcr_Lite;
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace SinunyTranslate.Utility
{
    internal class Ocr_ChineseOcrLite
    {
        private OcrLite ocrEngine;
        /// <summary>
        /// 引擎初始化
        /// </summary>
        internal void EngineInit()
        {
            string detPath = "Resource\\ChineseOcr_Lite\\models\\dbnet.onnx";
            string clsPath = "Resource\\ChineseOcr_Lite\\models\\angle_net.onnx";
            string recPath = "Resource\\ChineseOcr_Lite\\models\\crnn_lite_lstm.onnx";
            string keysPath = "Resource\\ChineseOcr_Lite\\models\\keys.txt";
            int threadCount = 4;
            ocrEngine = new OcrLite();
            ocrEngine.InitModels(detPath, clsPath, recPath, keysPath, threadCount);
        }
        /// <summary>
        /// 启动引擎开始识别
        /// </summary>
        /// <returns></returns>
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
            WriteableBitmap openBitmap = await OpenWriteableBitmapFile(selectFile);
            StorageFolder storageFolder = ApplicationData.Current.LocalCacheFolder;
            StorageFile createFile = await storageFolder.CreateFileAsync(selectFile.Name, CreationCollisionOption.ReplaceExisting);
            await SaveWriteableBitmapImageFile(openBitmap, createFile);
            if (createFile != null)
            {
                int padding = 50;
                int imgResize = 1024;
                float boxScoreThresh = 0.618f;
                float boxThresh = 0.300f;
                float unClipRatio = 2.0f;
                bool doAngle = false;
                bool mostAngle = false;
                OcrResult ocrResult = ocrEngine.Detect(createFile.Path, padding, imgResize, boxScoreThresh, boxThresh, unClipRatio, doAngle, mostAngle);
                string result = ocrResult.StrRes;
                await createFile.DeleteAsync();
                return result;
            }
            else
            {
                await createFile.DeleteAsync();
                return "";
            }
        }
        /// <summary>
        /// 将图片保存到指定目录
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="file">保存路径</param>
        /// <returns></returns>
        private static async Task SaveWriteableBitmapImageFile(WriteableBitmap image, StorageFile file)
        {
            //BitmapEncoder 存放格式
            Guid bitmapEncoderGuid = BitmapEncoder.JpegEncoderId;
            string filename = file.Name;
            if (filename.EndsWith("jpg"))
            {
                bitmapEncoderGuid = BitmapEncoder.JpegEncoderId;
            }
            else if (filename.EndsWith("png"))
            {
                bitmapEncoderGuid = BitmapEncoder.PngEncoderId;
            }
            else if (filename.EndsWith("bmp"))
            {
                bitmapEncoderGuid = BitmapEncoder.BmpEncoderId;
            }
            else if (filename.EndsWith("tiff"))
            {
                bitmapEncoderGuid = BitmapEncoder.TiffEncoderId;
            }
            else if (filename.EndsWith("gif"))
            {
                bitmapEncoderGuid = BitmapEncoder.GifEncoderId;
            }
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite, StorageOpenOptions.None))
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(bitmapEncoderGuid, stream);
                Stream pixelStream = image.PixelBuffer.AsStream();
                byte[] pixels = new byte[pixelStream.Length];
                await pixelStream.ReadAsync(pixels, 0, pixels.Length);

                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
                          (uint)image.PixelWidth,
                          (uint)image.PixelHeight,
                          96.0,
                          96.0,
                          pixels);
                await encoder.FlushAsync();
            }
        }
        /// <summary>
        /// 读取图片
        /// </summary>
        /// <param name="file">读取的文件</param>
        /// <returns>WriteableBitmap</returns>
        private static async Task<WriteableBitmap> OpenWriteableBitmapFile(StorageFile file)
        {
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                WriteableBitmap image = new WriteableBitmap((int)decoder.PixelWidth, (int)decoder.PixelHeight);
                image.SetSource(stream);

                return image;
            }
        }
    }
}
