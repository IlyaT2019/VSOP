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
        private Excel _excel;

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

        public BitmapImage imagesource;
        public BitmapImage ImageSource
        {
            get => imagesource;
            set
            {
                imagesource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        public bool IsEnabledComboBox { get; set; } = false;        

        public BaseView(AppRepository<object> appRepository)
        {
            NameTables = new ObservableCollection<string>() { "Выбрите название таблицы" };            
            _appRepository = appRepository;
            _excel = (Excel)new ApiExcel().Create();
        }        

        private async void ViewTable(string nameTable)
        {
            Surface = (DataTable)_appRepository.AppVariables.FirstOrDefault(t => t.NameAppVariable == NameAppVariable.BaseTable).Value;
            var pathSaveImage = Path.Combine((string)_appRepository.AppVariables.FirstOrDefault(v => v.NameAppVariable == NameAppVariable.WorkDir).Value, "Исходная поверхность.jpeg");
            await SetImageBaseSurfase(pathSaveImage, Surface);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(pathSaveImage);
            bitmapImage.EndInit();
            ImageSource = bitmapImage;            
        }

        public async Task SetImageBaseSurfase(string pathSaveImage, DataTable dT)
        {           
            await _excel.CreateAsync("", new Office.Core.Filter() { DisplayAlerts = false, Visible = false });
            await _excel.WritingDateTableInExcel(new SheetParametr() { IndexPage = 1 }, dT);
            await _excel.CreateChart(new SheetParametr() { IndexPage = 1 }, new ChartParametr() { Left = 200, Top = 200, Height = 300, Width = 300, PathToSave = pathSaveImage });
            _excel.Close();
        }

        public async Task SetDataTableOfSurface(string path)
        {            
            var dS = await _excel.ReadAsync(path);
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
