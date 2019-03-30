using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tool
{
    public class DataTableAct : IActFilter,IActionResult,IDisposable
    {
        private DataTable baseDt;
        public DataTable BaseDt => baseDt;

        private DataTable temp;
        
        private DataTable updateDt;
        public DataTable UpdateDt => updateDt;

        public DataTableAct(DataTable dT)
        {
            baseDt = dT;
        }
    
        public DataTable Do(List<Action> acts)
        {
            foreach (var act in acts)
            {
                act.Invoke();
            }
            return null;
        }

        public void Dispose()
        {
           
        }

        public DataTable DeleteNullRows(DataTable dT = null)
        {
            if (baseDt != null)
            {
                for (int i = 0; i < baseDt.Rows.Count; i++)
                {
                    if (baseDt.Rows[i].ItemArray.All(a => a.ToString() == ""))
                    {
                        baseDt.Rows[i].Delete();
                    }
                }
                updateDt = baseDt;
            }
            else
            {
                //TODO повтор кода
                for (int i = 0; i < dT.Rows.Count; i++)
                {
                    if (dT.Rows[i].ItemArray.All(a => a.ToString() == ""))
                    {
                        dT.Rows[i].Delete();
                    }
                }
                updateDt = dT;
            }
            return updateDt;
        }

        public DataTable Rename(Filter filter, DataTable dT = null)
        {
            throw new NotImplementedException();
        }

        public DataTable RenameColumns(Filter filter, DataTable dT = null)
        {
            if (baseDt != null)
            {
                for (int i = 0; updateDt.Columns.Count == filter.ColumnNames.Count && i < filter.ColumnNames.Count; i++)
                {
                    updateDt.Columns[i].ColumnName = filter.ColumnNames[i];
                }
                updateDt = baseDt;
            }
            else
            {
                for (int i = 0; dT.Columns.Count == filter.ColumnNames.Count && i < filter.ColumnNames.Count; i++)
                {
                    dT.Columns[i].ColumnName = filter.ColumnNames[i];
                }
                updateDt = dT;
            }

            filter.Dispose();

            return updateDt;
        }

        public DataTable DeleteNullColumns(DataTable dT = null)
        {
            if (baseDt != null)
            {
                for (int i = baseDt.Columns.Count - 1; i >= 0; i--)
                {
                    if (baseDt.AsEnumerable().All(r => string.IsNullOrEmpty(r[i].ToString())))
                    {
                        baseDt.Columns.RemoveAt(i);
                    }
                }

                updateDt = baseDt;
            }
            else
            {
                for (int i = dT.Columns.Count - 1; i >= 0; i--)
                {
                    if (dT.AsEnumerable().All(r => string.IsNullOrEmpty(r[i].ToString())))
                    {
                        dT.Columns.RemoveAt(i);
                    }
                }

                updateDt = dT;
            }

            return updateDt;
        }

        public DataTable CreateColumn(Filter filter, DataTable dT = null)
        {
            throw new NotImplementedException();
        }

        public DataTable CreateRow(Filter filter, DataTable dT = null)
        {
            throw new NotImplementedException();
        }

        public DataTable Do(List<Func<DataTable, DataTable>> acts)
        {
            throw new NotImplementedException();
        }
    }
}
