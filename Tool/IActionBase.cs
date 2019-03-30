using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
    public interface IActionBase
    {
        DataTable Create(Filter filter);
        DataSet Add(DataTable dT);
    }
}
