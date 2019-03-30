
using NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.Core
{
    public class Document <T> 
    {
        







































































        /*
        private Application ApplicationExcel;

        private Workbook DocumentExcel;

        private Worksheet Sheet;

        public Excel(bool _Visible)
        {
            InitApplicationExcel(_Visible);
        }

        private void InitApplicationExcel(bool _Visible)
        {
            ApplicationExcel = new Application();

            ApplicationExcel.Visible = _Visible;

            ApplicationExcel.DisplayAlerts = false;
        }

        public void CreateWorkbookExcel()
        {
            DocumentExcel = null;

            DocumentExcel = ApplicationExcel.Workbooks.Add();
        }

        public void OpenExcelDocument(string NameFile, string PathToFile = "")
        {
            if (PathToFile == "")
            {
                DocumentExcel = ApplicationExcel.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, NameFile));
            }
            else
            {
                DocumentExcel = ApplicationExcel.Workbooks.Open(PathToFile);
            }
        }

        public void SaveAsDocumentExcel(bool SaveAs = false, string Name = "", string PathForSaveAs = "")
        {
            if (SaveAs == true)
            {
                if (PathForSaveAs == "")
                {
                    DocumentExcel.SaveAs(Path.Combine(Environment.CurrentDirectory, Name), XlFileFormat.xlWorkbookNormal);
                }
                else
                {
                    DocumentExcel.SaveAs(Path.Combine(PathForSaveAs, Name), XlFileFormat.xlWorkbookNormal);
                }
            }
            else
            {
                DocumentExcel.Save();
            }
        }

        public void SetWorkSheet(int Index = 1, string Name = "")
        {
            if (Index == 1 && Name == "")
            {
                Sheet = (Worksheet)DocumentExcel.Worksheets[Index];
            }
            else
            {
                if (Name == "" && Index != 1)
                {
                    Sheet = (Worksheet)DocumentExcel.Worksheets[Index];
                }
                else
                {
                    Sheet = (Worksheet)DocumentExcel.Worksheets[Name];
                }
            }
        }


        public void Create3DSurface(int Left, int Top, int Width, int Height, string PathToSave)
        {
            var Chart_3D = ((ChartObjects)Sheet.ChartObjects()).Add(Left, Top, Width, Height);

            var Cell_1 = GetCell(Sheet.UsedRange.Rows.First().Cells.Row, Sheet.UsedRange.Columns.First().Column);

            var Cell_2 = GetCell(Sheet.UsedRange.Rows.Last().Cells.Row, Sheet.UsedRange.Columns.Last().Column);

            Chart_3D.Chart.SetSourceData(GetRange(Cell_1, Cell_2));

            Chart_3D.Chart.ChartType = XlChartType.xlSurface;

            Chart_3D.Chart.Export(PathToSave); 
        }

        public void WritingDateTableInExcel(System.Data.DataTable DataTableCoordinate)
        {
            for (int Row = 0; Row < DataTableCoordinate.Rows.Count; Row++)
            {
                for (int Column = 0; Column < DataTableCoordinate.Columns.Count; Column++)
                {
                    SetValueCell(Row + 2, Column + 2, DataTableCoordinate.Rows[Row][Column].ToString());
                }
            }
        }

        public Range GetRange(object Cell_1, object Cell_2)
        {
            return Sheet.Range(Cell_1, Cell_2);
        }

        public object GetCell(int Row, int Column)
        {
            return Sheet.UsedRange.Cells[Row, Column];
        }

        public void SetValueCell(int Row, int Column, string ValueCell)
        {
            Sheet.Cells[Row, Column].Value2 = ValueCell;
        }
        */
    }
}

