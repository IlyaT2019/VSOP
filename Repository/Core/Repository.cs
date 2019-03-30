using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Core
{
    public class Repository<G>
    {
        public List<AppVariable<G>> AppVariables { get; set; }

        public List<AppVariableCommand<G>> VariableCommands { get; set; }

        public Repository()
        {
            AppVariables = new List<AppVariable<G>>();

            VariableCommands = new List<AppVariableCommand<G>>();
        }

        public AppVariable<string> GetVariableTypeString(string nameVariable)
        {
            AppVariable<string> appVariable = new AppVariable<string>();

            AppVariables.ForEach(e =>
            {
                if (e.Name == nameVariable)
                {
                    appVariable.Name = e.Name;

                    appVariable.Value = e.Value as string;// сделать на основе условного выражения

                    appVariable.TypeVariable = TypeVariable._string;
                }
            });

            return appVariable;
        }

        public AppVariable<double> GetVariableTypeDouble(string nameVariable)
        {
            AppVariable<double> appVariable = new AppVariable<double>();

            AppVariables.ForEach(e =>
            {
                if (e.Name == nameVariable)
                {
                    appVariable.Name = e.Name;

                    appVariable.Value = Convert.ToDouble(e.Value);// сделать на основе условного выражения

                    appVariable.TypeVariable = TypeVariable._double;
                }
            });

            return appVariable;
        }

        public AppVariable<DataTable> GetVariableTypeDataTable(string nameVariable)
        {
            AppVariable<DataTable> appVariable = new AppVariable<DataTable>();

            AppVariables.ForEach(e =>
            {
                if (e.Name == nameVariable)
                {
                    appVariable.Name = e.Name;

                    appVariable.Value = e.Value as DataTable;// сделать на основе условного выражения

                    appVariable.TypeVariable = TypeVariable._datatable;
                }
            });

            return appVariable;
        }
    }
}
