using RPA_FLAT.Core.CommandWPF;
using RPA_FLAT.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RPA_FLAT.Core.Context
{
    public class RedactionVariableContext
    {
        public AppRepository<object> AppRepository { get; set; } 

        private string _nameVariable { get; set; }       
       
        public RedactionVariableContext(AppRepository<object> appRepository, string nameVariable)
        {
            AppRepository = appRepository;
            _nameVariable = nameVariable;
        }
        
        private Command save;
        public Command Save
        {
            get => save ?? (save = new Command((o) => 
            {
                var variable = AppRepository.AppVariables.FirstOrDefault(e => e.Name == _nameVariable);                
                ((DataTable)variable.Value).TableName = AppRepository.NewName;                
                variable.Name = AppRepository.NewName;
                CollectionViewSource.GetDefaultView(AppRepository.AppVariables).Refresh();
            }));
        }
    }
}
