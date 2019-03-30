using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_FLAT.Core.Working_File
{
    public class CSV_File
    {
        public static DataTable ReadCSV(string PathToSCV = null)
        {
            DataTable TableCoordinateZOfPoints = null;

            FileInfo fileInfo = new FileInfo(PathToSCV);

            if (fileInfo.Exists)
            {
                List<string> RowWithValues = File.ReadAllLines(PathToSCV).ToList();

                var ColumnWithValues = from Column in RowWithValues select Column.Split(';').ToList();

                var CountColumns = ColumnWithValues.Count() + 1;

                TableCoordinateZOfPoints = WorkDataTable.CreateOfDataTable(CountColumns, RowWithValues.Count(), ColumnWithValues.ToList());
            }
            return TableCoordinateZOfPoints;
        }
    }
}
