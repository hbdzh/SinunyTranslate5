using Windows.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace SinunyTranslate.Common
{
    internal class FileDownload
    {
        public FileDownload(ProgressBar progressBar, string fileUrl, StorageFile file)
        {
            progress = progressBar;
            url = fileUrl;
            worker = new BackgroundWorker();
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted); ;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            sampleFile = file;
        }
        ProgressBar progress;
        string url;
        StorageFile sampleFile;
        public BackgroundWorker worker;
        bool isDownloading = false;
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            dowloadFile();
        }
        public async void Invoke(Action action, Windows.UI.Core.CoreDispatcherPriority Priority = Windows.UI.Core.CoreDispatcherPriority.Normal)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Priority, () => { action(); });
        }
        async void dowloadFile()
        {
            if (isDownloading) return;
            isDownloading = true;
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.WebResponse response = await request.GetResponseAsync();
            System.IO.Stream ns = response.GetResponseStream();
            long totalSize = response.ContentLength;
            double hasDownSize = 0;
            byte[] nbytes = new byte[512];//521,2048 etc
            int nReadSize = 0;
            nReadSize = ns.Read(nbytes, 0, nbytes.Length);
            using (StorageStreamTransaction transaction = await sampleFile.OpenTransactedWriteAsync())
            {
                using (DataWriter dataWriter = new DataWriter(transaction.Stream))
                {
                    while (nReadSize > 0)
                    {
                        dataWriter.WriteBytes(nbytes);
                        nReadSize = ns.Read(nbytes, 0, 512);
                        hasDownSize += nReadSize;
                        Invoke(new Action(() =>
                        {
                            progress.Maximum = totalSize;
                            progress.Value = hasDownSize;
                        }));
                    }
                    transaction.Stream.Size = await dataWriter.StoreAsync();
                    await dataWriter.FlushAsync();
                    await transaction.CommitAsync();
                }
            }
            isDownloading = false;
        }
    }
}
