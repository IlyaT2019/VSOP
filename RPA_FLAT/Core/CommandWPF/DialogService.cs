using Microsoft.Win32;
using RPA_FLAT.Core.CommandWPF.Interface;
using RPA_FLAT.Core.Model;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RPA_FLAT.Core.CommandWPF
{
    public class DialogService : IDialogService, IDisposable
    {
        public InformationDialogService InformationDialogService { get; set; }
        public OpenFileDialog OpenFileDialog { get; set; }

        public DialogService()
        {
            OpenFileDialog = new OpenFileDialog();            
        }       

        public InformationDialogService Open(Dictionary<string,string> filters = null)
        {
            if (OpenFileDialog.ShowDialog() == true)
            {
                InformationDialogService = new InformationDialogService();
                InformationDialogService.Path = OpenFileDialog.FileName;
                InformationDialogService.NameFile = Path.GetFileNameWithoutExtension(OpenFileDialog.FileName);
                switch (Path.GetExtension(OpenFileDialog.FileName).Replace(".",""))
                {
                    case "xlsx": InformationDialogService.ExpansionFile = ExtensionFile.xlsx; break;
                    case "csv": InformationDialogService.ExpansionFile = ExtensionFile.csv; break;
                    case "txt": InformationDialogService.ExpansionFile = ExtensionFile.txt; break;
                    default: MessageBox.Show("Неверный тип файла"); break;
                }
            }
            return InformationDialogService;
        }        

        public void Dispose()
        {
            
        }
    }
}
