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

        public void Create(string path, Filter filter)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            workbook.Close();
            ExcelApp.Dispose();            
        }        
    }
}
