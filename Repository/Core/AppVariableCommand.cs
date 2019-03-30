using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Core
{
    public class AppVariableCommand<T>:Variable
    {
        public override string Id { get => base.Id; set => base.Id = value; }

        public NameAppVariableCommand  NameAppVariableCommand { get; set; } 

        public override int Number { get => base.Number; set => base.Number = value; }

        public override bool IsChecked { get => base.IsChecked; set => base.IsChecked = value; }

        public StatusCommand StatusCommand { get; set; }

        public ObservableCollection<AppVariable<T>> CommandVariables { get; set; }
                
        public AppVariableCommand()
        {
            CommandVariables = new ObservableCollection<AppVariable<T>>();           
        }
    }

    public enum StatusCommand
    {
        Dont_Start = 0,
        Start,
        Process,
        Finish
    }       
}
