using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
    public interface IActionResult
    {
        DataTable Do(List<Func<DataTable, DataTable>> acts);
        DataTable Do(List<Action> acts);
    }
}
