using Repository.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_FLAT.Core.Model
{
    public class AppRepository<G>:INotifyPropertyChanged, IDisposable
    {
        private static AppRepository<G> appRepository;
        private static object lockobj = new object();

        private string newName { get; set; }
        public string NewName
        {
            get => newName;
            set
            {
                newName = value;
                OnPropertyChanged("Surface");
            }
        }

        private ObservableCollection<AppVariable<G>> appVariables { get; set; }
        public ObservableCollection<AppVariable<G>> AppVariables
        {
            get => appVariables;
            set
            {
                appVariables = value;
                OnPropertyChanged("AppVariables");
            }
        }

        private ObservableCollection<AppVariableCommand<G>> appVariableCommands { get; set; }
        public ObservableCollection<AppVariableCommand<G>> AppVariableCommands
        {
            get => appVariableCommands;
            set
            {
                appVariableCommands = value;
                OnPropertyChanged("AppVariableCommands");
            }
        }

        private PointSurface point  { get; set; }
        public PointSurface Point
        {
            get => point;
            set
            {
                point = value;
                OnPropertyChanged("Point");
            }
        }

        public AppRepository()
        {
            InitAppVar();
            InitAppVariableCommands();
            InitAppVariableCommands2();
            InitAppVariablesCommands3();            
            point = new PointSurface();
        }

        private void InitAppVar()
        {
            appVariables = new ObservableCollection<AppVariable<G>>();
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.WorkDir, Number = appVariables.Count, TypeVariable = TypeVariable._string });
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.BaseTable, Number = appVariables.Count, TypeVariable = TypeVariable._datatable });
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.SortTable, Number = appVariables.Count, TypeVariable = TypeVariable._datatable });
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.FirstPoint, Number = appVariables.Count, TypeVariable = TypeVariable.Point });
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.MiddlePoint, Number = appVariables.Count, TypeVariable = TypeVariable.Point });
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.FinishPoint, Number = appVariables.Count, TypeVariable = TypeVariable.Point });
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.CountPoints, Number = appVariables.Count, TypeVariable = TypeVariable._int});
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.MaxPoint, Number = appVariables.Count, TypeVariable = TypeVariable._double });
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.A_pr, Number = appVariables.Count, TypeVariable = TypeVariable._double });
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.B_pr, Number = appVariables.Count, TypeVariable = TypeVariable._double });
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.A, Number = appVariables.Count, TypeVariable = TypeVariable._double });
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.D, Number = appVariables.Count, TypeVariable = TypeVariable._double });
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.D1, Number = appVariables.Count, TypeVariable = TypeVariable.Point });
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.Amin, Number = appVariables.Count, TypeVariable = TypeVariable._double });            
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.Nmin, Number = appVariables.Count, TypeVariable = TypeVariable._double });
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.Amax, Number = appVariables.Count, TypeVariable = TypeVariable._double });
            appVariables.Add(new AppVariable<G>() { NameAppVariable = NameAppVariable.Nmax, Number = appVariables.Count, TypeVariable = TypeVariable._double });
        }

        private void InitAppVariableCommands()
        {
            appVariableCommands = new ObservableCollection<AppVariableCommand<G>>();
            appVariableCommands.Add(new AppVariableCommand<G>() {
                NameAppVariableCommand = NameAppVariableCommand.SortingTable,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.BaseTable)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.SortTable)),
                }
            });
            appVariableCommands.Add(new AppVariableCommand<G>()
            {
                NameAppVariableCommand = NameAppVariableCommand.GetFirstPoint,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.SortTable)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FirstPoint)),
                }
            });
            appVariableCommands.Add(new AppVariableCommand<G>()
            {
                NameAppVariableCommand = NameAppVariableCommand.GetMiddlePoint,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.CountPoints)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.SortTable)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.MiddlePoint)),
                }
            });
            appVariableCommands.Add(new AppVariableCommand<G>()
            {
                NameAppVariableCommand = NameAppVariableCommand.GetFinishPoint,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.SortTable)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FinishPoint)),
                }
            });           
        }

        private void InitAppVariableCommands2()
        {
            appVariableCommands.Add(new AppVariableCommand<G>()
            {
                NameAppVariableCommand = NameAppVariableCommand.GetApr,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FirstPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FinishPoint)),
                }
            });
            appVariableCommands.Add(new AppVariableCommand<G>()
            {
                NameAppVariableCommand = NameAppVariableCommand.GetBpr,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FirstPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FinishPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.MiddlePoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.A_pr)),

                }
            });
            appVariableCommands.Add(new AppVariableCommand<G>()
            {
                NameAppVariableCommand = NameAppVariableCommand.GetD,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FirstPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FinishPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.A_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.B_pr)),

                }
            });
            appVariableCommands.Add(new AppVariableCommand<G>()
            {
                NameAppVariableCommand = NameAppVariableCommand.GetMaxPoint,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.MaxPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.A_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.B_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.CountPoints))
                }
            });
            appVariableCommands.Add(new AppVariableCommand<G>()
            {
                NameAppVariableCommand = NameAppVariableCommand.GetD1,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.MaxPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.A_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.B_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.D1))
                }
            });
        }
        
        private void InitAppVariablesCommands3()
        {/*            
            appVariableCommands.Add(new AppVariableCommand<G>()
            {
                NameAppVariableCommand = NameAppVariableCommand.isNotEqualByFirstAndFinishPoint,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FirstPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FinishPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.MiddlePoint))                    
                }
            });

            appVariableCommands.Add(new AppVariableCommand<G>()
            {
                NameAppVariableCommand = NameAppVariableCommand.isEqualFirstPointByXAndNotEqualPointByZ_1,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FirstPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FinishPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.MiddlePoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.CountPoints)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.SortTable)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.A)),
                     (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.Amin)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.Nmin)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.A_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.B_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.D)),
                }
            });

            appVariableCommands.Add(new AppVariableCommand<G>()
            {
                NameAppVariableCommand = NameAppVariableCommand.isEqualFirstPointByXAndNotEqualPointByZ_2,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FirstPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FinishPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.MiddlePoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.Amax)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.SortTable)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.A)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.Nmax)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.A_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.B_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.D)),
                }
            });

            appVariableCommands.Add(new AppVariableCommand<G>()
            {
                NameAppVariableCommand = NameAppVariableCommand.isEqualFirstPointByXAndNotEqualPointByZ_3,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FirstPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FinishPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.MiddlePoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.Amax)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.SortTable)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.A)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.Nmax)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.A_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.B_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.D)),
                }
            });

            appVariableCommands.Add(new AppVariableCommand<G>()
            {
                NameAppVariableCommand = NameAppVariableCommand.isEqualFirstPointByXAndNotEqualPointByZ_4,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FirstPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FinishPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.MiddlePoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.Amin)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.SortTable)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.A)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.Nmin)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.A_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.B_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.D)),
                }
            });

            appVariableCommands.Add(new AppVariableCommand<G>()
            {
                NameAppVariableCommand = NameAppVariableCommand.ListIf,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FirstPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FinishPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.MaxPoint)),

                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.A_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.B_pr))
                    
                }
            });
            */
            appVariableCommands.Add(new AppVariableCommand<G>()
            {
                NameAppVariableCommand = NameAppVariableCommand.Analis,
                Number = appVariableCommands.Count,
                StatusCommand = StatusCommand.Dont_Start,
                CommandVariables = new ObservableCollection<AppVariable<G>>()
                {

                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.BaseTable)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.SortTable)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.FirstPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.CountPoints)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.MaxPoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.MiddlePoint)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.A_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.B_pr)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.A)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.Amin)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.Nmin)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.D)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.Nmax)),
                    (appVariables.FirstOrDefault(i=>i.NameAppVariable == NameAppVariable.Nmax)),

                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string NameProperty=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(NameProperty));           
        }

        public static AppRepository<G> Init()
        {
            lock (lockobj)
            {
                if (appRepository == null)
                {
                    appRepository = new AppRepository<G>();
                }
                return appRepository;                
            }
        }

        public bool Contain(string name)
        {
            return appVariables.Any(e => e.Name == name);
        }

        public string SetName(string name, int count = 0)
        {
            if (Contain(name))
            {
                return SetName(name + "_" + count, count++);
            }
            else
            {
                return name;
            }
        }

        public void Dispose()
        {            
            AppVariables = null;
            AppVariableCommands = null;
        }
    }
}
