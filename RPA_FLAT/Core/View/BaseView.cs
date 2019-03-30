using Office.Core;
using Repository.Core;
using RPA_FLAT.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool;

namespace RPA_FLAT.Core.View
{
    public class BaseView : INotifyPropertyChanged
    {
        private AppRepository<object> _appRepository;

        private string selectedTable = "Выбрите название таблицы";
        public string SelectedTable
        {
            get => selectedTable;
            set
            {
                selectedTable = value;                
                ViewTable(selectedTable);
            }
        }

        public bool IsEnabledComboBox { get; set; } = false;

        private ObservableCollection<string> nameTables { get; set; }
        public ObservableCollection<string> NameTables
        {
            get => nameTables;
            set
            {
                nameTables = value;
                OnPropertyChanged("NameTables");
            }
        }

        private DataTable surface { get; set; }
        public DataTable Surface
        {
            get => surface;
            set
            {
                surface = value;
                OnPropertyChanged("Surface");
            }
        }

        public BaseView(AppRepository<object> appRepository)
        {
            NameTables = new ObservableCollection<string>() { "Выбрите название таблицы" };            
            _appRepository = appRepository;
        }        

        private void ViewTable(string nameTable)
        {
            var dT = (DataTable)_appRepository.AppVariables.FirstOrDefault(t => t.NameAppVariable == NameAppVariable.BaseTable).Value;
            Surface = dT;
        }

        public async Task SetDataTableOfSurface(string path)
        {
            var excel = (Excel)new ApiExcel().Create();
            var dS = await excel.ReadAsync(path);
            if (dS.Tables.Count == 1)
            {
                var dT = SetFilter(dS.Tables[0]);
                NameTables.Add(_appRepository.SetName(dS.Tables[0].TableName));
                _appRepository.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.BaseTable).Value = dT;
            }
            if (dS.Tables.Count > 1)
            {
                foreach (DataTable t in dS.Tables)
                {
                    var dT = SetFilter(t);
                    NameTables.Add(_appRepository.SetName(dS.Tables[0].TableName));
                    _appRepository.AppVariables.FirstOrDefault(i => i.NameAppVariable == NameAppVariable.BaseTable).Value = dT;
                }
            }          
        }

        private DataTable SetFilter(DataTable dataTable)
        {
            var dTAct = new DataTableAct(dataTable);
            dTAct.DeleteNullColumns();
            dTAct.DeleteNullRows();
            dTAct.RenameColumns(new Tool.Filter());
            return dTAct.UpdateDt;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string NameProperty = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(NameProperty));
        }
    }
}
