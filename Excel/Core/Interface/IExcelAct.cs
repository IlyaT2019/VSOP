using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.Core.Interface
{
    public interface IExcelAct
    {
        Task WritingDateTableInExcel(SheetParametr excelParametr, System.Data.DataTable dtT);

        Task CreateChart(SheetParametr excelParametr, ChartParametr chartParametr);
    }
}