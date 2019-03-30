using Repository.Core;
using RPA_FLAT.Core.CommandWPF;
using RPA_FLAT.Core.Model;
using RPA_FLAT.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace RPA_FLAT.Core.Context
{
    public class CommandVariablesContext
    {        
        public CommandVariablesView CmdVarView { get; set; }
        private CommandVariablesModel comVars;
        private AppRepository<object> _appRepository;

        public CommandVariablesContext(AppRepository<object> appRepository)
        {
            _appRepository = appRepository; 
            comVars = InitComVar(appRepository);
            CmdVarView = new CommandVariablesView(appRepository, comVars.CommandVariables);
        }         

        private CommandVariablesModel InitComVar(AppRepository<object> appRepository)
        {
            var comVarModel = new CommandVariablesModel();            

            foreach (var j in appRepository.AppVariableCommands)
            {
                var comVars = new ObservableCollection<AppVariable<object>>();

                foreach (var i in appRepository.AppVariables)
                {                    
                    comVars.Add(new AppVariable<object>() { NameAppVariable = i.NameAppVariable, Number = i.Number, TypeVariable = i.TypeVariable, Value = i.Value });
                }
                comVarModel.CommandVariables.Add(comVars);
            }
            return comVarModel;
        }

        private Command saveCommandVariables;
        public Command SaveCommandVariables
        {

            get => saveCommandVariables ?? (saveCommandVariables = new Command((o) =>
            {
                for (int i = 0; i < comVars.CommandVariables.Count; i++)
                {
                    foreach (var j in comVars.CommandVariables[i])
                    {
                        if (j.IsChecked == true)
                        {
                            _appRepository.AppVariableCommands[i].CommandVariables.Add(j);
                        }
                    }
                    
                }
                CollectionViewSource.GetDefaultView(_appRepository.AppVariableCommands).Refresh();                
            }));
        }
    }
}
