using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinunyTranslate.Model
{
    internal class TessdataManageModel : ObservableObject
    {
        private List<PackList> packList;
        public List<PackList> PackList
        {
            get => packList;
            set => SetProperty(ref packList, value);
        }
        private PackList selectPack;
        public PackList SelectPack
        {
            get => selectPack;
            set => SetProperty(ref selectPack, value);
        }
    }
    internal class PackList : ObservableObject
    {
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        private string code;
        public string Code
        {
            get => code;
            set => SetProperty(ref code, value);
        }
        private string status;
        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }
    }
}
