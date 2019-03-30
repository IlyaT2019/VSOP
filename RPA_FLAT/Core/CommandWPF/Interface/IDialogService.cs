using Microsoft.Win32;
using RPA_FLAT.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_FLAT.Core.CommandWPF.Interface
{
    public interface IDialogService
    {
        InformationDialogService InformationDialogService { get; set; }
        OpenFileDialog OpenFileDialog { get; set; }
        InformationDialogService Open(Dictionary<string, string> filters = null);
    }
}
