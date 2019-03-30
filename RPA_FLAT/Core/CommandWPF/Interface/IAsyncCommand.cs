using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RPA_FLAT.Core.CommandWPF.Interface
{
    public interface IAsyncCommand:ICommand
    {
        Task ExecuteAsync(object param);
    }
}
