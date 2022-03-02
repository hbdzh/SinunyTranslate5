using Microsoft.Toolkit.Mvvm.ComponentModel;
namespace SinunyTranslate.Model
{
    internal class DictManageModel : ObservableObject
    {

        private int selectRow;
        public int SelectRow
        {
            get => selectRow;
            set => SetProperty(ref selectRow, value);
        }
        private string code;
        public string Code
        {
            get => code;
            set => SetProperty(ref code, value);
        }
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        private string author;
        public string Author
        {
            get => author;
            set => SetProperty(ref author, value);
        }
        private string version;
        public string Version
        {
            get => version;
            set => SetProperty(ref version, value);
        }
        private string size;
        public string Size
        {
            get => size;
            set => SetProperty(ref size, value);
        }
        private string intro;
        public string Intro
        {
            get => intro;
            set => SetProperty(ref intro, value);
        }
        private string status;
        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }
    }
}
