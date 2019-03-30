using Repository.Core;
using RPA_FLAT.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RPA_FLAT.Core.View
{
    public class ResultView
    {
        private AppRepository<object> repository;

        public ResultView (AppRepository<object> repository)
        {
            this.repository = repository;
        }

        public List<Expander> GetContentListBox()
        {
            return new List<Expander>()
            {
                GetExpanderSurfacePoints(),
                GetExpanderSortedTable(),
                GetExpanderSimpleVariableValues()
            }; 
        }

        private Expander GetExpanderSimpleVariableValues()
        {
            var result = repository.AppVariables
                .Where(v => v.NameAppVariable != NameAppVariable.FirstPoint)
                .Where(v => v.NameAppVariable != NameAppVariable.MiddlePoint)
                .Where(v => v.NameAppVariable != NameAppVariable.FinishPoint)
                .Where(v => v.NameAppVariable != NameAppVariable.MaxPoint)
                .Where(v => v.NameAppVariable != NameAppVariable.SortTable)
                .Where(v => v.NameAppVariable != NameAppVariable.BaseTable)
                .Select(v => v).ToList();

            return new Expander()
            {
                Header = "Расчетные коэффициенты",
                Content = GetDataGridSimpleVariableValues(result)
            };
        }

        private DataGrid GetDataGridSimpleVariableValues(List<AppVariable<object>> appVariables)
        {
            var dTG = new DataGrid();
            dTG.ItemsSource = appVariables;
            dTG.AutoGenerateColumns = false;
            dTG.CanUserAddRows = false;
            dTG.Columns.Add(new DataGridTextColumn() { Header = "Название переменной", Binding = new Binding("NameAppVariable") });
            dTG.Columns.Add(new DataGridTextColumn() { Header = "Значение", Binding = new Binding("Value") });
            return dTG;
        }

        private Expander GetExpanderSortedTable()
        {
            return new Expander()
            {
                Header = "Отсортированная таблица",
                Content = new DataGrid()
                {                    
                    ItemsSource = ((DataTable)repository.AppVariables.FirstOrDefault(p => p.NameAppVariable == NameAppVariable.SortTable).Value).AsDataView()
                }
            };
        }

        private Expander GetExpanderSurfacePoints()
        {
            var points = new List<PointSurface>()
            {
                (PointSurface)repository.AppVariables.FirstOrDefault(p=>p.NameAppVariable == NameAppVariable.FirstPoint).Value,
                (PointSurface)repository.AppVariables.FirstOrDefault(p=>p.NameAppVariable == NameAppVariable.MiddlePoint).Value,
                (PointSurface)repository.AppVariables.FirstOrDefault(p=>p.NameAppVariable == NameAppVariable.FinishPoint).Value,
            };
            return new Expander()
            {
                Header = "Заданные точки",
                Content = GetDataGridSurfacePoints(points)
            };
        }

        private DataGrid GetDataGridSurfacePoints(List<PointSurface> points)
        {
            var dTG = new DataGrid();
            dTG.ItemsSource = points;
            dTG.AutoGenerateColumns = false;
            dTG.CanUserAddRows = false;
            dTG.Columns.Add(new DataGridTextColumn() { Header = "Название точки", Binding = new Binding("Name") });
            dTG.Columns.Add(new DataGridTextColumn() { Header = "Номер точки", Binding = new Binding("Number") });
            dTG.Columns.Add(new DataGridTextColumn() { Header = "X", Binding = new Binding("X") });
            dTG.Columns.Add(new DataGridTextColumn() { Header = "Y", Binding = new Binding("Y") });
            dTG.Columns.Add(new DataGridTextColumn() { Header = "Z", Binding = new Binding("Z") });
            return dTG;
        }
    }
}
