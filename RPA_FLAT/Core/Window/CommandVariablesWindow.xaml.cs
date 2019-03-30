using MahApps.Metro.Controls;
using RPA_FLAT.Core.Context;
using RPA_FLAT.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RPA_FLAT.Core.Window
{
    /// <summary>
    /// Логика взаимодействия для SetCommandVariablesWindow.xaml
    /// </summary>
    public partial class CommandVariablesWindow : MetroWindow
    {
        public CommandVariablesWindow(AppRepository<object> appRepository)
        {
            InitializeComponent();

            DataContext = new CommandVariablesContext(appRepository);
        }
    }
}
