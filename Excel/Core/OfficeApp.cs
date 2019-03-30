using NetOffice.ExcelApi;
using NetOffice.WordApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.Core
{
    public abstract class OfficeApp
    {
        protected virtual NetOffice.ExcelApi.Application ExcelApp { get; set; }   
        protected virtual NetOffice.ExcelApi.Application WordApp { get; set; }  
        protected abstract Filter filter { get; set; }
    }
}
