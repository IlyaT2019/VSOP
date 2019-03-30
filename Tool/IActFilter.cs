using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
    public interface IActFilter
    {
        DataTable Rename(Filter filter, DataTable dT = null);
        DataTable RenameColumns(Filter filter, DataTable dT = null);
        DataTable DeleteNullRows(DataTable dT = null);
        DataTable DeleteNullColumns(DataTable dT = null);
        DataTable CreateColumn(Filter filter, DataTable dT = null);
        DataTable CreateRow(Filter filter, DataTable dT = null);     
    }
}
