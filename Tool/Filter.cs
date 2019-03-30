using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
    public class Filter:IDisposable
    {
        public string NameDataTable { get; set; }
        public int CountRow { get; set; }
        public int CountColumn { get; set; }
        public List<string> ColumnNames { get; set; }
        public List<string> Values { get; set; }
        public List<DataRow> Rows { get; set; }
        public List<DataColumn> Columns { get; set; }  
        
        public Filter()
        {
            ColumnNames = new List<string>() { "Номер точки", "X", "Y", "Z" };
            Values = new List<string>();
            Rows = new List<DataRow>();
            Columns = new List<DataColumn>();
        }

        public void Dispose()
        {
            NameDataTable = null;
            ColumnNames = null;
            Values = null;
            Rows = null;
            Columns = null;
        }
}
}
