using Repository.Core;
using RPA_FLAT.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RPA_FLAT.Core.ViewModel
{
    public class CommandVariablesView
    {
        private List<Expander> expanders { get; set; }
        public List<Expander> Expanders
        {
            get => expanders;
            set
            {
                expanders = value;
            }
        }        

        public CommandVariablesView(AppRepository<object> appRepository, List<ObservableCollection<AppVariable<object>>> commandVariables)
        {
            InitExpanders(appRepository, commandVariables);
        }

        private void InitExpanders(AppRepository<object> appRepository, List<ObservableCollection<AppVariable<object>>> commandVariables)
        {
            Expanders = new List<Expander>();
            
            for(int i = 0; i < appRepository.AppVariableCommands.Count; i++)
            {                
                Expanders.Add(new Expander()
                {
                    Header = appRepository.AppVariableCommands[i].NameAppVariableCommand,
                    Margin = new Thickness(3),
                    Content = InitDataGrid(commandVariables[i])
                });                            
            }
        }

        private DataGrid InitDataGrid(ObservableCollection<AppVariable<object>> commandVariables)
        {
            DataGrid dTG = new DataGrid()
            {
                ItemsSource = commandVariables,
                CanUserAddRows = false,
                AutoGenerateColumns = false
            };
            dTG.Columns.Add(new DataGridCheckBoxColumn() { Binding = new Binding("IsChecked"), Header = "Статус" });
            dTG.Columns.Add(new DataGridTextColumn() { Binding = new Binding("NameAppVariable"), Header = "Имя переменной" });
            return dTG;
        }       
    }
}
