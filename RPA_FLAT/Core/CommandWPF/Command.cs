using RPA_FLAT.Core.CommandWPF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RPA_FLAT.Core.CommandWPF
{    
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private Action<object> _action;        

        public Command(Action<object> action)
        {
            _action = action;            
        }

        public bool CanExecute(object parameter)
        {
            return true; // Заглушка
        }

        public void Execute(object parameter)
        {
            _action?.Invoke(parameter); 
        }        
    }
}
