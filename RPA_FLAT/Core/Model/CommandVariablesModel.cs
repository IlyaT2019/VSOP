using Repository.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace RPA_FLAT.Core.Model
{
    public class CommandVariablesModel
    {
        private List<ObservableCollection<AppVariable<object>>> commandVariables { get; set; }
        public List<ObservableCollection<AppVariable<object>>> CommandVariables { get; set; }

        public CommandVariablesModel()
        {
            CommandVariables = new List<ObservableCollection<AppVariable<object>>>();
        }        
    }
}