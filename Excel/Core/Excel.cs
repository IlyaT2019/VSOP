using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExcelDataReader;
using NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;
using Office.Core.Interface;

namespace Office.Core
{
    public class Excel : OfficeApp, IActBase
    {
        protected override Application ExcelApp { get => base.ExcelApp; set => base.ExcelApp = value; }

        protected override Filter filter { get; set; }
        public Filter Filter => filter;

        private Workbook workbook;
        public Workbook Workbook => workbook;
        
        public Excel()
        {                        
        }

        private void SetFilters(Filter filter, Application app)
        {
            this.filter = filter;
            app.Visible = filter.Visible == null ? false: (bool)filter.Visible;
            app.DisplayAlerts = filter.DisplayAlerts == null ? false : (bool)filter.DisplayAlerts;
        }

        public async Task OpenAsync(string Path, Filter filter)
        {
            await Task.Run(() => 
            {
                ExcelApp = new Application();
                SetFilters(filter, ExcelApp);
            });
        }       

        public async Task<DataSet> ReadAsync(string path)
        {            
            return await Task.Run(() => 
            {
                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        return reader.AsDataSet();
                    }
                }
            });                         
        }

        public async Task CreateAsync(string path, Filter filter)
        {
            await Task.Run(()=> 
            {
                ExcelApp = new Application();
                SetFilters(filter, ExcelApp);
                workbook = ExcelApp.Workbooks.Add();
            });            
        }        

        public Worksheet SetWorkSheet(SheetParametr excelParametr)
        {
            if (string.IsNullOrEmpty(excelParametr.NamePage))
              return (Worksheet)workbook.Worksheets[excelParametr.IndexPage];
            else
                return (Worksheet)workbook.Worksheets[excelParametr.NamePage];
        }


        public async Task CreateChart(SheetParametr excelParametr, ChartParametr chartParametr)
        {
            await Task.Run(()=> 
            {
                var worksheet = SetWorkSheet(excelParametr);

                var chart = ((ChartObjects)worksheet.ChartObjects()).Add(chartParametr.Left, chartParametr.Top, chartParametr.Width, chartParametr.Width);
                var cell1 = worksheet.UsedRange.Cells[worksheet.UsedRange.Rows.First().Row, worksheet.UsedRange.Columns.First().Column];
                var cell2 = worksheet.UsedRange.Cells[worksheet.UsedRange.Rows.Last().Row, worksheet.UsedRange.Columns.Last().Column];
                chart.Chart.SetSourceData(worksheet.Range(cell1, cell2));
                chart.Chart.ChartType = XlChartType.xlSurface;
                chart.Chart.Export(chartParametr.PathToSave);
            });            
        }

        public async Task WritingDateTableInExcel(SheetParametr excelParametr, System.Data.DataTable dtT)
        {
            await Task.Run(() => 
            {
                var worksheet = SetWorkSheet(excelParametr);

                for (int Row = 2; Row < dtT.Rows.Count; Row++)
                {
                    for (int Column = 2; Column < dtT.Columns.Count; Column++)
                    {
                        worksheet.Cells[Row, Column].Value = dtT.Rows[Row][Column].ToString();
                    }
                }
            });            
        }

        public void Save()
        {
            workbook.Save();            
        }

        public void Close()
        {
            workbook.Close();
            workbook.Dispose();
            ExcelApp.Quit();           
            ExcelApp.Dispose();            
        }

        
    }
}
