using Office.Core;
using Repository.Core;
using RPA_FLAT.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Tool;

namespace RPA_FLAT.Core.View
{
    public class BaseView : INotifyPropertyChanged
    {
        private AppRepository<object> _appRepository;
        

        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string NameProperty = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(NameProperty));
        }
    }
}
