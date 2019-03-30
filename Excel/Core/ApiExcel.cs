using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.Core
{
    public class ApiExcel : Api
    {
        public override OfficeApp Create()
        {
            return new Excel();
        }
    }
}
