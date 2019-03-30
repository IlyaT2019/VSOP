using MahApps.Metro.Controls;
using Office.Core;
using Repository.Core;
using RPA_FLAT.Core.CommandWPF;
using RPA_FLAT.Core.Model;
using RPA_FLAT.Core.TOOLS;
using RPA_FLAT.Core.View;
using RPA_FLAT.Core.Window;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Tool;

namespace RPA_FLAT.Core.Context
{
    public class BaseContext
    {
        public AppRepository<object> AppRepository { get; set; }
        public BaseView BaseView { get; set; }

        public BaseContext()
        {                       
            AppRepository = AppRepository<object>.Init();            
            CreateWorkFolder();
            BaseView = new BaseView(AppRepository);
        }

        private async void CreateWorkFolder()
        {
            await Task.Run(() => 
            {
                var pathWorkDir = ConfigurationManager.AppSettings["WorkDir"];
                AppRepository.AppVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.WorkDir).Value = pathWorkDir;
                if (!Directory.Exists(pathWorkDir))
                {
                    Directory.CreateDirectory(pathWorkDir);                  
                }
                
            });            
        }

        #region Открытие/закрытие вкладок
        private Command openStartPageTab;
        public Command OpenStartPageTab
        {
            get => openStartPageTab ?? (openStartPageTab = new Command((o) =>
            {
                var parentGrid = o as Grid;
                var comboBoxNameTables = parentGrid.FindChild<ComboBox>("ComboBoxNameTables");
                comboBoxNameTables.Visibility = Visibility.Visible;

                var dataGridSurface = parentGrid.FindChild<DataGrid>("DataGridSurface");
                dataGridSurface.Visibility = Visibility.Visible;

                var dataGridVariable = parentGrid.FindChild<DataGrid>("DataGridVariable");
                dataGridVariable.Visibility = Visibility.Hidden;

                var dataGridCommand = parentGrid.FindChild<DataGrid>("DataGridCommand");
                dataGridCommand.Visibility = Visibility.Hidden;

                var lisboxResultVariable = parentGrid.FindChild<ListBox>("LisboxResultVariable");
                lisboxResultVariable.Visibility = Visibility.Hidden;

                var labelNameTag = parentGrid.FindChild<Label>("LabelNameTag");
                labelNameTag.Content = "Стартовая страница";

                var stackPanelTools = parentGrid.FindChild<StackPanel>("StackPanelTools");
                stackPanelTools.Visibility = Visibility.Hidden;
            })); 
        }

        private Command openVariablesTab;
        public Command OpenVariablesTab
        {
            get => openVariablesTab ?? (openVariablesTab = new Command((o) =>
            {
                var parentGrid = o as Grid;
                var comboBoxNameTables = parentGrid.FindChild<ComboBox>("ComboBoxNameTables");
                comboBoxNameTables.Visibility = Visibility.Hidden;

                var dataGridSurface = parentGrid.FindChild<DataGrid>("DataGridSurface");
                dataGridSurface.Visibility = Visibility.Hidden;

                var dataGridVariable = parentGrid.FindChild<DataGrid>("DataGridVariable");
                dataGridVariable.Visibility = Visibility.Visible;

                var dataGridCommand = parentGrid.FindChild<DataGrid>("DataGridCommand");
                dataGridCommand.Visibility = Visibility.Hidden;

                var lisboxResultVariable = parentGrid.FindChild<ListBox>("LisboxResultVariable");
                lisboxResultVariable.Visibility = Visibility.Hidden;

                var labelNameTag = parentGrid.FindChild<Label>("LabelNameTag");
                labelNameTag.Content = "ПЕРЕМЕННЫЕ";

                var stackPanelTools = parentGrid.FindChild<StackPanel>("StackPanelTools");
                stackPanelTools.Visibility = Visibility.Visible;
                
            }));
        }

        private Command openCommandsTab;
        public Command OpenCommandsTab
        {
            get => openCommandsTab ?? (openCommandsTab = new Command((o) =>
            {
                var parentGrid = o as Grid;
                var comboBoxNameTables = parentGrid.FindChild<ComboBox>("ComboBoxNameTables");
                comboBoxNameTables.Visibility = Visibility.Hidden;

                var dataGridSurface = parentGrid.FindChild<DataGrid>("DataGridSurface");
                dataGridSurface.Visibility = Visibility.Hidden;

                var dataGridVariable = parentGrid.FindChild<DataGrid>("DataGridVariable");
                dataGridVariable.Visibility = Visibility.Hidden;

                var dataGridCommand = parentGrid.FindChild<DataGrid>("DataGridCommand");
                dataGridCommand.Visibility = Visibility.Visible;

                var lisboxResultVariable = parentGrid.FindChild<ListBox>("LisboxResultVariable");
                lisboxResultVariable.Visibility = Visibility.Hidden;

                var labelNameTag = parentGrid.FindChild<Label>("LabelNameTag");
                labelNameTag.Content = "КОММАНДЫ";

                var stackPanelTools = parentGrid.FindChild<StackPanel>("StackPanelTools");
                stackPanelTools.Visibility = Visibility.Visible;
            }));
        }

        private Command openResultTab;
        public Command OpenResultTab
        {
            get => openResultTab ?? (openResultTab = new Command((o)=> 
            {
                var parentGrid = o as Grid;
                var comboBoxNameTables = parentGrid.FindChild<ComboBox>("ComboBoxNameTables");
                comboBoxNameTables.Visibility = Visibility.Hidden;

                var dataGridSurface = parentGrid.FindChild<DataGrid>("DataGridSurface");
                dataGridSurface.Visibility = Visibility.Hidden;

                var dataGridVariable = parentGrid.FindChild<DataGrid>("DataGridVariable");
                dataGridVariable.Visibility = Visibility.Hidden;

                var dataGridCommand = parentGrid.FindChild<DataGrid>("DataGridCommand");
                dataGridCommand.Visibility = Visibility.Hidden;                

                var labelNameTag = parentGrid.FindChild<Label>("LabelNameTag");
                labelNameTag.Content = "РЕЗУЛЬТАТЫ ВЫЧИСЛЕНИЙ";

                var lisboxResultVariable = parentGrid.FindChild<ListBox>("LisboxResultVariable");
                lisboxResultVariable.Visibility = Visibility.Visible;
                lisboxResultVariable.ItemsSource = new ResultView(AppRepository).GetContentListBox();

                var stackPanelTools = parentGrid.FindChild<StackPanel>("StackPanelTools");
                stackPanelTools.Visibility = Visibility.Hidden;
            }));
        }

        private Command openModelsTab;
        public Command OpenModelsTab
        {
            get => openModelsTab ?? (openModelsTab = new Command((o) =>
            {

            }));
        }
        #endregion

        #region Открытие файла и получение DataView
        private Command _openFile;
        public Command OpenFile
        {
            get => _openFile ?? (_openFile = new Command(setData));
        }

        private async void setData(object wpfElement)
        {
            using (var dialogService = new DialogService())
            {
                var informDs = dialogService.Open();
                if (informDs != null)
                {
                    switch (informDs.ExpansionFile.GetHashCode())
                    {
                        case 0: break;
                        case 1: break;
                        case 2:
                            await BaseView.SetDataTableOfSurface(informDs.Path);                            
                            break;
                    }
                }
            }
        }

       
        /*
        private Command redaction;
        public Command Redaction
        {
            get => redaction ?? (redaction = new Command((o) =>
            {
                var variableWindow = new RedactionVariableWindow(AppRepository, SelectedTable);
                if (variableWindow.ShowDialog() == true)
                {
                    variableWindow.Show();
                }
            }));
        }
        */
        #endregion

        #region "Добавление/удаление переменных"
        private Command addVariable;
        public Command AddVariable
        {
            get => addVariable ?? (addVariable = new Command((o) =>
            {
                AppRepository.AppVariables.Add(new AppVariable<object>()
                {                    
                    Number = AppRepository.AppVariables.Count
                });
            }));
        }

        private Command removeVariable;
        public Command RemoveVariable
        {
            get => removeVariable ?? (removeVariable = new Command((o) =>
            {
                for (int i = AppRepository.AppVariables.Count - 1; i > 0; i--)
                {
                    if (AppRepository.AppVariables[i].IsChecked)
                    {
                        AppRepository.AppVariables.RemoveAt(i);
                    }
                }
                ChangeIndexAppVariables();
            }));
        }

        private void ChangeIndexAppVariables()
        {
            foreach (var v in AppRepository.AppVariables)
            {
                v.Number = AppRepository.AppVariables.IndexOf(v);
            }
            CollectionViewSource.GetDefaultView(AppRepository.AppVariables).Refresh();
        }
        #endregion

        #region "Добавление/удаление комманд"
        private Command addCommand;
        public Command AddCommand
        {
            get => addCommand ?? (addCommand = new Command((o) =>
            {
                var appVariableCommand = new AppVariableCommand<object>()
                {                    
                    Number = AppRepository.AppVariableCommands.Count
                };
                AppRepository.AppVariableCommands.Add(appVariableCommand);
                //((DataGridTemplateColumn)o).CellTemplate = InitDataTemplate(appVariableCommand);
            }));
        }

        private Command chooseAll;
        public Command ChooseAll
        {
            get => chooseAll ?? (chooseAll = new Command(o => 
            {
                foreach (var i in AppRepository.AppVariableCommands)
                {
                    i.IsChecked = true;
                }
                CollectionViewSource.GetDefaultView(AppRepository.AppVariables).Refresh();
            }));
        }

        /*
        private DataTemplate InitDataTemplate(AppVariableCommand<object> appVariableCommand)
        {
            var dtTemp = new DataTemplate();
            dtTemp.VisualTree = InitStackPanel(appVariableCommand);

            return dtTemp;
        }
        private FrameworkElementFactory InitStackPanel(AppVariableCommand<object> appVariableCommand)
        {            
            var buttonDoCommand = new FrameworkElementFactory(typeof(Button));
            buttonDoCommand.SetValue(Button.ContentProperty, "Выполнить команду");
            var skPanel = new FrameworkElementFactory(typeof(StackPanel));
            skPanel.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);            
            skPanel.AppendChild(buttonDoCommand);
            return skPanel;
        }*/

        private Command doCommand;
        public Command DoCommand
        {
            get => doCommand ?? (doCommand = new Command((o) => 
            {
                using (var logica = new Logica(AppRepository))
                {
                    logica.DoCommand();
                }                    
            }));
        }

        private Command addVariableInCommand;
        public Command AddVariableInCommand
        {
            get => addVariableInCommand ?? (addVariableInCommand = new Command((o) =>
            {
                var cmdVariablesWin = new CommandVariablesWindow(AppRepository);
                if (cmdVariablesWin.ShowDialog() == true)
                {
                    cmdVariablesWin.Show();
                }
            }));
        }

        private Command removeCommand;
        public Command RemoveCommand
        {
            get => removeCommand ?? (removeCommand = new Command((o) =>
            {
                for (int i = AppRepository.AppVariableCommands.Count-1; i >= 0; i--)
                {
                    if (AppRepository.AppVariableCommands[i].IsChecked == true)
                    {
                        AppRepository.AppVariableCommands.RemoveAt(i);
                    }                    
                }
                ChangeIndexAppCommandVariables();
            }));
        }
        private void ChangeIndexAppCommandVariables()
        {
            foreach (var v in AppRepository.AppVariableCommands)
            {
                v.Number = AppRepository.AppVariableCommands.IndexOf(v);
            }
            CollectionViewSource.GetDefaultView(AppRepository.AppVariableCommands).Refresh();
        }
        #endregion
      
        #region "Сохранение/Загрузка параметров приложения"
        private Command saveParamsApp;
        public Command SaveParamsApp
        {
            get => saveParamsApp ?? (saveParamsApp = new Command(async (o) =>
            {
                var variableXml = AppRepository.AppVariables.FirstOrDefault(e => e.NameAppVariable == NameAppVariable.WorkDir).Value.ToString();
                using (var xml = new ActXml<AppRepository<object>>(Path.Combine(variableXml,"Variables")))
                {
                    await xml.SerializeAsync(AppRepository);
                }
            }));
        }
        
        private Command downloadParamsApp;
        public Command DownloadParamsApp
        {
            get => downloadParamsApp ?? (downloadParamsApp = new Command(async(o)=> 
            {
                var variableXml = AppRepository.AppVariables.FirstOrDefault(e => e.NameAppVariable == NameAppVariable.WorkDir).Value.ToString();
                using (var xml = new ActXml<AppRepository<object>>(Path.Combine(variableXml, "Variables")))
                {
                    var result = await xml.DesirializeAsync();

                    AddAppVariableXml(result.AppVariables);
                    AddAppCommandVariables(result.AppVariableCommands);
                    CollectionViewSource.GetDefaultView(AppRepository.AppVariables).Refresh();
                }
            }));            
        }

        private void AddAppVariableXml(ObservableCollection<AppVariable<object>> appVariables)
        {
            foreach (var v in appVariables)
            {
                AppRepository.AppVariables.Add(new AppVariable<object>()
                {
                    Name = AppRepository.SetName(v.Name),
                    Number = AppRepository.AppVariables.Count,
                    TypeVariable = v.TypeVariable,
                    Value = v.Value
                });
            }
        }

        private void AddAppCommandVariables(ObservableCollection<AppVariableCommand<object>> appVariableCommands)
        {
            var temp = new ObservableCollection<AppVariable<object>>();

            foreach (var v in appVariableCommands)
            {
                foreach (var i in v.CommandVariables)
                {
                    temp.Add(new AppVariable<object>() { Name = AppRepository.SetName(i.Name), TypeVariable = i.TypeVariable, Value = i.Value});                    
                }
                AppRepository.AppVariableCommands.Add(new AppVariableCommand<object>()
                {
                    Name = AppRepository.SetName(v.Name),
                    Number = AppRepository.AppVariableCommands.Count,
                    CommandVariables = temp
                });
            }
        }
        #endregion
    }
}
