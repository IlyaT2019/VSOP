using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.Core.Interface
{
    public interface IActBase
    {
        Task OpenAsync(string path, Filter filter);
        void Create(string path, Filter filter);
        void Save();
        Task<DataSet> ReadAsync(string path);        
        void Close();

    }
}
